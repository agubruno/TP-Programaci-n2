using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrabajoPrácticoTRUCO.Entidades;

namespace TrabajoPrácticoTRUCO.Test
{
    [TestClass]
    public class ManoTest
    {
        //[TestMethod]
        //public void Repartir()
        //{

        //}

        [TestMethod]
        public void CompararCartas()
        {
            //Arange
            var carta1 = new Carta(palos.Oro, 3);
            var carta2 = new Carta(palos.Copa, 2);
            var mano = new Mano();

            //Act
            var resultado = mano.CompararCartas(carta1,carta2);

            //Assert
            Assert.AreEqual(resultado, carta1);
        }
    }
}
