using System.ComponentModel.DataAnnotations;

namespace Terminal.Backend.Application.Invitations;

internal sealed class InvitationOptions
{
    [Required] public required string LinkBase { get; set; }
}