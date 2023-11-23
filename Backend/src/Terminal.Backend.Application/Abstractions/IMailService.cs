using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Application.Abstractions;

public interface IMailService
{
    Task SendInvitation(Invitation invitation);
}