namespace Api1.Models.Request
{
    public class OrdeneRequest
    {
        public int IdOrden { get; set; }

        public int IdCliente { get; set; }

        public int IdEmpleado { get; set; }

        public DateTime FechaOrden { get; set; }

        public decimal MontoTotal { get; set; }

        public DateTime CreadoEn { get; set; }

        public DateTime ActualizadoEn { get; set; }
    }
}
