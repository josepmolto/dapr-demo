namespace Search.Dto;
public class Rate
{
    public string RoomType { get; set; } = default!;
    public decimal Cost { get; set; }
    public IEnumerable<CancellationPolicy> CancellationPolicies { get; set; } = default!;
    public string BookingKey { get; set; } = default!;
}