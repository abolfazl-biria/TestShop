using Application.Interfaces;
using Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using Persistence.Repositories;

namespace Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IDbContextTransaction? _transaction;

    public IProductRepository Products { get; }
    public IOrderRepository Orders { get; }
    public IOrderItemRepository OrderItems { get; }
    public ICustomerRepository Customers { get; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;

        Products = new ProductRepository(_context);
        Orders = new OrderRepository(_context);
        OrderItems = new OrderItemRepository(_context);
        Customers = new CustomerRepository(_context);
    }

    public async Task BeginTransactionAsync() => _transaction ??= await _context.Database.BeginTransactionAsync();

    public async Task CommitAsync()
    {
        if (_transaction != null)
        {
            await _context.SaveChangesAsync();
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task<bool> SaveChangesAsync() => await _context.SaveChangesAsync() > 0;

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
    }
}