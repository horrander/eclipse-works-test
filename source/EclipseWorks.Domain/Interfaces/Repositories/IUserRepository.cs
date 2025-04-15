using System;
using EclipseWorks.Domain.Models;

namespace EclipseWorks.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    /// <summary>
    /// Get all non-removed Users
    /// </summary>
    /// <returns>List of user</returns> 
    IEnumerable<User> GetUsers();
}
