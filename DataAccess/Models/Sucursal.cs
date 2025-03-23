using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Sucursal
{
    public int IdSucursal { get; set; }

    public string Direccion { get; set; } = null!;

    public string TelefonoSucu { get; set; } = null!;

    public virtual ICollection<OrdenDigital> OrdenDigitals { get; set; } = new List<OrdenDigital>();

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
