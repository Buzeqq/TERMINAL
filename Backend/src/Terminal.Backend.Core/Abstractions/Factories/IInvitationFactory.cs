using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Core.Abstractions.Factories;

public interface IInvitationFactory
{
    Invitation Crate(User user);
}