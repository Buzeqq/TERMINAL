using MediatR;

namespace Terminal.Backend.Infrastructure.DAL;

internal sealed class PostgresUnitOfWork<TResponse> : IUnitOfWork<TResponse>
{
    private readonly TerminalDbContext _dbContext;

    public PostgresUnitOfWork(TerminalDbContext dbContext)
        => _dbContext = dbContext;

    public async Task<TResponse> ExecuteAsync(RequestHandlerDelegate<TResponse> next)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {
            var response = await next();
            await _dbContext.SaveChangesAsync();
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