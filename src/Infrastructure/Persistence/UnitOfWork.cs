using Application.Interfaces;
using Application.Interfaces.Repositories;
using Persistence.Repositories;

namespace Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public IProductRepository Products { get; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;

        Products = new ProductRepository(_context);
    }

    public async Task<bool> CommitAsync() => await _context.SaveChangesAsync() > 0;

    public void Dispose() => _context.Dispose();
}