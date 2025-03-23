using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class OrdenFisica
{
    public int IdOrdenF { get; set; }

    public int IdEmpleado { get; set; }

    public int? IdRefresco { get; set; }

    public string? SaborIngredientes { get; set; }

    public string TamanioPizza { get; set; } = null!;

    public DateTime? FechaF { get; set; }

    public string? UrlOrdenF { get; set; }

    public decimal MontoTotal { get; set; }

    public virtual Empleado? IdEmpleadoNavigation { get; set; }

    public virtual Refresco? IdRefrescoNavigation { get; set; }
}
