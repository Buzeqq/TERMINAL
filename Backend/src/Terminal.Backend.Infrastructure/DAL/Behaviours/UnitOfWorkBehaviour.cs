using MediatR;

namespace Terminal.Backend.Infrastructure.DAL.Behaviours;

public sealed class UnitOfWorkBehaviour<TRequest, TResponse>(IUnitOfWork<TResponse> unitOfWork)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public Task<TResponse> Handle(TRequest command, RequestHandlerDelegate<TResponse> next, CancellationToken ct) => !IsCommand() ? next() : unitOfWork.ExecuteAsync(next);

    private static bool IsCommand() => typeof(TRequest).Name.EndsWith("Command");
}