using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Reserva
{
    public int IdReserva { get; set; }

    public int IdCliente { get; set; }

    public int IdSucursal { get; set; }

    public DateTime FechaReserva { get; set; }

    public int? CantidadPersonas { get; set; }

    public string? EstadoReserva { get; set; }

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Sucursal IdSucursalNavigation { get; set; } = null!;
}
