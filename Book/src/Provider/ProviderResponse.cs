namespace Book.Provider;
public record ProviderResponse
{
    public int Code { get; set; }
    public string ErrorMessage { get; set; } = default!;
}