using System.Text.Json.Serialization;
using MediatR;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Commands.Tag.Create;

public sealed record CreateTagCommand(
    [property: JsonIgnore] TagId Id,
    string Name) : IRequest;