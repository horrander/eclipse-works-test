using System;

namespace EclipseWorks.Domain.Exceptions;

public class UserException : BusinessException
{
    public UserException(BusinessError error) : base(error) { }

    public static readonly BusinessError UserNotFoundError = new("UserNotFoundError", "Usuário não encontrado");
}
