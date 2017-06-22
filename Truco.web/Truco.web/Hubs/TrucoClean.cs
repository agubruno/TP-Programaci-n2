﻿using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrabajoPrácticoTRUCO.Entidades;//aca va nuestras entidades

namespace Truco.Web.Hubs
{
    [HubName("truco")]
    public class Truco : Hub
    {
        public static Partida juego = new Partida();

        public void AgregarJugador(string nombre)
        {
            if (juego.VerificarDisponibilidad() == false)
            {
                // Si el juego esta completo...
                Clients.Caller.mostrarmensaje("El juego ya está completo!");
            }
            if (juego.VerificarDisponibilidad() == true)
            {                          
                // Sino ... abajo nos dice en nombre del jugador que se une
                juego.InsertarJugador(nombre, Context.ConnectionId);
                Clients.Others.mostrarnuevousuario(nombre);
                foreach (var jugador in juego.Jugadores)
                {
                    // Por cada jugador - muestra cada jugador en el tablero
                    Clients.All.mostrarnombre(jugador);
                }


                if (juego.VerificarDisponibilidad() == false)
                {
                    foreach (var jugador in juego.Jugadores)
                    {
                        Clients.Client(jugador.IdConexion).mostrarpuntos("Ellos", 0);
                        Clients.Client(jugador.IdConexion).mostrarpuntos("Nosotros", 0);
                        juego.AsignarEquipos();
                    }
                    Repartir();
                    HabilitarCartas();
                }
            }
        }
          
        public void HabilitarCartas()
        {
            foreach (var jugador in juego.Jugadores)
            {
                if (jugador.Turno)
                {
                    Clients.Client(jugador.IdConexion).habilitarMovimientos();
                }
                else
                {
                    Clients.Client(jugador.IdConexion).deshabilitarMovimientos();
                }
            }
        }

        //public void cantar(string accion)
        //{
        //    //requiere jugador -> id conexion
        //    Jugador jugador = new Jugador();
        //    var conexion = Context.ConnectionId;
        //    foreach (var jug in juego.Jugadores)
        //    {
        //        if (jug.IdConexion == conexion)
        //        {
        //            jugador = jug;
        //            break;
        //        }
        //    }
        //    Mano mano = new Mano();

        //    Clients.Others.mostrarmensaje("Jugador X canto ACCION");
        //    Clients.Caller.mostrarmensaje("Yo cante ACCION");

        //    Clients.Client(jugador.IdConexion).deshabilitarMovimientos();

        //    // Si el juego termino...
        //    Clients.Client(jugador.IdConexion).mostrarMensajeFinal(true); // GANADOR
        //    Clients.Client(jugador.IdConexion).mostrarMensajeFinal(false); // PERDEDOR
        //    Clients.All.deshabilitarMovimientos();

        //    // Sino
        //    Clients.All.limpiarpuntos();
        //    //ellos 

        //    //List<Carta> cartas = new List<Carta>();
        //    //foreach (var jugado in juego.Jugadores)
        //    //{
        //    //    foreach (var cart in jugado.Cartas)
        //    //    {
        //    //        cartas.Add(cart);
        //    //    }
        //    //}

        //    //Clients.All.mostrarPuntosEnvido(mano.CalcularPuntosEnvido(cartas)); 
            

        //    // Y mostrar puntos y repartir.

           
        //    switch (accion)
        //    {
        //        case "me voy al mazo":
        //            break;
        //        case "envido":
        //            Clients.All.hidemazo();
        //            mano.jugarEnvido(juego.Jugadores);//ver
        //            //envido
        //            break;
        //        case "envidoenvido":
        //            Clients.All.hidemazo();
        //            break;
        //        case "faltaenvido":
        //            Clients.All.hidemazo();
        //            break;
        //        case "realenvido":
        //            Clients.All.hidemazo();
        //            break;
        //        case "truco":
        //            break;
        //        case "retruco":
        //            break;
        //        case "vale4":
        //            break;
        //    }
        //}
        //// PARA ENVIDO FALTA ASIGNAR MANO

        //public void EjecutarAccion(string accion, bool confirmacion)
        //{
        //    //confirmacion == true //=> Acepto la acción.
        //    Clients.All.mostrarmensaje("Jugador X acepto/rechazo la ACCION");

        //    //
        //    Jugador jugador = new Jugador();
        //    var conexion = Context.ConnectionId;
        //    foreach (var jug in juego.Jugadores)
        //    {
        //        if (jug.IdConexion == conexion)
        //        {
        //            jugador = jug;
        //            break;
        //        }
        //    }

