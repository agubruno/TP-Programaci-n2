using Microsoft.AspNet.SignalR;
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
                    Clients.Client(jugador.IdConexion).desabilitarMovimientos();
                }
            }
        }

        public void cantar(string accion)
        {
            //requiere jugador -> id conexion
            Jugador jugador = new Jugador();
            var conexion = Context.ConnectionId;
            foreach (var jug in juego.Jugadores)
            {
                if (jug.IdConexion == conexion)
                {
                    jugador = jug;
                    break;
                }
            }
            Clients.Others.mostrarmensaje("Jugador X canto ACCION");
            Clients.Caller.mostrarmensaje("Yo cante ACCION");

            Clients.Client(jugador.IdConexion).deshabilitarMovimientos();

            // Si el juego termino...
            Clients.Client(jugador.IdConexion).mostrarMensajeFinal(true); // GANADOR
            Clients.Client(jugador.IdConexion).mostrarMensajeFinal(false); // PERDEDOR
            Clients.All.deshabilitarMovimientos();

            // Sino
            Clients.All.limpiarpuntos();

            // Y mostrar puntos y repartir.


            switch (accion)
            {
                case "me voy al mazo":
                    break;
                case "envido":
                    Clients.All.hidemazo();
                    break;
                case "envidoenvido":
                    Clients.All.hidemazo();
                    break;
                case "faltaenvido":
                    Clients.All.hidemazo();
                    break;
                case "realenvido":
                    Clients.All.hidemazo();
                    break;
                case "truco":
                    break;
                case "retruco":
                    break;
                case "vale4":
                    break;
            }
        }
        // PARA ENVIDO FALTA ASIGNAR MANO

        public void EjecutarAccion(string accion, bool confirmacion)
        {
            //confirmacion == true //=> Acepto la acción.
            Clients.All.mostrarmensaje("Jugador X acepto/rechazo la ACCION");

            //
            Jugador jugador = new Jugador();
            var conexion = Context.ConnectionId;
            foreach (var jug in juego.Jugadores)
            {
                if (jug.IdConexion == conexion)
                {
                    jugador = jug;
                    break;
                }
            }

            switch (accion)
            {
                case "Envido":
                    Clients.All.showmazo();
                    Clients.Client(jugador.IdConexion).habilitarMovimientos();
                    break;
                case "EnvidoEnvido":
                    Clients.All.showmazo();
                    Clients.Client(jugador.IdConexion).habilitarMovimientos();
                    break;
                case "RealEnvido":
                    Clients.All.showmazo();
                    Clients.Client(jugador.IdConexion).habilitarMovimientos();
                    break;
                case "FaltaEnvido":
                    Clients.All.showmazo();
                    Clients.Client(jugador.IdConexion).habilitarMovimientos();
                    break;
                case "Truco":
                    break;
                case "ReTruco":
                    break;
                case "Vale4":
                    break;
            }
        }

        public void JugarCarta(string codigoCarta)
        {
            Jugador jugador = juego.Jugadores.Find(x => x.IdConexion == Context.ConnectionId); //hacerlo en un metodo y metodo que devuelva equipo

            if (jugador.Turno)
            {
                Carta carta = jugador.Cartas.Find(x => x.Codigo == codigoCarta);
                int CartaElegida = jugador.Cartas.FindIndex(x => x.Codigo == codigoCarta) + 1;
                Clients.All.mostrarCarta(carta, jugador.NombreInterno, CartaElegida);

                juego.AsignarTurno();
                
                //juego.Jugadores = juego.AsignarTurno(juego.Jugadores);
                HabilitarCartas();
            }
        }

        public void Repartir()
        {
            Clients.All.limpiarTablero();
            var nuevomazo = juego.mazo.MezclarMazo();
            Mano nuevamano = new Mano();
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

//Clients.Client(jugador.IdConexion).desabilitarMovimientos();
//Clients.Client(jugador.IdConexion).hideEnvidoOptions();
//Clients.Client(jugador.IdConexion).hideTrucoBotton();
//Clients.Client(jugador.IdConexion).hideReTrucoBotton();
////Clients.Client(jugador.IdConexion).hideVale4Botton();

