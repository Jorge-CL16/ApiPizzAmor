namespace Api1.Models
{
    public partial class Refresco
    {
        public int IdRefresco { get; set; }  
        public string Nombre { get; set; } = null!;  
        public decimal Precio { get; set; }  
        public string Tamano { get; set; } = null!;  

        public virtual ICollection<RefrescosOrdene> RefrescosOrdenes { get; set; } = new List<RefrescosOrdene>();
    }
}

