using System.Text.Json.Serialization;
using MediatR;

namespace Terminal.Backend.Application.Commands.Project.Update;

public sealed record UpdateProjectCommand([property: JsonIgnore] Guid Id, string Name) : IRequest;