using System.Text.Json.Serialization;

namespace Book.Dto;
public abstract class Response
{
    private Response() { }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Status Status { get; init; }
    public class Success : Response
    {
        public Success()
        {
            Status = Status.OK;
        }
    }

    public class Error : Response
    {
        public Error()
        {
            Status = Status.Error;
        }

        public string Code { get; set; } = default!;
        public string ErrorMessage { get; set; } = default!;
    }

}
public enum Status
{
    OK,
    Error
}