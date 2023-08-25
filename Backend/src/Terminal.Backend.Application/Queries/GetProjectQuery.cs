using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Application.Queries;

public sealed class GetProjectQuery : IQuery<Project?>
{
    public Guid ProjectId { get; set; }
}