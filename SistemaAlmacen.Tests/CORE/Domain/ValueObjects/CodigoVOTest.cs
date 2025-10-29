using SistemaAlmacen.Domain.Exceptions;
using SistemaAlmacen.Domain.ValueObjects;

namespace SistemaAlmacen.Tests.CORE.Domain.ValueObjects
{
    [TestClass]
    public class CodigoVOTest
    {
        //Casos de prueba para la regla RB01
        //RB01: El código es obligatorio y debe tener 5 caracteres.

        #region Pruebas con Datos Correctos

        [TestMethod]
        public void Constructor_CodigoValido_CreaInstanciaCorrectamente()
        {
            // Configuración
            string codigoValido = "AB123";

            // Ejecución
            var codigoVO = new CodigoVO(codigoValido);

            // Verificación
            Assert.AreEqual(codigoValido, codigoVO.Valor);
        }
        #endregion

        #region Prueba con Datos Incorrectos

        [TestMethod]
        [ExpectedException(typeof(ExcepcionReglaNegocio))]
        public void Constructor_CodigoNulo_LanzaExcepcion_Simple()
        {
            new CodigoVO(null!);
        }

        [TestMethod]
        public void Constructor_CodigoNulo_LanzaExcepcion_Estructurado()
        {
            // Configuración
            string codigoNulo = null!;

            // Ejecución
            var exception = Assert.ThrowsException<ExcepcionReglaNegocio>(() => new CodigoVO(codigoNulo));

            // Verificación
            Assert.AreEqual("1001 - El código es obligatorio.", exception.Message);
            Assert.AreEqual(1001, exception.Codigo);
        }

        [TestMethod]
        public void Constructor_CodigoVacio_LanzaExcepcion()
        {
            // Configuración
            string codigoVacio = "";

            // Ejecución
            var exception = Assert.ThrowsException<ExcepcionReglaNegocio>(() => new CodigoVO(codigoVacio));

            // Verificación
            Assert.AreEqual("1001 - El código es obligatorio.", exception.Message);
            Assert.AreEqual(1001, exception.Codigo);
        }

        [TestMethod]
        public void Constructor_CodigoConEspacios_LanzaExcepcion()
        {
            // Arrange
            string codigoConEspacios = "   ";

            // Act
            var exception = Assert.ThrowsException<ExcepcionReglaNegocio>(() => new CodigoVO(codigoConEspacios));

            // Assert
            Assert.AreEqual("1001 - El código es obligatorio.", exception.Message);
            Assert.AreEqual(1001, exception.Codigo);
        }

        [TestMethod]
        public void Constructor_CodigoConMasDe5Caracteres_LanzaExcepcion()
        {
            // Configuración
            string codigoLargo = "ABCDE12345";

            // Ejecución
            var exception = Assert.ThrowsException<ExcepcionReglaNegocio>(() => new CodigoVO(codigoLargo));

            // Verificación
            Assert.AreEqual("1002 - El código debe ser de 5 caracteres.", exception.Message);
            Assert.AreEqual(1002, exception.Codigo);
        }

        [TestMethod]
        public void Constructor_CodigoConMenosDe5Caracteres_LanzaExcepcion()
        {
            // Configuración
            string codigoLargo = "A123";

            // Ejecución
            var exception = Assert.ThrowsException<ExcepcionReglaNegocio>(() => new CodigoVO(codigoLargo));

            // Verificación
            Assert.AreEqual("1002 - El código debe ser de 5 caracteres.", exception.Message);
            Assert.AreEqual(1002, exception.Codigo);
        }

        #endregion
    }
}
