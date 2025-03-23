namespace WebPizzAmor.Models
{
    public class OrdenDigitalData
    {
        public int IdOrdenD { get; set; }
        public string NombreCliente { get; set; } = "Desconocido";
        public string Telefono { get; set; } = "No disponible";
        public string Domicilio { get; set; } = "No disponible";
        public string SaborIngredientes { get; set; } = null!;
        public string TamanioPizza { get; set; } = null!;
        public decimal MontoTotal { get; set; }
    }
}
