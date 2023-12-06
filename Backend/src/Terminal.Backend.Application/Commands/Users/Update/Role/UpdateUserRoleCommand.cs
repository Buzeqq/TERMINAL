using System.Text.Json.Serialization;
using MediatR;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Commands.Users.Update.Role;

public sealed record UpdateUserRoleCommand([property: JsonIgnore] UserId Id, string Role) : IRequest;