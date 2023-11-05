using BackendXComponent.ComponentX.Domain.Models;
using BackendXComponent.ComponentX.Domain.Repositories;
using BackendXComponent.ComponentX.Domain.Services.Communication;
using BackendXComponent.ComponentX.Domain.Services.Communication.Communication;
using BackendXComponent.ComponentX.Resources;

namespace BackendXComponent.ComponentX.Services;

public class CartService: ImplCartService
{
    public readonly ImplCartRepository _cartRepository;
    public readonly ImplUnitOfWork _unitOfWork;
    
    public CartService(ImplCartRepository CartRepository, ImplUnitOfWork unitOfWork)
    {
        _cartRepository = CartRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<Cart>> ListAsync()
    {
      return await _cartRepository.ListAsync();
      
    }
    
    public async Task<IEnumerable<Cart>> GetCartByUserId(int userId)
    {
        return await _cartRepository.GetCartByUserId(userId);
    }
    
    
    public async Task<CartResponse> UpdateAsync(int id, UpdateQuantityAndPriceResource cart)
    {
        var cartByUserId = await _cartRepository.GetCartByUserId(id);
        if (cartByUserId== null)
            return new CartResponse("Cart not found.");
        
        //Buscar solo el ID del producto que coincide con el ID del producto que se envia en el body de la peticion
        var cartProduct = cartByUserId.Where(c => c.ProductID == cart.ProductId).FirstOrDefault();
            
        //Si el producto no existe en el carrito, se manda un mensaje de error
        if (cartProduct == null)
            return new CartResponse("Product not found in the cart for this user.");
        
        //Actualizar cantidad y total price
        cartProduct.Quantity = cart.Quantity;
        cartProduct.TotalPrice = cart.TotalPrice ;
        
        try
        {
            _cartRepository.Update(cartProduct);
            await _unitOfWork.CompleteAsync();
            return new CartResponse(cartProduct);
        }
        catch (Exception e)
        {
            return new CartResponse($"An error occurred when updating the cart: {e.Message}");
        }
    }

    public async Task<CartResponse> SaveAsync(Cart cart)
    {
        //Verificar si el usuario ya tiene un carrito de compras
        var cartByUserId = await _cartRepository.GetCartByUserId(cart.UserId);
        if (cartByUserId == null)
            return new CartResponse("Cart not found.");
        
        //Buscar solo el ID del producto que coincide con el ID del producto que se envia en el body de la peticion
        var cartProduct = cartByUserId.Where(c => c.ProductID == cart.ProductID).FirstOrDefault();
        if (cartProduct != null)
        {
            //Si el producto ya existe en el carrito, se modifica la cantidad y el total price
            cartProduct.Quantity = cart.Quantity;
            cartProduct.TotalPrice = cart.TotalPrice ;
            //guardar cambios
            try
            {
                _cartRepository.Update(cartProduct);
                await _unitOfWork.CompleteAsync();
                return new CartResponse(cartProduct);
            }
            catch (Exception e)
            {
                return new CartResponse($"An error occurred when updating the cart: {e.Message}");
            }
            
            
            
        }//Si el producto no existe en el carrito, se agrega
        
            try
            {
                await _cartRepository.AddAsync(cart);
                await _unitOfWork.CompleteAsync();
                return new CartResponse(cart);

            }
            catch (Exception e)
            {
                return new CartResponse($"An error occurred when saving the cart: {e.Message}");

            }
        
        
      
        
    }
    

 

    public async Task<CartResponse> DeleteAsync(int id, DeleteFromCarritoResource resource)
    { 
        //Cuando el usuario elimina el producto del carrito, osea su cantidad es 0, se elimina el registro del carrito
        //Tambien si el usuario compra el producto, se elimina el registro del carrito, lo cual pasa a ser una orden
    
        //Verificar si el usuario ya tiene un carrito de compras
        var cartByUserId = await _cartRepository.GetCartByUserId(id);
        if (cartByUserId == null)
            return new CartResponse("Cart not found.");
        
        //Buscar solo el ID del producto que coincide con el ID del producto que se envia en el body de la peticion
        var cartProduct = cartByUserId.Where(c => c.ProductID == resource.ProductId).FirstOrDefault();
        if (cartProduct == null)
            return new CartResponse("Product not found in the cart for this user.");
        
        //Si el producto existe en el carrito, se elimina
        try
        {
            _cartRepository.Remove(cartProduct);
            await _unitOfWork.CompleteAsync();
            return new CartResponse(cartProduct);
        }
        catch (Exception e)
        {
            return new CartResponse($"An error occurred when deleting the cart: {e.Message}");
        }
        
        var existingCart = await _cartRepository.FindByIdAsync(id);
        if (existingCart == null)
            return new CartResponse("Cart not found.");
        try
        {
            _cartRepository.Remove(existingCart);
            await _unitOfWork.CompleteAsync();
            return new CartResponse(existingCart);
        }
        catch (Exception e)
        {
            return new CartResponse($"An error occurred when deleting the category: {e.Message}");
        }
    }
    
}