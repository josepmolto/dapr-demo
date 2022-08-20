namespace Derbyzone.Dto.Book;
public abstract class Response
{
    private Response() { }

    public class Success : Response
    {

    }

    public class Error : Response
    {
        public int Code { get; set; }
        public string ErrorMessage { get; set; } = default!;
    }
}