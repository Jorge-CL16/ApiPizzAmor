using System;
using System.Collections.Generic;

namespace Api1.Models;

public partial class DetallesOrden
{
    public int IdDetalle { get; set; }

    public int IdOrden { get; set; }

    public int IdMenu { get; set; }

    public int Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public virtual Menu IdMenuNavigation { get; set; } = null!;

    public virtual Ordene IdOrdenNavigation { get; set; } = null!;
}
