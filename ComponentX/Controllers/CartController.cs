using AutoMapper;
using BackendXComponent.ComponentX.Domain.Services.Communication;
using BackendXComponent.ComponentX.Resources;
using BackendXComponent.ComponentX.Domain.Models;
using BackendXComponent.Shared.Extensions;

namespace BackendXComponent.ComponentX.Controllers;
using Microsoft.AspNetCore.Mvc;
[ApiController]
    [Route("api/v1/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ImplCartService _cartService; // Asegúrate de que el servicio se llame ICartService
        private readonly IMapper _mapper;

        public CartController(ImplCartService cartService, IMapper mapper)
        {
            _cartService = cartService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartResource>>> GetAllAsync()
        {
            var carts = await _cartService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Cart>, IEnumerable<CartResource>>(carts);
            return Ok(resources);
        }
        
        [HttpGet]
        [Route("GetCartByUserId/{userId}")]
        public async Task<ActionResult<IEnumerable<CartResource>>> GetCartByUserId(int userId)
        {
            var carts = await _cartService.GetCartByUserId(userId);
            var resources = _mapper.Map<IEnumerable<Cart>, IEnumerable<CartResource>>(carts);
            return Ok(resources);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveCartResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var cart = _mapper.Map<SaveCartResource, Cart>(resource);
            
            //cart.TotalPrice = cart.Quantity * cart.SubproductId.Price;
            
            var result = await _cartService.SaveAsync(cart);

            if (!result.Success)
                return BadRequest(result.Message);

            var cartResource = _mapper.Map<Cart, CartResource>(result.Resource);
            return Ok(cartResource);
        }

       
        
        
        
        //Actualizar cantidad y total price
        [HttpPut]
        [Route("UpdateQuantityAndPrice/{id}")]
        public async Task<IActionResult> UpdateQuantityAndPrice(int id, [FromBody] UpdateQuantityAndPriceResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            
            //Devolver un objeto cartResponse
            
            
            

            //var cart = _mapper.Map<UpdateQuantityAndPriceResource, Cart>(resource);
            var result = await _cartService.UpdateAsync(id, resource);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        
        }

        

        [HttpDelete]
        [Route("DeleteProducCart/{id}")]

        public async Task<IActionResult> DeleteAsync(int id, [FromBody] DeleteFromCarritoResource resource)
        {
            var result = await _cartService.DeleteAsync(id, resource);

            if (!result.Success)
                return BadRequest(result.Message);

            var cartResource = _mapper.Map<Cart, CartResource>(result.Resource);
            return Ok(cartResource);
        }
    }