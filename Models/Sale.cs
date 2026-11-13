using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

public class Sale
{
  
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SaleId { get; set; }
    public int Quantity { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public double UnitPrice { get; set; }
    [Column(TypeName = "date")]
    public DateTime SaleDate { get; set; }
    [MaxLength(50)]
    [Column(TypeName = "varchar(50)")]
    public string ClientName { get; set; }
    public int InventoryId { get; set; }
    [ForeignKey("InventoryId")]
    public virtual Inventory Inventory { get; set; }

}
