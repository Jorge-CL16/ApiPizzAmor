using System;
using System.Collections.Generic;

namespace Api1.Models;

public partial class Puesto
{
    public int IdPuesto { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
