using System.ComponentModel.DataAnnotations;

namespace BackendXComponent.ComponentX.Resources;

public class DeleteFromCarritoResource
{
    [Required]
    public int ProductId { get; set; } // Clave externa para el usuario
    
    [Required]
    public int UserId { get; set; }
}