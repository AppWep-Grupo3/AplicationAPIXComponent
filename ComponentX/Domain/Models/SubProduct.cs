namespace BackendXComponent.ComponentX.Domain.Models;

public class SubProduct
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Specification { get; set; }
    
    public decimal Price { get; set; }
    public string Image { get; set; }
    
    
    //relacion de asociacion de muchos a uno
    
    public int ProductId { get; set; }
    public Product Product { get; set; }

    
    
}