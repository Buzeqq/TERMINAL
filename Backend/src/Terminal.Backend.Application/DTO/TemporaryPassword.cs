using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.DTO;

public record TemporaryPassword(string PasswordPlainText, Password PasswordHashed);