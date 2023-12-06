using System.Text.Json.Serialization;
using MediatR;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Commands.Users.Update.Password.WithAdminPrivileges;

public sealed record UpdateUserPasswordAdministratorCommand(
    [property: JsonIgnore] UserId Id,
    string NewPassword)
    : IRequest;