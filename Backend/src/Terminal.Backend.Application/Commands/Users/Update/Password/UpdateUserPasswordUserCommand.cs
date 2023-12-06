using System.Text.Json.Serialization;
using MediatR;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Commands.Users.Update.Password;

public sealed record UpdateUserPasswordUserCommand(
    [property: JsonIgnore] UserId Id,
    string OldPassword,
    string NewPassword)
    : IRequest;