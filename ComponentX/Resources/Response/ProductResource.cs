namespace BackendXComponent.ComponentX.Resources;

public class ProductResource
{
    //Aca va lo que se va a mostrar en el response para el usuario
    
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public string Image { get; set; }
    
    public decimal Price { get; set; }
    
    public string SpecificDetails { get; set; }
    
    public string GeneralDetails { get; set; }
    
    //Agregar los subproductos relacionados con el producto
    public List<SubProductResource> SubProductsList { get; set; }
}