using System.Text.Json.Serialization;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Tags.Create;

public sealed record CreateTagCommand(
    [property: JsonIgnore] TagId Id,
    string Name) : IRequest;