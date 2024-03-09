using System.Text.Json.Serialization;
using MediatR;

namespace Terminal.Backend.Application.Projects.ChangeStatus;

public sealed record ChangeProjectStatusCommand([property: JsonIgnore] Guid ProjectId, bool IsActive) : IRequest;