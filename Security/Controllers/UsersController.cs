using AutoMapper;
using BackendXComponent.Security.Authorization.Attributes;
using BackendXComponent.Security.Domain.Models;
using BackendXComponent.Security.Domain.Services;
using BackendXComponent.Security.Domain.Services.Communication;
using BackendXComponent.Security.Resources.Response;
using BackendXComponent.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BackendXComponent.Security.Controllers;

[Authorize]
[ApiController]
[Route("/api/v1/[controller]")]

public class UsersController: ControllerBase
{
    private readonly ImplUserService _userService;
    private readonly IMapper _mapper;
    
    public UsersController(ImplUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }
    
    [AllowAnonymous]
    [HttpPost("sign-in")]
    public async Task<IActionResult> Authenticate(AuthenticateRequest
        request)
    {
        var response = await _userService.Authenticate(request);
        return Ok(response);
    }
    
    [AllowAnonymous]
    [HttpPost("sign-up")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        await _userService.RegisterAsync(request);
        return Ok(new { message = "Registration successful" });
    }
    
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.ListAsync();
        var resources = _mapper.Map<IEnumerable<User>,
            IEnumerable<UserResponseDto>>(users);
        return Ok(resources);
    }
    
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _userService.GetByIdAsync(id);
        var resource = _mapper.Map<User, UserResponseDto>(user);
        return Ok(resource);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateRequest request)
    {
        await _userService.UpdateAsync(id, request);
        return Ok(new { message = "User updated successfully" });
    }

    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _userService.DeleteAsync(id);
        return Ok(new { message = "User deleted successfully" });
    }
    
}