using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI.Common;
using ProjectWorkApi;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.Extensions.ObjectPool;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("localhost",
        corsPolicyBuilder => corsPolicyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().WithExposedHeaders("Content-Disposition")
    );
});
builder.Services.AddDbContext<DatabaseContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("MainDB")));
builder.Services.AddAutoMapper(c=>c.AddProfile<DtoMappingProfile>());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("localhost");
app.UseHttpsRedirection();

app.MapGet("categories", async (DatabaseContext context, string? search, ushort page=1, ushort pagesize=10) =>
{
    var retval = await context.Categories.Where(w=>search==null || w.Name.Contains(search)).Skip( (page-1)*pagesize ).Take(pagesize).ToListAsync();
    var totalCount = await context.Categories.Where(w=>search==null || w.Name.Contains(search)).CountAsync();
    return Results.Ok(new PaginatedResultDto<Category>(retval,totalCount));
})
.WithOpenApi();

app.MapGet("categories/{id}", async (DatabaseContext context, int id) =>
{
    var retval = await context.Categories.FirstOrDefaultAsync(q=>q.Id==id);
    if( retval==null) return Results.NotFound();
    return Results.Ok(retval);
})
.WithOpenApi();

app.MapGet("categories/{id}/products", async (DatabaseContext context, string? search, int id,ushort page=1, ushort pagesize=10) =>
{
    if(page<=1)page=1;
    if(pagesize<=0) pagesize=10;

    var retval = await context.Products.Include(i=>i.Category).Where(w=>search==null || w.Description.Contains(search) || w.Title.Contains(search)).Skip( (page-1)*pagesize ).Take(pagesize).ToListAsync();
    var totalCount = await context.Products.Where(w=>search==null || w.Description.Contains(search) || w.Title.Contains(search)).CountAsync();
    if( retval.Count==0 ) return Results.NotFound();

    return Results.Ok(new PaginatedResultDto<Product>(retval,totalCount));
})
.WithOpenApi();

app.MapGet("products", async (DatabaseContext context, string? search, ushort page=1, ushort pagesize=10) =>
{
    if(page<=1)page=1;
    if(pagesize<=0) pagesize=10;

    var retval = await context.Products.Where(w=>search==null || w.Description.Contains(search) || w.Title.Contains(search)).Include(i=>i.Category).Skip( (page-1)*pagesize ).Take(pagesize).ToListAsync();
    var totalCount = await context.Products.Where(w=>search==null || w.Description.Contains(search) || w.Title.Contains(search)).CountAsync();
    return Results.Ok(new PaginatedResultDto<Product>(retval,totalCount));
})
.WithOpenApi();

app.MapGet("products/{id}", async (DatabaseContext context, int id) =>
{
    var retval = await context.Products.Include(i=>i.Category).FirstOrDefaultAsync(q=>q.Id==id);
    if( retval==null ) return Results.NotFound();
    return Results.Ok(retval);
})
.WithOpenApi();

app.MapGet("orders", async (DatabaseContext context, IMapper mapper, ushort page=1, ushort pagesize=10) =>
{
    if(page<=1)page=1;
    if(pagesize<=0) pagesize=10;

    var retval = await context.Orders.ProjectTo<OrderDto>(mapper.ConfigurationProvider).Skip( (page-1)*pagesize ).Take(pagesize).ToListAsync();
    var totalCount = await context.Orders.CountAsync();
    return Results.Ok(new PaginatedResultDto<OrderDto>(retval,totalCount));
})
.WithOpenApi();

app.MapGet("orders/{id}", async (DatabaseContext context, IMapper mapper,int id) =>
{
    var retval = await context.Orders.ProjectTo<OrderDto>(mapper.ConfigurationProvider).FirstOrDefaultAsync(q=>q.Id==id);
    if( retval==null ) return Results.NotFound();
    return Results.Ok(retval);
})
.WithOpenApi();

app.MapPost("orders",async (DatabaseContext context, IMapper mapper,[FromBody]OrderDto newOrder) =>
{
    try
    {
        var candidate = mapper.Map<Order>(newOrder);

        if(candidate==null) return Results.BadRequest("Dati mancanti");
        if( candidate.Details==null || candidate.Details.Count()==0) return Results.BadRequest("Prodotti mancanti");
        if( candidate.Details.Where(q=>q.Quantity<=0).Count()>0) return Results.BadRequest("Prodotti con quantità mancanti");
        
        double totalPriceCheck = 0;
        foreach(var detail in candidate.Details)
        {
            var product = await context.Products.FirstOrDefaultAsync(q=>q.Id == detail.IdProduct);
            if( product==null) return Results.BadRequest("Prodotto inesistente");
            totalPriceCheck += product.Price*detail.Quantity;
        }
        if(totalPriceCheck!=candidate.TotalPrice) return Results.BadRequest("Totale non corretto");

        if( newOrder.Payment==null) return Results.BadRequest("Metodo di pagamento mancante");
        if( newOrder.Payment.Cvv <=100 || newOrder.Payment.Cvv>999 ) return Results.BadRequest("CVV Errato");
        if( newOrder.Payment.Number.Length != 16 || !long.TryParse(  newOrder.Payment.Number,out long foo) ) return Results.BadRequest("Numero Carta Errato");
        if( string.IsNullOrEmpty(newOrder.Payment.OwnerName) || string.IsNullOrWhiteSpace(newOrder.Payment.OwnerName)) return Results.BadRequest("Intestatrio Mancante");

        if( newOrder.Payment.Expire.Length != 4 || !int.TryParse(newOrder.Payment.Expire,out int foo2) ) return Results.BadRequest("Scadenza Errata");
        int meseScadenza = int.Parse(newOrder.Payment.Expire.Substring(0,2));
        int annoScadenza = 2000 + int.Parse(newOrder.Payment.Expire.Substring(2,2));
        int annoAttuale = DateTime.Today.Year;
        int meseAttuale = DateTime.Today.Month;
        if( annoScadenza < annoAttuale || (annoScadenza==annoAttuale && meseScadenza<meseAttuale) ) return Results.BadRequest("Scadenza Errata");

        context.Orders.Add(candidate);
        await context.SaveChangesAsync();
        return Results.Ok();
    }
    catch(Exception e)
    {
        return Results.Problem();
    }
})
.WithOpenApi();

app.Run();
