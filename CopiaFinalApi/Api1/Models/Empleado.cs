using System;
using System.Collections.Generic;

namespace Api1.Models;

public partial class Empleado
{
    public int IdEmpleado { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public int Edad { get; set; }

    public string? Sexo { get; set; }

    public DateOnly FechaContratacion { get; set; }

    public int? IdPuesto { get; set; }

    public virtual Puesto? IdPuestoNavigation { get; set; }

    public virtual ICollection<Ordene> Ordenes { get; set; } = new List<Ordene>();
}
