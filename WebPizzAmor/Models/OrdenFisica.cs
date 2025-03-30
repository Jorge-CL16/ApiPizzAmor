using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace WebPizzAmor.Models
{
    public class OrdenFisicaViewModel
    {
        public int IdEmpleado { get; set; }
        public string SaborIngredientes { get; set; }
        public string TamanioPizza { get; set; }
        public int IdRefresco { get; set; }
        public decimal PrecioPizza { get; set; }
        public decimal PrecioRefresco { get; set; }
        public decimal MontoTotal { get; set; }
        public List<SelectListItem> Empleados { get; set; }
        public List<SelectListItem> Refrescos { get; set; }
    }
}
