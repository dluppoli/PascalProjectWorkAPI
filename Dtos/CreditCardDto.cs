public class CreditCardDto
{
    public required string Number { get; set; }
    public required string OwnerName { get; set; }
    public required string Expire { get; set; }
    public int Cvv { get; set; }
}