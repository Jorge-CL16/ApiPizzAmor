using System;
using System.Collections.Generic;

namespace Api1.Models;

public partial class TamanoDePizza
{
    public int IdTamano { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal Precio { get; set; }

    public virtual ICollection<Menu> Menus { get; set; } = new List<Menu>();
}
