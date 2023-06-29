using Microsoft.EntityFrameworkCore;

namespace Terminal.Poc;

public class MeasurementRepository : IRepository<Measurement>
{
    private readonly MyDbContext _dbContext;

    public MeasurementRepository(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Measurement?> FindAsync(Guid id)
    {
        return await _dbContext.Measurements.FindAsync(id);
    }

    public async Task<IEnumerable<Measurement>> GetAllAsync()
    {
        return await _dbContext.Measurements.ToListAsync();
    }

    public async Task CreateAsync(Measurement entity)
    {
        await _dbContext.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Measurement entity)
    {
        _dbContext.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Measurement entity)
    {
        _dbContext.Update(entity);
        await _dbContext.SaveChangesAsync();
    }
}

public interface IRepository<T>
{
    Task<T?> FindAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task CreateAsync(T entity);
    Task DeleteAsync(T entity);
    Task UpdateAsync(T entity);
}