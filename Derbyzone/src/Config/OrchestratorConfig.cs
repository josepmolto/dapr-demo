namespace Derbyzone.Config;
public record OrchestratorConfig
{
    public int MaxDegreeOfParalelism { get; init; }
    public ulong OffersToGenerate { get; init; }
}