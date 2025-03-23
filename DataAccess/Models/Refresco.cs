using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class Refresco
{
    public int IdRefresco { get; set; }

    public string Marca { get; set; } = null!;

    public decimal PrecioR { get; set; }

    public string TamanioR { get; set; } = null!;

    public virtual ICollection<Menu> Menus { get; set; } = new List<Menu>();

    public virtual ICollection<OrdenDigital> OrdenDigitals { get; set; } = new List<OrdenDigital>();

    public virtual ICollection<OrdenFisica> OrdenFisicas { get; set; } = new List<OrdenFisica>();
}
