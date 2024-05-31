namespace ProjectWorkApi;

public class OrderDto
{
    
    public int Id { get; set; }
    public string ClientName { get; set; }
    public string Address { get; set; }
    public double TotalPrice { get; set; }

    public List<OrderDetailDto> Details { get; set; }
}
