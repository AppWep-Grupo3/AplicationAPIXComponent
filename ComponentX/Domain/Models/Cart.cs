namespace BackendXComponent.ComponentX.Domain.Models;

public class Cart
{
    public int Id { get; set; } // Clave primaria
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public int UserId { get; set; } // Clave externa para el usuario
    //public User User { get; set; }//Relacion de asociacion de uno a uno a usuario
   
   
   //Relacion de asociacion de uno a muchos con productos
   
   public int ProductID { get; set; } // Clave externa para el producto
    public int? SubproductId { get; set; } // Clave externa para el subproducto (puede ser nulo si no se selecciona subproducto)
    
   
}