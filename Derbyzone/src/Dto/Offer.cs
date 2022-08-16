namespace Derbyzone.Dto;
public record Offer
{
    public DateTime Date { get; init; }
    public string HotelCode { get; init; } = default!;
    public string RoomType { get; init; } = default!;
    public decimal Cost { get; init; }
    public IEnumerable<CancellationPolicy> CancellationPolicies { get; init; } = default!;
    public string Key { get; set; } = default!;
}