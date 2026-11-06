using System;

namespace Backend.DTOs;

public class PostDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string title { get; set; }
    public string body { get; set; }

    
}
