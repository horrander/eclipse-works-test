namespace EclipseWorks.Domain.Exceptions;

public class ProjectExceptions : BusinessException
{
    public ProjectExceptions(BusinessError error) : base(error) { }

    public static readonly BusinessError ProjectWithoutTaskError =
        new("ProjectWithoutTaskError", "Projeto não possui taks associadas");

    public static readonly BusinessError ProjectToManyTasksError =
        new("ProjectToManyTasksError", "Projeto deve possui no máximo 20 Tarefas");

    public static readonly BusinessError ProjectIncorrectUserError =
        new("ProjectIncorrectUserError", "Erro ao associar usário informado ao projeto");

    public static readonly BusinessError ProjectNotFoundError =
        new("ProjectNotFoundError", "Projeto não encontrado");

    public static readonly BusinessError ProjectNotFoundForUserError =
        new("ProjectNotFoundForUserError", "Nenhum projeto encontrado para o usuário informado");
}
