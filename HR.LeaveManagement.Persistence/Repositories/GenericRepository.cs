using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly LeaveManagementDbContext dbContext;

    public GenericRepository(LeaveManagementDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<T> Add(T entity)
    {
        await dbContext.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task Delete(T entity)
    {
        dbContext.Set<T>().Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task<bool> Exists(int id)
    {
        var entity = await Get(id);
        return entity != null;
    }

    public async Task<T> Get(int id)
    {
        return await dbContext.Set<T>().FindAsync(id);
    }

    public async Task<IReadOnlyList<T>> GetAll()
    {
        return dbContext.Set<T>().ToList();
    }

    public async Task Update(T entity)
    {
        dbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        await dbContext.SaveChangesAsync();
    }
}