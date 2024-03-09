using MediatR;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Samples.Delete;

public sealed record DeleteSampleCommand(SampleId Id) : IRequest;