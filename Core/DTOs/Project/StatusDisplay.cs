﻿using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Project;

public class StatusDisplay
{
    [Required]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; } = null!;
}