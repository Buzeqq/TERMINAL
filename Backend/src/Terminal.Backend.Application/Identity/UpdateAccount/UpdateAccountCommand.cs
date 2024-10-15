using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Identity.UpdateAccount;

public record UpdateAccountCommand(Email? NewEmail, Password? NewPassword, Password? OldPassword) : IRequest;
