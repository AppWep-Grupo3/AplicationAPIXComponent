using AutoMapper;
using BackendXComponent.ComponentX.Domain.Models;
using BackendXComponent.ComponentX.Domain.Services.Communication;
using BackendXComponent.ComponentX.Resources;
using BackendXComponent.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BackendXComponent.ComponentX.Controllers;



[ApiController]
[Route("/api/[controller]")]

public class SubProductController: ControllerBase

{
    private readonly ImplSubProductService _subProductService;
    private readonly IMapper _mapper;
    
    public SubProductController(ImplSubProductService subProductService, IMapper mapper)
    {
        _subProductService = subProductService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<SubProductResource>> GetAllAsync()
    {
        var subProducts = await _subProductService.GetAllAsync();
        var resources = _mapper.Map<IEnumerable<SubProduct>, IEnumerable<SubProductResource>>(subProducts);
        return resources;
    }
    
    [HttpGet("{ProductId}")]
    public async Task<IEnumerable<SubProductResource>> GetByProductIdAsync(int ProductId)
    {
        var subProducts = await _subProductService.FindByProductIdAsync(ProductId);
        var resources = _mapper.Map<IEnumerable<SubProduct>, IEnumerable<SubProductResource>>(subProducts);
        return resources;
    }
    
    
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveSubProductResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var subProduct = _mapper.Map<SaveSubProductResource, SubProduct>(resource);
        var result = await _subProductService.AddAsync(subProduct);
        
        if (!result.Success)
            return BadRequest(result.Message);
        
        var subProductResource = _mapper.Map<SubProduct, SubProductResource>(result.Resource);
        return Ok(subProductResource);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveSubProductResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var subProduct = _mapper.Map<SaveSubProductResource, SubProduct>(resource);
        var result = await _subProductService.Update(id, subProduct);
        
        if (!result.Success)
            return BadRequest(result.Message);
        
        var subProductResource = _mapper.Map<SubProduct, SubProductResource>(result.Resource);
        return Ok(subProductResource);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _subProductService.Delete(id);
        
        if (!result.Success)
            return BadRequest(result.Message);
        
        var subProductResource = _mapper.Map<SubProduct, SubProductResource>(result.Resource);
        return Ok(subProductResource);
    }
}