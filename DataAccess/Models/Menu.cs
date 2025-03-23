using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Menu
{
    public int IdMenu { get; set; }

    public int IdRefresco { get; set; }

    public string NombrePizza { get; set; } = null!;

    public string? DescripPizza { get; set; }

    public string? UrlMenu { get; set; }

    public decimal Precio { get; set; }

    public string TamanioPizza { get; set; } = null!;

    public virtual Refresco? IdRefrescoNavigation { get; set; }
}
