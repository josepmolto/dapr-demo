namespace Derbyzone.Config;
public record RandomData
{
    public IEnumerable<string> Hotels { get; init; } = default!;
    public IEnumerable<string> RoomTypes { get; init; } = default!;
}