using Terminal.Backend.Application.DTO.Projects;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Projects.Get;

public record GetProjectQuery(ProjectId Id) : IRequest<GetProjectDto?>;
