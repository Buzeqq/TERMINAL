using Terminal.Backend.Application.DTO.ParameterValues;
using Terminal.Backend.Application.DTO.Samples;
using Terminal.Backend.Application.Exceptions;
using Terminal.Backend.Core.Abstractions.Repositories;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.Entities.Parameters;
using Terminal.Backend.Core.Entities.ParameterValues;
using Terminal.Backend.Core.Exceptions;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Common.Services;

internal sealed class ConvertDtoService(IParameterRepository parameterRepository, ITagRepository tagRepository)
    : IConvertDtoService
{
    public async Task<IEnumerable<SampleStep>> ConvertAsync(IEnumerable<CreateSampleStepDto> stepsDto,
        CancellationToken cancellationToken)
    {
        var steps = new List<SampleStep>();
        foreach (var stepDto in stepsDto)
        {
            var parameters = new List<ParameterValue>();
            foreach (var parameterDto in stepDto.Parameters)
            {
                var id = ParameterValueId.Create();
                var parameter = await GetParameterValueAsync(id, parameterDto, cancellationToken);
                parameters.Add(parameter);
            }

            steps.Add(new SampleStep(StepId.Create(), new Comment(stepDto.Comment), parameters));
        }

        return steps;
    }

    public async Task<IEnumerable<SampleStep>> ConvertAsync(IEnumerable<UpdateSampleStepDto> stepsDto,
        CancellationToken cancellationToken)
    {
        var steps = new List<SampleStep>();
        foreach (var stepDto in stepsDto)
        {
            var parameters = new List<ParameterValue>();
            foreach (var parameterDto in stepDto.Parameters)
            {
                var id = parameterDto.ParameterId;
                var parameter = await GetParameterValueAsync(id, parameterDto, cancellationToken);
                parameters.Add(parameter);
            }

            steps.Add(new SampleStep(stepDto.Id, new Comment(stepDto.Comment), parameters));
        }

        return steps;
    }

    private async Task<ParameterValue> GetParameterValueAsync(Guid id, StepParameterValueDto parameterDto,
        CancellationToken cancellationToken)
    {
        ParameterValue parameter = parameterDto switch
        {
            StepDecimalParameterValueDto @decimal
                => new DecimalParameterValue(id, await parameterRepository.GetAsync<DecimalParameter>(@decimal.ParameterId, cancellationToken)
                                                 ?? throw new ParameterNotFoundException(), @decimal.Value),
            StepIntegerParameterValueDto integer
                => new IntegerParameterValue(id, await parameterRepository.GetAsync<IntegerParameter>(integer.ParameterId, cancellationToken)
                                                 ?? throw new ParameterNotFoundException(), integer.Value),
            StepTextParameterValueDto text
                => new TextParameterValue(id, await parameterRepository.GetAsync<TextParameter>(text.ParameterId, cancellationToken)
                                              ?? throw new ParameterNotFoundException(), text.Value),
            _ => throw new UnknownParameterTypeException(parameterDto)
        };
        return parameter;
    }

    public Task<IEnumerable<Tag>> ConvertAsync(IEnumerable<TagId> tagIds, CancellationToken cancellationToken)
        => tagRepository.GetManyAsync(tagIds, cancellationToken);
}
