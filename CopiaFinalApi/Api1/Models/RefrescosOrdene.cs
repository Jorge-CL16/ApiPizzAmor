namespace Api1.Models
{
    public partial class RefrescosOrdene
    {
        public int IdOrden { get; set; }
        public int IdRefresco { get; set; }
        public virtual Refresco Refresco { get; set; } = null!;

        public virtual Ordene Ordene { get; set; } = null!;
    }
}
