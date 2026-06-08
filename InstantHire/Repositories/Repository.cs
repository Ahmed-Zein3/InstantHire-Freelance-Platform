using Microsoft.EntityFrameworkCore;
using InstantHire.Data;

namespace InstantHire.Repositories;

public class Repository<T> where T : class
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    // =========================
    // ADD
    // =========================
    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    // =========================
    // GET BY ID
    // =========================
    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    // =========================
    // GET ALL
    // =========================
    public async Task<List<T>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    // =========================
    // DELETE
    // =========================
    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    // =========================
    // SAVE CHANGES
    // =========================
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}