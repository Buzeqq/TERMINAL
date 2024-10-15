using Terminal.Backend.Application.DTO.Samples;
using Terminal.Backend.Core.Exceptions;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Exceptions;

public sealed class AmbiguousCreateSampleRequestException(RecipeId? id, IEnumerable<CreateSampleStepDto>? steps)
    : TerminalException($"Ambiguous create sample request: recipe - {id}, steps - {steps}.");
