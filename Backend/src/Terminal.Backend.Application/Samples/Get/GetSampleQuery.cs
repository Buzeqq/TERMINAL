using Terminal.Backend.Application.DTO.Samples;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Samples.Get;

public record GetSampleQuery(SampleId Id) : IRequest<GetSampleDto?>;
