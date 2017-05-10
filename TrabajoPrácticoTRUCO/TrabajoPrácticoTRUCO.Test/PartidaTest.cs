using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrabajoPrácticoTRUCO.Entidades;
using System.Collections.Generic;

namespace TrabajoPrácticoTRUCO.Test
{
    [TestClass]
    public class PartidaTest
    {
        [TestMethod]
        public void ArmarEquipo()
        {
            //Arange
            var partida = new Partida();
            var partida2 = new Partida();
            //Act
            partida.ArmarEquipos();
            partida2.ArmarEquipos(); //ver
            //Assert

        }
    }
}
