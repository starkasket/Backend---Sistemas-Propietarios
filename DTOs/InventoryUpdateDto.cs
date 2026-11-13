using System;

namespace Backend.DTOs;

public class InventoryUpdateDto
{
    public int Id { get; set; }
    public int Quantity { get; set; }
    public int BatchNumber { get; set; }
    public int BeerId { get; set; }
    public DateTime ProductionDate { get; set; }
    public string StorageSection { get; set; }
    public decimal PurchasePrice { get; set; }
    public DateTime BestBeforeDate { get; set; }
}
