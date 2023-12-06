using System.Text.Json.Serialization;
using MediatR;

namespace Terminal.Backend.Application.Commands.Project.Create;

public sealed record CreateProjectCommand([property: JsonIgnore] Guid Id, string Name) : IRequest;