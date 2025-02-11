using System;
using System.Collections.Generic;

namespace Api1.Models;

public partial class Ordene
{
    public int IdOrden { get; set; }

    public int IdCliente { get; set; }

    public int IdEmpleado { get; set; }

    public DateTime FechaOrden { get; set; }

    public decimal MontoTotal { get; set; }

    public DateTime CreadoEn { get; set; }

    public DateTime ActualizadoEn { get; set; }

    public virtual ICollection<DetallesOrden> DetallesOrdens { get; set; } = new List<DetallesOrden>();

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Empleado IdEmpleadoNavigation { get; set; } = null!;

    public virtual ICollection<IngredientesExtra> IngredientesExtras { get; set; } = new List<IngredientesExtra>();

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();

    public virtual ICollection<RefrescosOrdene> RefrescosOrdenes { get; set; }
}
