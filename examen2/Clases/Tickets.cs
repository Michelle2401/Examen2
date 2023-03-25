using System;

namespace Clases
{
    public class Tickets
    {
        public string Id { get; set; }
        public DateTime Fecha { get; set; }
        public string IdentidadCliente { get; set; }
        public string NombreCliente { get; set; }
        public string TipoSoporte { get; set; }
        public string DescripcionProblema { get; set; }
        public decimal Costo { get; set; }
        public string DescripcionSolucion { get; set; }
    }
}
