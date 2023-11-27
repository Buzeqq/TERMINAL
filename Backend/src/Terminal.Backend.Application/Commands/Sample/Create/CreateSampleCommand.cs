using MediatR;
using Terminal.Backend.Application.DTO;

namespace Terminal.Backend.Application.Commands.Sample.Create;

public sealed record CreateSampleCommand(
    Guid SampleId,
    Guid ProjectId,
    Guid? RecipeId, 
    IEnumerable<CreateSampleStepDto>? Steps, 
    IEnumerable<Guid> TagIds, 
    string Comment) : IRequest;