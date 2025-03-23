using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Empleado
{
    public int IdEmpleado { get; set; }

    public string NombreE { get; set; } = null!;

    public string ApellidoE { get; set; } = null!;

    public int? Edad { get; set; }

    public string? Sexo { get; set; }

    public string Puesto { get; set; } = null!;

    public virtual ICollection<OrdenFisica> OrdenFisicas { get; set; } = new List<OrdenFisica>();
}
