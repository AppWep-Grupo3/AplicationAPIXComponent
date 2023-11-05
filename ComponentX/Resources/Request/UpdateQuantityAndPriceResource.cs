using System.ComponentModel.DataAnnotations;

namespace BackendXComponent.ComponentX.Resources;

public class UpdateQuantityAndPriceResource
{
    [Required]
    public int ProductId { get; set; } // Clave externa para el usuario
    
    [Required]
    public int Quantity { get; set; }
    
    [Required]
    public decimal TotalPrice { get;  set; }
}