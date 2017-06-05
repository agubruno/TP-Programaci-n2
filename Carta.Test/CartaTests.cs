using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrabajoPrácticoTRUCO.Entidades;

namespace TrabajoPrácticoTRUCO.Test
{
    [TestClass]
    public class CartaTests
    {
        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void NoPermitirCrearCartaDuplicada()
        {
            var carta = Carta.CrearCarta();
            //Arange

            //    var carta1 = Carta. referenciar
            //Act

            //Assert -> Espera Excepcion

        }
    }
}
