using System;

namespace Backend.DTOs;

public class BrandUpdateDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int SupplierId { get; set; }
    
}
