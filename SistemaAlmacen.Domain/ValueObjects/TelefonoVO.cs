using SistemaAlmacen.Domain.Exceptions;

namespace SistemaAlmacen.Domain.ValueObjects
{
    public sealed record TelefonoVO
    {
        public string Valor { get; }
        public TelefonoVO(string valor)
        {

            if (string.IsNullOrWhiteSpace(valor))
            {
                throw new ExcepcionReglaNegocio("El teléfono no puede estar vacío.", 1003);
            }
            else if (valor.Length != 10 || !valor.All(char.IsDigit))
            {
                throw new ExcepcionReglaNegocio("El teléfono debe tener exactamente 10 dígitos numéricos.", 1004);
            }

            Valor = valor;
        }
    }
}
