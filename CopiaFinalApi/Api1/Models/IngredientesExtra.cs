using System;
using System.Collections.Generic;

namespace Api1.Models;

public partial class IngredientesExtra
{
    public int IdExtra { get; set; }

    public int IdOrden { get; set; }

    public int IdIngrediente { get; set; }

    public virtual Ingrediente IdIngredienteNavigation { get; set; } = null!;

    public virtual Ordene IdOrdenNavigation { get; set; } = null!;
}
