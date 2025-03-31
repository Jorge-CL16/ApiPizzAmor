namespace WebPizzAmor.Models
{
    public class CajaViewModel
    {
        public int IdEmpleado { get; set; }
        public int IdRefresco { get; set; }
        public string SaborIngredientes { get; set; }
        public string TamanioPizza { get; set; }
        public decimal MontoTotal { get; set; }
        public List<string> IngredientesSeleccionados { get; set; }
    }
}
