using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Identity.Register;

public sealed record RegisterCommand(Email Email, Password Password, string RoleName) : IRequest;
