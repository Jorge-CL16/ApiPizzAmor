using System;
using System.Collections.Generic;

namespace Api1.Models;

public partial class TiposDePizza
{
    public int IdTipoPizza { get; set; }

    public string Nombre { get; set; }

    public virtual ICollection<Menu> Menus { get; set; } = new List<Menu>();
}
