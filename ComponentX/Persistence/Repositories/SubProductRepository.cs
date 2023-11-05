//Agregamos Microsdk de EntityFrameworkCore

using BackendXComponent.ComponentX.Domain.Models;
using BackendXComponent.ComponentX.Domain.Repositories;
using BackendXComponent.Shared.Persistence.Contexts;
using BackendXComponent.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BackendXComponent.ComponentX.Persistence.Repositories;

public class SubProductRepository: BaseRepository, ImplSubProductRepository
{
    //Agregamos el constructor
    public SubProductRepository(AppDbContext context) : base(context)
    {
    }
    
    //Agregamos el metodo GetAll
    public async Task<IEnumerable<SubProduct>> ListAsync()
    {
        //se hace el include y otras coass por la relacion de la tabla , ya que tiene el id de la tabla product
        return await _context.SubProducts
            //.Include(p =>p.Product)
            .ToListAsync();
    }
    
    //Agregamos el metodo AddAsync
    public async Task AddAsync(SubProduct subProduct)
    {
        await _context.SubProducts.AddAsync(subProduct);
    }
    
    //Agregamos el metodo GetById
    public async Task<SubProduct> FindByIdAsync(int id)
    {
        return await _context.SubProducts
            .Include(p => p.Product)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
    
    
    //Agregamos el metodo FindByProductIdAsync
    public async Task<IEnumerable<SubProduct>> FindByProductIdAsync(int productId)
    {
        return await _context.SubProducts
            .Where(p => p.ProductId == productId)
            .Include(p => p.Product)
            .ToListAsync();
    }//aca es IEnumerable porque puede haber mas de un subproducto con el mismo nombre,
    //en cambio en el metodo FindByIdAsync no puede haber mas de un producto con el mismo id
    //porque se usa el firstordefault que solo devuelve un elemento, el primero encontrado
    
    
    
    //Agregamos el metodo FindByNameAsync
    public async Task<IEnumerable<SubProduct>> FindByNameAsync(string name)
    {
        return await _context.SubProducts
            .Where(p => p.Name == name)
            .Include(p => p.Product)
            .ToListAsync();
    }
    
    //Agregamos el metodo FindByNameOneAsync
    public async Task<SubProduct> FindByNameOneAsync(string name)
    {
        return await _context.SubProducts
            .Include(p => p.Product)
            .FirstOrDefaultAsync(p=> p.Name == name);
    }
    
    //Agregamos el metodo Update
    public void Update(SubProduct subProduct)
    {
        _context.SubProducts.Update(subProduct);
    }
    //Agregamos el metodo Delete
    public void Delete(SubProduct subProduct)
    {
        _context.SubProducts.Remove(subProduct);
    }
    
    
}