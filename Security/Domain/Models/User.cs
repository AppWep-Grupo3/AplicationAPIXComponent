using System.Text.Json.Serialization;
using BackendXComponent.ComponentX.Domain.Models;

namespace BackendXComponent.Security.Domain.Models;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    
    //public string Username { get; set; }
    
    
    [JsonIgnore]
    public string Password { get; set; }//TODO: Hash this password
    
    //Relacion de asociacion de uno a uno a carrito
    public IList<Order> OrdersList { get; set; } = new List<Order>();
}