namespace Api1.Models.Request
{
    public class MenuRequest
    {
        public int IdMenu { get; set; }

        public string Nombre { get; set; } = null!;

        public string Descripcion { get; set; }

        public string ImagenUrl { get; set; }

        public decimal Precio { get; set; }

        public int IdTipoPizza { get; set; }

        public int IdTamano { get; set; }
    }
}
