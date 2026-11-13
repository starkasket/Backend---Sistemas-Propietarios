using System;

namespace Backend.DTOs;

public class SaleInsertDto
{
    public int Quantity { get; set; }
    public double UnitPrice { get; set; }
    public DateTime SaleDate { get; set; }
    public string ClientName { get; set; }
    public int InventoryId { get; set; }


}
