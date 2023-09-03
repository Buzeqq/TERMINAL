using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Application.DTO;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.Exceptions;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Commands.Handlers;

internal sealed class CreateMeasurementCommandHandler : ICommandHandler<CreateMeasurementCommand>
{
    private readonly IStepsRepository _stepsRepository;
    private readonly IConvertDtoService _convertService;
    private readonly IRecipeRepository _recipeRepository;
    private readonly IMeasurementRepository _measurementRepository;

    public CreateMeasurementCommandHandler(IStepsRepository stepsRepository, IConvertDtoService convertService, IRecipeRepository recipeRepository, IMeasurementRepository measurementRepository)
    {
        _stepsRepository = stepsRepository;
        _convertService = convertService;
        _recipeRepository = recipeRepository;
        _measurementRepository = measurementRepository;
    }

    public async Task HandleAsync(CreateMeasurementCommand command, CancellationToken ct)
    {
        var (recipeId, stepsDto, tagsDto, comment) = command;

        var isAmbiguous = (recipeId is null && stepsDto is null) ||
                          (recipeId is not null && stepsDto is not null);
        if (isAmbiguous)
        {
            throw new AmbiguousCreateMeasurementRequestException(recipeId, stepsDto);
        }

        IEnumerable<Step> steps;
        Recipe? recipe = null;
        if (recipeId is null)
        {
            steps = await _convertService.ConvertAsync(stepsDto!, ct);
        }
        else
        {
            steps = await _stepsRepository.GetFromRecipeAsync(recipeId, ct);
            recipe = await _recipeRepository.GetAsync(recipeId, ct);
        }


        var tags = await _convertService.ConvertAsync(tagsDto, ct);
        var measurement = new Measurement(recipe, new Comment(comment), steps.ToList(), tags.ToList());
        await _measurementRepository.AddAsync(measurement, ct);
    }
}

public interface IMeasurementRepository
{
    Task AddAsync(Measurement measurement, CancellationToken ct);
}

public interface IRecipeRepository
{
    Task<Recipe?> GetAsync(RecipeId recipeId, CancellationToken ct);
}

public interface IConvertDtoService
{
    Task<IEnumerable<Step>> ConvertAsync(IEnumerable<CreateMeasurementStepDto> stepsDto, CancellationToken ct);
    Task<IEnumerable<Tag>> ConvertAsync(IEnumerable<string> tagNames, CancellationToken ct);
}

public interface IStepsRepository
{
    Task<IEnumerable<Step>> GetFromRecipeAsync(RecipeId id, CancellationToken ct);
}

public sealed class AmbiguousCreateMeasurementRequestException : TerminalException
{
    public AmbiguousCreateMeasurementRequestException(RecipeId? id, IEnumerable<CreateMeasurementStepDto>? steps) 
        : base($"Ambiguous create measurement request: recipe - {id}, steps - {steps}.")
    {
    }
}