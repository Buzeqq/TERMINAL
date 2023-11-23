using MediatR;
using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Application.Exceptions;
using Terminal.Backend.Core.Abstractions.Repositories;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.Exceptions;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Commands.Measurement.Create;

internal sealed class CreateMeasurementCommandHandler : IRequestHandler<CreateMeasurementCommand>
{
    private readonly IStepsRepository _stepsRepository;
    private readonly IConvertDtoService _convertService;
    private readonly IRecipeRepository _recipeRepository;
    private readonly IMeasurementRepository _measurementRepository;
    private readonly IProjectRepository _projectRepository;

    public CreateMeasurementCommandHandler(IStepsRepository stepsRepository, IConvertDtoService convertService, IRecipeRepository recipeRepository, IMeasurementRepository measurementRepository, IProjectRepository projectRepository)
    {
        _stepsRepository = stepsRepository;
        _convertService = convertService;
        _recipeRepository = recipeRepository;
        _measurementRepository = measurementRepository;
        _projectRepository = projectRepository;
    }

    public async Task Handle(CreateMeasurementCommand command, CancellationToken ct)
    {
        var (measurementId, projectId, recipeId, stepsDto, tagsDto, comment) = command;

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
            // create new steps
            steps = await _convertService.ConvertAsync(stepsDto!, ct);
        }
        else
        {
            // retrieve steps from database
            steps = await _stepsRepository.GetFromRecipeAsync(recipeId, ct);
            recipe = await _recipeRepository.GetAsync(recipeId, ct);
        }

        var tags = await _convertService.ConvertAsync(tagsDto.Select(t => new TagId(t)), ct);
        var project = await _projectRepository.GetAsync(projectId, ct) ?? throw new ProjectNotFoundException(projectId);
        var measurement = new Core.Entities.Measurement(measurementId, project, recipe, new Comment(comment), steps.ToList(), tags.ToList());
        project.Measurements.Add(measurement);
        await _measurementRepository.AddAsync(measurement, ct);
    }
}