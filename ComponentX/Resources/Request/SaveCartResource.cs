using System.ComponentModel.DataAnnotations;

namespace BackendXComponent.ComponentX.Resources;

public class SaveCartResource
{
    [Required]
    public int UserId { get; set; } // Clave externa para el usuario
        
    [Required]
    public int ProductId { get; set; } // Clave externa para el producto
        
    public int? SubproductId { get; set; } // Clave externa para el subproducto (puede ser nulo si no se selecciona subproducto)
        
    [Required]
    public int Quantity { get; set; }
    
    [Required]
    public decimal TotalPrice { get;  set; }
}