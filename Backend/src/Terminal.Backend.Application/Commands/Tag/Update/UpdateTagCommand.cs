using System.Text.Json.Serialization;
using MediatR;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Commands.Tag.Update;

public sealed record UpdateTagCommand([property: JsonIgnore] TagId Id, string Name) : IRequest;