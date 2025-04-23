namespace EclipseWorks.Domain.Exceptions;

public class BusinessException : Exception
{
    public BusinessError BusinessError { get; set; }

    public BusinessException(BusinessError error)
    {
        BusinessError = new BusinessError(error.Key, error.Message);
    }

    public static readonly BusinessError EntityAlreadyRemovedError =
        new("EntityAlreadyRemovedError", "Entidade já foi removida anteriormente");
}
