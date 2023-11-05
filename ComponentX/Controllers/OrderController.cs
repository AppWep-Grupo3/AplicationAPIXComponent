using AutoMapper;
using BackendXComponent.ComponentX.Domain.Models;
using BackendXComponent.ComponentX.Domain.Services.Communication;
using BackendXComponent.ComponentX.Resources;
using BackendXComponent.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BackendXComponent.ComponentX.Controllers;

[ApiController]
[Route("api/v1/[controller]")]  
public class OrderController : ControllerBase
{
    private readonly ImplOrderService _orderService;
    private readonly IMapper _mapper;
    
    public OrderController(ImplOrderService orderService, IMapper mapper)
    {
    
        _orderService = orderService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<OrderResource>> GetAllAsync()
    {
        var orders = await _orderService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderResource>>(orders);
        return resources;
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveOrderResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest((ModelState.GetErrorMessages()));
        var order = _mapper.Map <SaveOrderResource, Order>(resource);
        var result = await _orderService.SaveAsync(order);
        
        if (!result.Success)
            return BadRequest(result.Message);
        var orderResource = _mapper.Map<Order, OrderResource>(result.Resource);
        return Ok(orderResource);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveOrderResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var order = _mapper.Map<SaveOrderResource, Order>(resource);
        var result = await _orderService.UpdateAsync(id, order);
        
        if (!result.Success)
            return BadRequest(result.Message);
        var orderResource = _mapper.Map<Order, OrderResource>(result.Resource);
        return Ok(orderResource);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _orderService.DeleteAsync(id);
        
        if (!result.Success)
            return BadRequest(result.Message);
        var orderResource = _mapper.Map<Order, OrderResource>(result.Resource);
        return Ok(orderResource);
    }
}
