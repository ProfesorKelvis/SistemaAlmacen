using SistemaAlmacen.Domain.Exceptions;

namespace SistemaAlmacen.Domain.ValueObjects
{
    /// <summary>
    /// Implementación de Reglas: RB01
    /// </summary>
    public sealed record CodigoVO
    {        

        // Properties
        public string Valor { get; }

        // Constructor
        public CodigoVO(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                throw new ExcepcionReglaNegocio("El código es obligatorio.", 1001);

            var v = valor.Trim().ToUpper();

            if (v.Length != 5)
                throw new ExcepcionReglaNegocio("El código debe ser de 5 caracteres.", 1002);

            Valor = v;
        }
    }
}
