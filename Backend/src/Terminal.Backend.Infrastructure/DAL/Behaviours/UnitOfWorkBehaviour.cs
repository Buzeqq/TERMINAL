using MediatR;

namespace Terminal.Backend.Infrastructure.DAL.Behaviours;

public sealed class UnitOfWorkBehaviour<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IUnitOfWork<TResponse> _unitOfWork;

    public UnitOfWorkBehaviour(IUnitOfWork<TResponse> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<TResponse> Handle(TRequest command, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
    {
        if (!IsCommand())
        {
            return next();
        }

        return _unitOfWork.ExecuteAsync(next);
    }

    private static bool IsCommand() => typeof(TRequest).Name.EndsWith("Command");
}