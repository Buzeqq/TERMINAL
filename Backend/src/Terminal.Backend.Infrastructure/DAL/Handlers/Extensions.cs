using Terminal.Backend.Application.DTO;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.DAL.Handlers;

public static class Extensions
{
    public static GetProjectsDto AsGetProjectsDto(this Project entity)
        => new()
        {
            Id = entity.Id,
            Name = entity.Name
        };

    public static GetProjectDto AsGetProjectDto(this Project entity)
        => new()
        {
            Id = entity.Id,
            Name = entity.Name,
            IsActive = entity.IsActive,
            Measurements = entity.Measurements
        };
}