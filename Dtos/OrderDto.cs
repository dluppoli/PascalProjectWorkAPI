namespace ProjectWorkApi;

public class OrderDto
{
    
    public int Id { get; set; }
    public required string ClientName { get; set; }
    public required string Address { get; set; }
    public double TotalPrice { get; set; }

    public required CreditCardDto Payment { get; set; }

    public required List<OrderDetailDto> Details { get; set; }
}
