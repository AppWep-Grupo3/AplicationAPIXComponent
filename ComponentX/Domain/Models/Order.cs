namespace BackendXComponent.ComponentX.Domain.Models;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public DateTime Date { get; set; }
    public string Status { get; set; }
    public int Total { get; set; }
    
    public int ProductId { get; set; }
}