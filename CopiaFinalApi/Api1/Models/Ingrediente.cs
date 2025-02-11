using System;
using System.Collections.Generic;

namespace Api1.Models;

public partial class Ingrediente
{
    public int IdIngrediente { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<IngredientesExtra> IngredientesExtras { get; set; } = new List<IngredientesExtra>();

    public virtual ICollection<Menu> IdMenus { get; set; } = new List<Menu>();
}
