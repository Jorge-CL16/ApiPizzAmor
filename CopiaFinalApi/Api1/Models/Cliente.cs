using System;
using System.Collections.Generic;

namespace Api1.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string Nombre { get; set; }

    public string? Correo { get; set; }

    public string? Telefono { get; set; }

    public int Activo { get; set; }
    public virtual ICollection<Ordene> Ordenes { get; set; } = new List<Ordene>();
}
