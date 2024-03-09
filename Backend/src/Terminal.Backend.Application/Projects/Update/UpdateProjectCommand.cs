using System.Text.Json.Serialization;
using MediatR;

namespace Terminal.Backend.Application.Projects.Update;

public sealed record UpdateProjectCommand([property: JsonIgnore] Guid Id, string Name) : IRequest;