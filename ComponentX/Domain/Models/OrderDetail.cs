﻿namespace BackendXComponent.ComponentX.Domain.Models
 {
     public class OrderDetail
     {
         public int Id { get; set; }
         public int Quantity { get; set; }
         public decimal UnitPrice { get; set; }
         public int OrderId { get; set; }
         
         public int ProductId { get; set; }
       
     }
 }