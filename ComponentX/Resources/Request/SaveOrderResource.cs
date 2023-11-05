using System.ComponentModel.DataAnnotations;

namespace BackendXComponent.ComponentX.Resources;

public class SaveOrderResource
{
    [Required]
    public DateTime Date { get; set; }
    
    [Required]
    public string State { get; set; }
    
    [Required]
    public int UserId { get; set; }
        
}