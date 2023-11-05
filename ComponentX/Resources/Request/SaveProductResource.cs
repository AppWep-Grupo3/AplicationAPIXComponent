using System.ComponentModel.DataAnnotations;

namespace BackendXComponent.ComponentX.Resources;

public class SaveProductResource
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string Description { get; set; }
    
    [Required]
    public string Image { get; set; }
    
    [Required]
    public decimal Price { get; set; }
    
    [Required]
    public string SpecificDetails { get; set; }
    
    [Required]
    public string GeneralDetails { get; set; }
    
    
    
}