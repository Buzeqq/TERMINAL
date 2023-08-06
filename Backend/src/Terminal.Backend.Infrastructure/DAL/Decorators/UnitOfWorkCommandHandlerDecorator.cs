using MediatR;

namespace Terminal.Backend.Infrastructure.DAL.Decorators;

internal sealed class UnitOfWorkCommandHandlerDecorator<TCommand> : IRequestHandler<TCommand>
where TCommand : class, IRequest
{
    private readonly IRequestHandler<TCommand> _commandHandler;
    private readonly IUnitOfWork _unitOfWork;

    public UnitOfWorkCommandHandlerDecorator(IRequestHandler<TCommand> commandHandler, IUnitOfWork unitOfWork)
    {
        _commandHandler = commandHandler;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Handle(TCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.ExecuteAsync(() => _commandHandler.Handle(request, cancellationToken));
    }
}