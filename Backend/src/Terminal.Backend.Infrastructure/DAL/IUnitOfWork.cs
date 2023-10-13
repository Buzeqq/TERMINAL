using MediatR;

namespace Terminal.Backend.Infrastructure.DAL;

public interface IUnitOfWork<TResponse>
{
    Task<TResponse> ExecuteAsync(RequestHandlerDelegate<TResponse> next);
}