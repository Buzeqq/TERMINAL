using System.Text.Json.Serialization;
using MediatR;

namespace Terminal.Backend.Application.Projects.Create;

public sealed record CreateProjectCommand([property: JsonIgnore] Guid Id, string Name) : IRequest;