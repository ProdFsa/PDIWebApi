using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PDIEntities.Models;

public partial class User
{
    [Key]
    [StringLength(50)]
    public string EmpId { get; set; } = null!;

    [StringLength(100)]
    public string? Name { get; set; }

    [StringLength(50)]
    public string? District { get; set; }

    [StringLength(10)]
    public string? Country { get; set; }

    [StringLength(100)]
    public string? Slc { get; set; }

    [StringLength(100)]
    public string? Email { get; set; }

    [StringLength(10)]
    public string? AdminAccess { get; set; }
}
