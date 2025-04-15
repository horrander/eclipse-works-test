using EclipseWorks.Domain.Enuns;

namespace EclipseWorks.Domain.Models;

public class Task : BaseModel
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTime DueDate { get; set; }
    public Enum Status { get; set; }

    public Task()
    {
        Title = "Nova Tarefa";
        Status = StatusEnum.Pending;
    }

    protected override void Validate()
    {
        throw new NotImplementedException();
    }
}
