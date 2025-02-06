﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Entities;


[Index(nameof(Name), IsUnique = true)]
public class Status
{
    [Key]
    public int Id {get; init;}

    [Column(TypeName = "nvarchar(50)")]
    [Required]
    public string Name {get ; init;} = null!;
}