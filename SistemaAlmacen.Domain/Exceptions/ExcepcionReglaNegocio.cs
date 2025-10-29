namespace SistemaAlmacen.Domain.Exceptions
{
    public class ExcepcionReglaNegocio:Exception
    {
        public int Codigo { get; }
        public ExcepcionReglaNegocio(string message, int codigo = 1000) : base($"{codigo} - {message}")
        {
            Codigo = codigo;
        }
    }
}
