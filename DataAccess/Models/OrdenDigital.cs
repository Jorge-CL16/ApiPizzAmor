using DataAccess.Models;

public partial class OrdenDigital
{
    public int IdOrdenD { get; set; }

    public int IdCliente { get; set; }

    public int IdRepartidor { get; set; }

    public int? IdRefresco { get; set; }

    public int IdSucursal { get; set; }

    public string? SaborIngredientes { get; set; }

    public string TamanioPizza { get; set; } = null!;

    public DateTime? FechaD { get; set; }

    public string Correo { get; set; } = null!;

    public string? UrlOrdenD { get; set; }

    public decimal MontoTotal { get; set; }

    public virtual Cliente? IdClienteNavigation { get; set; }
    public virtual Refresco? IdRefrescoNavigation { get; set; }
    public virtual Repartidor? IdRepartidorNavigation { get; set; }
    public virtual Sucursal? IdSucursalNavigation { get; set; }
}
