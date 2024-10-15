using MediatR;

namespace Terminal.Backend.Infrastructure.DAL.Behaviours;

public sealed class UnitOfWorkBehaviour<TRequest, TResponse>(IUnitOfWork<TResponse> unitOfWork)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken) => !IsCommand() ? next() : unitOfWork.ExecuteAsync(next);

    private static bool IsCommand() => typeof(TRequest).Name.EndsWith("Command", StringComparison.InvariantCultureIgnoreCase);
}
