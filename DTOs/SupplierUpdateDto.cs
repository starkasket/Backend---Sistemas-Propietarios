using System;

namespace Backend.DTOs;

public class SupplierUpdateDto
{
    public int SupplierId { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string Phone { get; set; }

}
