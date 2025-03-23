using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string NombreC { get; set; } = null!;

    public string ApellidoC { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string? UrlImagen { get; set; }

    public string? Telefono { get; set; }

    public string? Domicilio { get; set; }

    public virtual ICollection<OrdenDigital> OrdenDigitals { get; set; } = new List<OrdenDigital>();

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
