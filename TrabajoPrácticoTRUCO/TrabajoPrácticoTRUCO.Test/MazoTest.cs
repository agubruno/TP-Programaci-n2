using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrabajoPrácticoTRUCO.Entidades;
using System.Collections.Generic;
using System.Linq;

namespace TrabajoPrácticoTRUCO.Test
{
    [TestClass]
    public class MazoTest
    {
        [TestMethod]
        public void CrearMazo()
        {
            //Arange
            var mazo = new Mazo();
            //Act
            var cartas = mazo.ObtenerMazo();
            //Assert
            Assert.AreEqual(cartas.Count(), 40);
        }

        [TestMethod]
        public void MezclarMazo()
        {
            //Arange
            var mazo1 = new Mazo();
            var mazo2 = new Mazo();        
            
            //Act
            var cartas1 = mazo1.MezclarMazo();
            var cartas2 = mazo2.MezclarMazo();

            //Assert
            Assert.AreNotEqual(cartas1, cartas2);
        }
    }
}
