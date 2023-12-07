using System.Text.Json.Serialization;
using MediatR;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Commands.Users.Update.Email;

public sealed record UpdateUserEmailCommand([property: JsonIgnore] UserId Id, string Email) : IRequest;