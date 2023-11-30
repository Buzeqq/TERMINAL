using Terminal.Backend.Application.Abstractions;
using Terminal.Backend.Application.DTO.ParameterValues;
using Terminal.Backend.Application.DTO.Samples;
using Terminal.Backend.Application.Exceptions;
using Terminal.Backend.Core.Abstractions.Repositories;
using Terminal.Backend.Core.Entities;
using Terminal.Backend.Core.Entities.Parameters;
using Terminal.Backend.Core.Entities.ParameterValues;
using Terminal.Backend.Core.Exceptions;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Services;

internal sealed class ConvertDtoService : IConvertDtoService
{
    private readonly IParameterRepository _parameterRepository;
    private readonly ITagRepository _tagRepository;

    public ConvertDtoService(IParameterRepository parameterRepository, ITagRepository tagRepository)
    {
        _parameterRepository = parameterRepository;
        _tagRepository = tagRepository;
    }

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
                ParameterValue parameter = parameterDto switch
                {
                    CreateSampleDecimalParameterValueDto @decimal 
                        => new DecimalParameterValue(id, await _parameterRepository.GetAsync<DecimalParameter>(@decimal.Id, ct) 
                                                     ?? throw new ParameterNotFoundException(@decimal.Id), @decimal.Value),
                    CreateSampleIntegerParameterValueDto integer 
                        => new IntegerParameterValue(id, await _parameterRepository.GetAsync<IntegerParameter>(integer.Id, ct) 
                                                         ?? throw new ParameterNotFoundException(integer.Id), integer.Value),
                    CreateSampleTextParameterValueDto text 
                        => new TextParameterValue(id, await _parameterRepository.GetAsync<TextParameter>(text.Id, ct) 
                                                      ?? throw new ParameterNotFoundException(text.Id), text.Value),
                    _ => throw new UnknownParameterTypeException(parameterDto)
                };
                
                parameters.Add(parameter);
            }
            
            steps.Add(new SampleStep(StepId.Create(), new Comment(stepDto.Comment), parameters));
        }
        
        return steps;
    }

    public Task<IEnumerable<Tag>> ConvertAsync(IEnumerable<TagId> tagIds, CancellationToken ct)
        => _tagRepository.GetManyAsync(tagIds, ct);
}