        //    switch (accion)
        //    {
        //        case "Envido":
        //            Clients.All.showmazo();
        //            Clients.Client(jugador.IdConexion).habilitarMovimientos();
        //            break;
        //        case "EnvidoEnvido":
        //            Clients.All.showmazo();
        //            Clients.Client(jugador.IdConexion).habilitarMovimientos();
        //            break;
        //        case "RealEnvido":
        //            Clients.All.showmazo();
        //            Clients.Client(jugador.IdConexion).habilitarMovimientos();
        //            break;
        //        case "FaltaEnvido":
        //            Clients.All.showmazo();
        //            Clients.Client(jugador.IdConexion).habilitarMovimientos();
        //            break;
        //        case "Truco":
        //            break;
        //        case "ReTruco":
        //            break;
        //        case "Vale4":
        //            break;
        //    }
        //}

        public void JugarCarta(string codigoCarta)
        {
            Jugador Jugador = juego.Jugadores.Find(x => x.IdConexion == Context.ConnectionId); //hacerlo en un metodo y metodo que devuelva equipo
            var mano = juego.Manos[juego.Manos.Count() - 1];

            if (Jugador.Turno)
            {
                Carta carta = Jugador.Cartas.Find(x => x.Codigo == codigoCarta);
                int CartaElegida = Jugador.Cartas.FindIndex(x => x.Codigo == codigoCarta) + 1;
                Clients.All.mostrarCarta(carta, Jugador.NombreInterno, CartaElegida);

                mano.CartaJugada(carta);
                juego.AsignarTurno();

            }
            if (mano.ValidarCartasJugadas())
            {
                var jugadorGanador = mano.JugadorGanador(juego.Jugadores);
                juego.AsignarTurno(jugadorGanador);
                juego.Manos[juego.Manos.Count - 1].EquipoGanadorMano = jugadorGanador.Equipo;


                if (juego.RondaGanada())
                {
                    juego.Puntajes(jugadorGanador);

                    if (jugadorGanador.Equipo == 1)
                    {
                        Clients.Caller.mostrarmensaje("El equipo numero uno gano la ronda!");
                    }
                    else
                    {
                        Clients.Caller.mostrarmensaje("El equipo numero dos gano la ronda!");
                    }

                    foreach (var jugador in juego.Jugadores)
                    {
                        if (jugador.Equipo == 1)
                        {
                            Clients.Client(jugador.IdConexion).mostrarpuntos("Ellos", juego.PuntajesEquipo2);
                            Clients.Client(jugador.IdConexion).mostrarpuntos("Nosotros", juego.PuntajesEquipo1);
                        }
                        else
                        {
                            Clients.Client(jugador.IdConexion).mostrarpuntos("Ellos", juego.PuntajesEquipo1);
                            Clients.Client(jugador.IdConexion).mostrarpuntos("Nosotros", juego.PuntajesEquipo2);
                        }
                    }
                    Repartir();
                }

                Mano nuevamano = new Mano();
                nuevamano.NumeroDeRonda = mano.NumeroDeRonda + 1;
                juego.Manos.Add(nuevamano);
            } 
            

            HabilitarCartas();
        }

        public void Repartir()
        {
            //juego.RemoverCartas();
            Clients.All.limpiarTablero();
            var nuevomazo = juego.mazo.MezclarMazo();
            Mano nuevamano = new Mano();
            nuevamano.NumeroDeRonda = 1;
            juego.Manos.Add(nuevamano);
            var jugadores = nuevamano.Repartir(nuevomazo, juego.Jugadores);

            foreach (var jugador in jugadores)
            {
                if (jugador.Turno)
                {
                    Clients.Client(jugador.IdConexion).habilitarMovimientos();
                }
                else
                {
                    Clients.Client(jugador.IdConexion).desabilitarMovimientos();
                }
                
                Clients.Client(jugador.IdConexion).mostrarCartas(jugador.Cartas.ToArray());
            }
        }
    }
}

//Clients.Client(jugador.IdConexion).hideEnvidoEnvidoBotton();
//Clients.Client(jugador.IdConexion).hideVale4Botton();
//Clients.Client(jugador.IdConexion).hideReTrucoBotton();
//Clients.Client(jugador.IdConexion).showEnvidoBotton();
//Clients.Client(jugador.IdConexion).showTrucoBotton();
//Clients.Client(jugador.IdConexion).showRealEnvidoBotton();
//Clients.Client(jugador.IdConexion).showFaltaEnvidoBotton();

//Clients.Client(jugador.IdConexion).deshabilitarMovimientos();
//Clients.Client(jugador.IdConexion).hideEnvidoOptions();
//Clients.Client(jugador.IdConexion).hideTrucoBotton();
//Clients.Client(jugador.IdConexion).hideReTrucoBotton();
////Clients.Client(jugador.IdConexion).hideVale4Botton();

