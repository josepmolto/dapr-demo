namespace Common.Dto.Derbyzone
{
    public record CancellationPolicy
    {
        public DateTime DateFrom { get; init; }
        public byte PenaltyPercentage { get; init; }
    }
}