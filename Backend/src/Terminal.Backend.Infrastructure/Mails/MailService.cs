using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Core.Entities;

namespace Terminal.Backend.Infrastructure.Mails;

internal sealed class MailService : IMailService
{
    public Task SendInvitation(Invitation invitation)
    {
        return Task.CompletedTask;
    }
}