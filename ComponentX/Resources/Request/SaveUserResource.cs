using System.ComponentModel.DataAnnotations;

namespace BackendXComponent.ComponentX.Resources;

public class SaveUserResource
{
    [Required]
    public string FirstName { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    [Required]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
}