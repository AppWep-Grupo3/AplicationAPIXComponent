namespace BackendXComponent.ComponentX.Resources;

public class CartResource
{
    public int Id { get; set; } // Clave primaria
    public int UserId { get; set; } // Clave externa para el usuario
    public int ProductId { get; set; } // Clave externa para el producto
    public int? SubproductId { get; set; } // Clave externa para el subproducto (puede ser nulo si no se selecciona subproducto)
    public int Quantity{ get; set; }
    public decimal TotalPrice { get; set; }
}