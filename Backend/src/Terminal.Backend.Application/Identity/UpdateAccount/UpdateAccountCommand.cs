using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Identity.UpdateAccount;

public sealed record UpdateAccountCommand(Email? NewEmail, Password? NewPassword, Password? OldPassword) : IRequest;
