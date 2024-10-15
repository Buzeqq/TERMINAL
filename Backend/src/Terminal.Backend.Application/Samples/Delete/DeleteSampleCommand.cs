using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Samples.Delete;

public record DeleteSampleCommand(SampleId Id) : IRequest;
