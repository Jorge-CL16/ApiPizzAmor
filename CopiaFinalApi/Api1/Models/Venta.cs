using System;
using System.Collections.Generic;

namespace Api1.Models;

public partial class Venta
{
    public int IdVenta { get; set; }

    public int IdOrden { get; set; }

    public DateTime FechaVenta { get; set; }

    public decimal MontoTotal { get; set; }

    public virtual Ordene IdOrdenNavigation { get; set; } = null!;
}
