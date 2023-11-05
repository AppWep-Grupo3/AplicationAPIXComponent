using System.ComponentModel.DataAnnotations;

namespace BackendXComponent.ComponentX.Resources;

public class SubProductResource
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Specification { get; set; }
    
    public decimal Price { get; set; }
    public string Image { get; set; }
    
    public int ProductId { get; set; }
    //public ProductResource Product { get; set; }
    
    
}