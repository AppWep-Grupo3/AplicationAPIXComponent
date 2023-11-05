using System.ComponentModel.DataAnnotations;

namespace BackendXComponent.ComponentX.Resources;

public class SaveSubProductResource
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Specification { get; set; }
    
    [Required]
    public decimal Price { get; set; }
    
    [Required]
    public string Image { get; set; }
    
    [Required]
    public int ProductId { get; set; }
}