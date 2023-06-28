namespace MarcadoresTP
{
    public class MarcadoresResponse
    {
        public string error { get; set; }
        public bool ok { get; set; }
        public string mensajeInfo { get; set; }
        public int statusCode { get; set; }
        public List<Marcadores> litadoMarcadores { get; set; }
    }
}
