using System;
using Microsoft.Identity.Client;

namespace Backend.DTOs;

public class SupplierDto
{
    public int SupplierId { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string Phone { get; set; }

}
