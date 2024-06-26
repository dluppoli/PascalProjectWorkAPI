﻿namespace ProjectWorkApi;

public class OrderDto
{
    
    public int Id { get; set; }
    
    public DateTime CreateDate { get; set; }

    public required string ClientName { get; set; }
    public required string Address { get; set; }
    
    public required string Email { get; set; }
    
    public required double TotalPrice { get; set; }

    public required CreditCardDto Payment { get; set; }

    public required List<OrderDetailDto> Details { get; set; }
}
