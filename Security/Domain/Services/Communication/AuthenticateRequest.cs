using System.ComponentModel.DataAnnotations;

namespace BackendXComponent.Security.Domain.Services.Communication;

public class AuthenticateRequest
{
    [Required] public string Email { get; set; }
    [Required] public string Password { get; set; }
}