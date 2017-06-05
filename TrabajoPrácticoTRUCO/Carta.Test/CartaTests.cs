﻿using System;
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
            //Arange

            //    var carta1 = Carta. referenciar
            //Act

            //Assert -> Espera Excepcion

        }

        [TestMethod]
        [ExpectedException(typeof(NotSupportedException))]
        public void partida()
        {
            //Arrange
            Partida nuevaPartida = new Partida();
            int c = 0;

            //Act
            while (nuevaPartida.VerificarDisponibilidad())
            {
                nuevaPartida.InsertarJugador("c");
            }
            var lista = nuevaPartida.Jugadores;
            //Assert

        }


    }
}
