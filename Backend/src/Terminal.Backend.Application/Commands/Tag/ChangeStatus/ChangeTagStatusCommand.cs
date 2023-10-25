using MediatR;
using Terminal.Backend.Core.ValueObjects;

namespace Terminal.Backend.Application.Commands.Tag.ChangeStatus;

public sealed record ChangeTagStatusCommand(TagName Name, bool IsActive) : IRequest;