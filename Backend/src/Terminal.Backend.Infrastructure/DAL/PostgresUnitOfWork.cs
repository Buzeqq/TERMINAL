using MediatR;

namespace Terminal.Backend.Infrastructure.DAL;

internal sealed class PostgresUnitOfWork<TResponse>(TerminalDbContext dbContext) : IUnitOfWork<TResponse>
{
    public async Task<TResponse> ExecuteAsync(RequestHandlerDelegate<TResponse> next)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync();

        try
        {
            var response = await next();
            await dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
            return response;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
