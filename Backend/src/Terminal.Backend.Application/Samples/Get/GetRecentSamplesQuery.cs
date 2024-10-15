using Terminal.Backend.Application.DTO.Samples;

namespace Terminal.Backend.Application.Samples.Get;

public record GetRecentSamplesQuery(int Length) : IRequest<GetRecentSamplesDto>;
