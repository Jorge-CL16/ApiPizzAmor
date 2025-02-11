namespace Api1.Models.Request
{
    public class ClienteRequest
    {
        public int IdCliente { get; set; } 
        public string Nombre { get; set; }

        public string Correo { get; set; }

        public string Telefono { get; set; }

        public int Activo { get; set; }

    }
}
