using System.Text.Json.Serialization;

namespace Terminal.Backend.Application.Projects.Update;

public sealed record UpdateProjectCommand([property: JsonIgnore] Guid Id, string Name) : IRequest;