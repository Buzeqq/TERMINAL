using Terminal.Backend.Application.DTO;
using Terminal.Backend.Core.Exceptions;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Exceptions;

public sealed class AmbiguousCreateSampleRequestException : TerminalException
{
    public AmbiguousCreateSampleRequestException(RecipeId? id, IEnumerable<CreateSampleStepDto>? steps) 
        : base($"Ambiguous create sample request: recipe - {id}, steps - {steps}.")
    {
    }
}