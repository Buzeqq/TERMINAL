using Terminal.Backend.Application.DTO;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.DAL.Handlers;

public static class Extensions
{
    public static GetProjectsDto AsDto(this Project entity)
        => new()
        {
            Id = entity.Id,
            Name = entity.Name
        };

    public static GetTagsDto AsDto(this Tag entity)
        => new()
        {
            Name = entity.Name
        };
}