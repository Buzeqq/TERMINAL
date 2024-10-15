using System.ComponentModel.DataAnnotations;

namespace Terminal.Backend.Infrastructure.Identity.Mails;

internal sealed class EmailSenderOptions
{
    [Required] public string BaseAddress { get; init; } = string.Empty;
    [Required] public string From { get; init; } = string.Empty;
    [Required] public string Token { get; init; } = string.Empty;
}
