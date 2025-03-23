using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Repartidor
{
    public int IdRepartidor { get; set; }

    public string NombreR { get; set; } = null!;

    public string ApellidoR { get; set; } = null!;

    public string Placa { get; set; } = null!;

    public string? UrlRepartidor { get; set; }

    public virtual ICollection<OrdenDigital> OrdenDigitals { get; set; } = new List<OrdenDigital>();
}
