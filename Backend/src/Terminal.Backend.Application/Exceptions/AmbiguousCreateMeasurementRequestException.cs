using Terminal.Backend.Application.DTO;
using Terminal.Backend.Core.Exceptions;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Exceptions;

public sealed class AmbiguousCreateMeasurementRequestException : TerminalException
{
    public AmbiguousCreateMeasurementRequestException(RecipeId? id, IEnumerable<CreateMeasurementStepDto>? steps) 
        : base($"Ambiguous create measurement request: recipe - {id}, steps - {steps}.")
    {
    }
}