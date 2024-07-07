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
        CancellationToken ct)
    {
        var steps = new List<SampleStep>();
        foreach (var stepDto in stepsDto)
        {
            var parameters = new List<ParameterValue>();
            foreach (var parameterDto in stepDto.Parameters)
            {
                var id = ParameterValueId.Create();
                var parameter = await GetParameterValueAsync(id, parameterDto, ct);
                parameters.Add(parameter);
            }

            steps.Add(new SampleStep(StepId.Create(), new Comment(stepDto.Comment), parameters));
        }

        return steps;
    }

    public async Task<IEnumerable<SampleStep>> ConvertAsync(IEnumerable<UpdateSampleStepDto> stepsDto,
        CancellationToken ct)
    {
        var steps = new List<SampleStep>();
        foreach (var stepDto in stepsDto)
        {
            var parameters = new List<ParameterValue>();
            foreach (var parameterDto in stepDto.Parameters)
            {
                var id = parameterDto.Id;
                var parameter = await GetParameterValueAsync(id, parameterDto, ct);
                parameters.Add(parameter);
            }

            steps.Add(new SampleStep(stepDto.Id, new Comment(stepDto.Comment), parameters));
        }

        return steps;
    }

    private async Task<ParameterValue> GetParameterValueAsync(Guid id, CreateSampleBaseParameterValueDto parameterDto,
        CancellationToken ct)
    {
        ParameterValue parameter = parameterDto switch
        {
            CreateSampleDecimalParameterValueDto @decimal
                => new DecimalParameterValue(id, await parameterRepository.GetAsync<DecimalParameter>(@decimal.Id, ct)
                                                 ?? throw new ParameterNotFoundException(), @decimal.Value),
            CreateSampleIntegerParameterValueDto integer
                => new IntegerParameterValue(id, await parameterRepository.GetAsync<IntegerParameter>(integer.Id, ct)
                                                 ?? throw new ParameterNotFoundException(), integer.Value),
            CreateSampleTextParameterValueDto text
                => new TextParameterValue(id, await parameterRepository.GetAsync<TextParameter>(text.Id, ct)
                                              ?? throw new ParameterNotFoundException(), text.Value),
            _ => throw new UnknownParameterTypeException(parameterDto)
        };
        return parameter;
    }

    public Task<IEnumerable<Tag>> ConvertAsync(IEnumerable<TagId> tagIds, CancellationToken ct)
        => tagRepository.GetManyAsync(tagIds, ct);
}
