namespace Search.Dto;
public class Request
{
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public string HotelCode { get; set; } = default!;
}