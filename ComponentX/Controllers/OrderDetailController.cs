using AutoMapper;
using BackendXComponent.ComponentX.Domain.Models;
using BackendXComponent.ComponentX.Domain.Services.Communication;
using BackendXComponent.ComponentX.Resources;
using BackendXComponent.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackendXComponent.ComponentX.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class OrderDetailController : ControllerBase
{
    private readonly ImpOrderDetailService _orderDetailService;
    private readonly IMapper _mapper;

    public OrderDetailController(ImpOrderDetailService orderDetailService, IMapper mapper)
    {
        _orderDetailService = orderDetailService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<OrderDetailResource>> GetAllAsync()
    {
        var orderDetails = await _orderDetailService.ListAsync();
        var resources = _mapper.Map<IEnumerable<OrderDetail>, IEnumerable<OrderDetailResource>>(orderDetails);
        return resources;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveOrderDetailResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var orderDetail = _mapper.Map<SaveOrderDetailResource, OrderDetail>(resource);
        var result = await _orderDetailService.SaveAsync(orderDetail);

        if (!result.Success)
            return BadRequest(result.Message);

        var orderDetailResource = _mapper.Map<OrderDetail, OrderDetailResource>(result.Resource);
        return Ok(orderDetailResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveOrderDetailResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var orderDetail = _mapper.Map<SaveOrderDetailResource, OrderDetail>(resource);
        var result = await _orderDetailService.UpdateAsync(id, orderDetail);

        if (!result.Success)
            return BadRequest(result.Message);

        var orderDetailResource = _mapper.Map<OrderDetail, OrderDetailResource>(result.Resource);
        return Ok(orderDetailResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _orderDetailService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var orderDetailResource = _mapper.Map<OrderDetail, OrderDetailResource>(result.Resource);
        return Ok(orderDetailResource);
    }
}