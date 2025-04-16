using System;

namespace EclipseWorks.Application.Dtos.Response;

public class UserResponse
{
    public int Id { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public UserResponse()
    {
        Email = string.Empty;
    }
}
