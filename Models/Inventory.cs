using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

public class Inventory
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int InventoryId { get; set; }
    public int Quantity { get; set; }
    public int BatchNumber { get; set; }
    public int BeerId { get; set; }
    [Column(TypeName = "date")]
    public DateTime ProductionDate { get; set; }
    [MaxLength(100)]
    [Column(TypeName = "varchar(100)")]
    public string StorageSection { get; set; } = string.Empty;
    [Column(TypeName = "decimal(18,2)")]
    public decimal PurchasePrice { get; set; }
    [Column(TypeName = "date")]
    public DateTime BestBeforeDate { get; set; }
    [ForeignKey("BeerId")]
    public virtual Beer Beer {get; set;}= null!;



}
