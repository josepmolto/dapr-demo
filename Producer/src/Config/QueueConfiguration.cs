namespace Producer.Config;

public record QueueConfiguration
{
    public string TopicName { get; init; } = default!;
    public string PubSubName { get; init; } = default!;
}