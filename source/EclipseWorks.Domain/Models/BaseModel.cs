using EclipseWorks.Domain.Exceptions;

namespace EclipseWorks.Domain.Models;

public abstract class BaseModel
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public DateTime? RemovedAt { get; private set; }

    public BaseModel()
    {
        CreatedAt = DateTime.Now;
    }

    public abstract void Validate();

    public void SetRemovedDate()
    {
        if (RemovedAt != null)
        {
            throw new BusinessException(BusinessException.EntityAlreadyRemovedError);
        }

        RemovedAt = DateTime.Now;
    }
}
