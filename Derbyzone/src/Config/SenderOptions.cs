namespace Derbyzone.Config;
public record SenderOptions
{
    public string DaprSenderClientName { get; init; } = default!;
    public string DaprSenderStoreName { get; init; } = default!;
    public string ClientHost { get; init; } = default!;
}