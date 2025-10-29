using SistemaAlmacen.Domain.Exceptions;
using SistemaAlmacen.Domain.ValueObjects;

namespace SistemaAlmacen.Tests.CORE.DOMAIN.ValueObjects
{
    [TestClass]
    public class TelefonoVOTests
    {
        #region Pruebas con Datos Correctos
        [TestMethod]
        public void Constructor_TelefonoValido_CreaInstanciaCorrectamente()
        {
            //Configuración
            string telefonoValido = "1234567890";

            //Ejecución
            var telefonoVO = new TelefonoVO(telefonoValido);

            //Verificación
            Assert.AreEqual(telefonoValido, telefonoVO.Valor);
        }
        #endregion

        #region Pruebas con Datos Incorrectos
        [TestMethod]
        [ExpectedException(typeof(ExcepcionReglaNegocio))]
        public void TelefonoNulo_LanzaExcepcion()
        {
            new TelefonoVO(null!);
        }

        [TestMethod]
        public void Constructor_TelefonoVacio_LanzaExcepcion()
        {
            //Configuración
            string telefonoVacio = "";

            //Ejecución
            var excepcion = Assert.ThrowsException<ExcepcionReglaNegocio>(() => new TelefonoVO(telefonoVacio));

            //Verificación
            Assert.AreEqual(1003, excepcion.Codigo);

        }

        [TestMethod]
        public void Constructor_TelefonoConMasDe10Caracteres_LanzaExcepcion()
        {
            //Configuración
            string codigoLargo = "01234567899";

            //Ejecución
            var excepcion = Assert.ThrowsException<ExcepcionReglaNegocio>(() => new TelefonoVO(codigoLargo));

            //Verificación
            Assert.AreEqual(1004, excepcion.Codigo);
        }

        [TestMethod]
        public void Constructor_TelefonoConMenosDe10Caracteres_LanzaExcepcion()
        {
            //Configuración
            string codigoLargo = "012345678";

            //Ejecución
            var excepcion = Assert.ThrowsException<ExcepcionReglaNegocio>(() => new TelefonoVO(codigoLargo));

            //Verificación
            Assert.AreEqual(1004, excepcion.Codigo);
        }

        [TestMethod]
        public void Constructor_TelefonoConCaracteresyDigitos_LanzaExcepcion()
        {
            //Configuración
            string codigoLargo = "A012345678";

            //Ejecución
            var excepcion = Assert.ThrowsException<ExcepcionReglaNegocio>(() => new TelefonoVO(codigoLargo));

            //Verificación
            Assert.AreEqual(1004, excepcion.Codigo);
        }

        #endregion

    }
}
