namespace EclipseWorks.Domain.Exceptions;

public class TaskCommentExceptions : BusinessException
{
    public TaskCommentExceptions(BusinessError error) : base(error)
    {
    }

    public static readonly BusinessError CommentInvalidError =
        new("CommentInvalidError", "Comentário não informado");

    public static readonly BusinessError CommentInvalidUserError =
        new("CommentInvalidUserError", "Comentário não foi associado a um Usuário válido");

    public static readonly BusinessError CommentInvalidTaskItemError =
        new("CommentInvalidTaskItemError", "Comentário não foi associado a uma Task válida");
}
