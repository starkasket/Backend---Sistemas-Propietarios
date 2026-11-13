using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

public class Supplier
{
    
     [Key]
     [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
     public int SupplierId { get; set; }
     [MaxLength(50)]
     [Column(TypeName = "varchar(50)")]
     public string Name { get; set; }
     [MaxLength(50)]
     [Column(TypeName = "varchar(50)")]
     public string Country { get; set; }
     [MaxLength(15)]
     [Column(TypeName = "varchar(15)")]
     public string Phone { get; set; }  

}
