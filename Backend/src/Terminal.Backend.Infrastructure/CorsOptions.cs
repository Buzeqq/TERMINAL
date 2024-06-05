using System.ComponentModel.DataAnnotations;

namespace Terminal.Backend.Infrastructure;

public class CorsOptions
{
    [Required]
    [MinLength(1)]
    public string[] AllowedOrigins { get; init; } = [];
}
