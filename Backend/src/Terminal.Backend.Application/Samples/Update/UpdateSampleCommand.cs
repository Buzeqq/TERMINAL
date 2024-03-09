using System.Text.Json.Serialization;
using MediatR;
using Terminal.Backend.Application.DTO.Samples;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Samples.Update;

public sealed record UpdateSampleCommand(
    [property: JsonIgnore] SampleId Id,
    Guid ProjectId,
    Guid? RecipeId,
    IEnumerable<UpdateSampleStepDto> Steps,
    IEnumerable<Guid> TagIds,
    string Comment) : IRequest;