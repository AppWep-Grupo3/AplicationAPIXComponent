using BackendXComponent.ComponentX.Domain.Models;
using BackendXComponent.ComponentX.Domain.Repositories;
using BackendXComponent.Shared.Persistence.Contexts;
using BackendXComponent.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
namespace BackendXComponent.ComponentX.Persistence.Repositories;

//Agregamos el microsdk de EntityFrameworkCore

public class ProductRepository: BaseRepository, ImplProductRepository 
{
    //Agregamos el constructor
    public ProductRepository(AppDbContext context) : base(context)
    {
    }
    
    //Agregamos el metodo GetAll
    public async Task<IEnumerable<Product>> ListAsync()
    {
        return await _context.Products
                    .Include(p=>p.SubProductsList)
                    .ToListAsync();
    }
    
    //Agregamos el metodo AddAsync
    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
    }
    
    //Agregamos el metodo FindByNameAsync
    public async Task<Product> FindByNameAsync(string name)
    {
        return await _context.Products
            .FirstOrDefaultAsync(p => p.Name == name);
    }
    
    //Agregamos el metodo  FindByIdAsync
    public async Task<Product> FindByIdAsync(int id)
    {
        return await _context.Products
            .Include(p=>p.SubProductsList)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
    
    //Agregamos el metodo Update
    public void Update(Product product)
    {
        _context.Products.Update(product);
    }
    //Agregamos el metodo Remove
    public void Remove(Product product)
    {
        _context.Products.Remove(product);
    }
}
