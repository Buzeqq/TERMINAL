using System.Text.Json.Serialization;

namespace Terminal.Backend.Application.Projects.Create;

public sealed record CreateProjectCommand([property: JsonIgnore] Guid Id, string Name) : IRequest;