using System;
using System.Collections.Generic;

namespace Api1.Models;

public partial class Menu
{
    public int IdMenu { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string? ImagenUrl { get; set; }

    public decimal Precio { get; set; }

    public int IdTipoPizza { get; set; }

    public int IdTamano { get; set; }

    public virtual ICollection<DetallesOrden> DetallesOrdens { get; set; } = new List<DetallesOrden>();

    public virtual TamanoDePizza IdTamanoNavigation { get; set; } = null!;

    public virtual TiposDePizza IdTipoPizzaNavigation { get; set; } = null!;

    public virtual ICollection<Ingrediente> IdIngredientes { get; set; } = new List<Ingrediente>();
}
