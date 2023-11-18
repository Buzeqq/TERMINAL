using MediatR;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Commands.Tag.ChangeStatus;

public sealed record ChangeTagStatusCommand(TagId Id, bool IsActive) : IRequest;