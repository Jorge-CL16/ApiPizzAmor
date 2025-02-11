namespace Api1.Models.Request
{
    public class EmpleadoRequest
    {
        public int IdEmpleado { get; set; }
        
        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public int Edad {  get; set; }

        public String Sexo { get; set; }

        public DateTime FechaContratacion { get; set; }

        public int IdPuesto { get; set; }   

    }
}
