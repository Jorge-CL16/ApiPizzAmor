namespace Api1.Models.Request
{
    public class DetallesOrdenRequest
    {
        public int IdDetalle { get; set; }

        public int IdOrden { get; set; }

        public int IdMenu { get; set; }

        public int Cantidad { get; set; }

        public decimal PrecioUnitario { get; set; }
    }
}
