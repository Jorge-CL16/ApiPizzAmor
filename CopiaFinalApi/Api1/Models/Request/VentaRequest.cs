namespace Api1.Models.Request
{
    public class VentaRequest
    {
        public int IdVenta { get; set; }

        public int IdOrden { get; set; }

        public DateTime FechaVenta { get; set; }

        public decimal MontoTotal { get; set; }
    }
}
