using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Abstractions;

public interface IPasswordHasher
{
    Password Hash(string password);

    bool Verify(string input, Password password);
}