namespace EclipseWorks.Domain.Exceptions;

public class BusinessError
{
    public string Key { get; set; }
    public string Message { get; set; }

    public BusinessError(string key, string message)
    {
        Key = key;
        Message = message;
    }
}
