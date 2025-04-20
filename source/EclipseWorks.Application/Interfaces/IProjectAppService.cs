using System;
using EclipseWorks.Application.Dtos.Request;
using EclipseWorks.Application.Dtos.Response;

namespace EclipseWorks.Application.Interfaces;

public interface IProjectAppService
{
    /// <summary>
    /// Obtem todos os projetos 
    /// </summary>
    /// <returns>Lista de Projetos</returns> 
    Task<IEnumerable<ProjectResponse>> GetAllAsync();

    /// <summary>
    /// Cria um novo projeto
    /// </summary>
    /// <param name="project">Projeto</param>
    Task<ProjectResponse> CreateAsync(CreateProjectRequest project);
}
