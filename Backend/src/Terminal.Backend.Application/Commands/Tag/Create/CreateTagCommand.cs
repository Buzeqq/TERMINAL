using MediatR;

namespace Terminal.Backend.Application.Commands.Tag.Create;

public sealed record CreateTagCommand(
    [property: System.Text.Json.Serialization.JsonIgnore] Guid Id, 
    string Name) : IRequest;