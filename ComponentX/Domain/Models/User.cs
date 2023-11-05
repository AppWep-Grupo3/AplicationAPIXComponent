namespace BackendXComponent.ComponentX.Domain.Models;

public class User
{
    public int Id { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Email { get; set; }
    
    public string Password { get; set; }
    
    //Relacion de asociacion de uno a uno a carrito
  //  public int CartId { get; set; }
    //public Cart Cart { get; set; }
    
    //Relacion de asociacion de uno a muchos a ordenes
    public IList<Order> OrdersList { get; set; } = new List<Order>();
    
    
    
}