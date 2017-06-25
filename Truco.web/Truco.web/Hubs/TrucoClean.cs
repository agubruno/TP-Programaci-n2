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
                    Clients.Client(jugador.IdConexion).deshabilitarMovimientos();
                }
            }
        }

        //DETERMINA QUE JUGADORES TIENEN EL QUIERE FRENTE A UN CANTO
        public void SeleccionDelQuiero(Jugador jugador, string accion) //VER BIEN ESTE METODO
        {
            //ARMO EQUIPOS PARA PODER DECIDIR EL QUIERO -> ESTO NO LO HICE EN PARTIDA PARA NO MODIFICAR NADA, SINO PODRIA AGREGAR OTRA CLASE DEL TIPO EQUIPO
            List<Jugador> Equipo1 = new List<Jugador>();
            List<Jugador> Equipo2 = new List<Jugador>();

            foreach (var jug in juego.Jugadores)
            {
                if (jug.Equipo == 1)
                {
                    Equipo1.Add(jug);
                }
                else
                {
                    Equipo2.Add(jug);
                }
            }

            if (jugador.Equipo == 1)
            {
                foreach (var jug in Equipo2) //al equipo opuesto al que canto le aparece el quiero
                {
                    if (accion == "envido" || accion == "envidoenvido" || accion == "faltaenvido" || accion == "realenvido")
                    {
                        Clients.Client(jug.IdConexion).mostrarOpcionesEnvido();
                    }
                    else
                    {
                        Clients.Client(jug.IdConexion).mostrarOpcionesTruco();
                    }
                }
            }
            else
            {
                foreach (var jug in Equipo1)
                {
                    if (accion == "envido" || accion == "envidoenvido" || accion == "faltaenvido" || accion == "realenvido")
                    {
                        Clients.Client(jug.IdConexion).mostrarOpcionesEnvido();

                    }
                    else
                    {
                        Clients.Client(jug.IdConexion).mostrarOpcionesTruco();
                    }
                }
            }       
        }

        //ACTUALIZA EN PANTALLA LOS PUNTAJES LUEGO DE JUGAR EL ENVIDO
        public void MostrarPuntajes() 
        {
            List<Jugador> Equipo1 = new List<Jugador>();
            List<Jugador> Equipo2 = new List<Jugador>();
            Clients.All.limpiarpuntos();
            foreach (var jug in juego.Jugadores)
            {
                if (jug.Equipo == 1)
                {
                    Equipo1.Add(jug);
                }
                else
                {
                    Equipo2.Add(jug);
                }
            }

           foreach (var jug in Equipo1)
           {
                Clients.Client(jug.IdConexion).mostrarpuntos("Ellos", juego.PuntajesEquipo2);
                Clients.Client(jug.IdConexion).mostrarpuntos("Nostros", juego.PuntajesEquipo1);
           }
           foreach (var jug in Equipo2)
           {
                Clients.Client(jug.IdConexion).mostrarpuntos("Ellos", juego.PuntajesEquipo1);
                Clients.Client(jug.IdConexion).mostrarpuntos("Nostros", juego.PuntajesEquipo2);
           }

        }

        //CUANDO SE TOCAN LOS BOTONES DE ABAJO A LA DERECHA, LLAMA AL METODO QUE EJECUTA LA ACCIÓN
        public void cantar(string accion)
        {
            Jugador jugador = juego.ObtenerJugador(Context.ConnectionId);
            string mensaje = "Jugador "+jugador.Nombre+" canto ACCION";

            //Mano mano = new Mano();

            Clients.Others.mostrarmensaje(mensaje);
            Clients.Caller.mostrarmensaje("Yo cante ACCION");

            Clients.Client(jugador.IdConexion).deshabilitarMovimientos();

            // Si el juego termino... (acá quizá iria la implementación del evento, que se declara en donde ctualiza puntajes)
            Clients.Client(jugador.IdConexion).mostrarMensajeFinal(true); // GANADOR (debería ser al equipo)
            Clients.Client(jugador.IdConexion).mostrarMensajeFinal(false); // PERDEDOR
            Clients.All.deshabilitarMovimientos();

            // Sino
            //Clients.All.limpiarpuntos();

            switch (accion)
            {
                case "me voy al mazo":
                    //Repartir()
                    //Puntos para el otro equipo
                    break;
                case "envido":
                    //permitir que solo puedan jugar los jugadores 3 y 4 de cada ronda
                    Clients.All.hidemazo();
                    SeleccionDelQuiero(jugador, accion);             
                    break;
                case "envidoenvido":
                    Clients.All.hidemazo();
                    SeleccionDelQuiero(jugador, accion);
                    break;
                case "faltaenvido":
                    Clients.All.hidemazo();
                    SeleccionDelQuiero(jugador, accion);
                    break;
                case "realenvido":
                    Clients.All.hidemazo();
                    SeleccionDelQuiero(jugador, accion);
                    break;
                case "truco":
                    SeleccionDelQuiero(jugador, accion);
                    break;
                case "retruco":
                    SeleccionDelQuiero(jugador, accion);
                    break;
                case "vale4":
                    SeleccionDelQuiero(jugador, accion);
                    break;
            }
        }
        // PARA ENVIDO FALTA ASIGNAR MANO -> VER

        public void EjecutarAccion(string accion, bool confirmacion)
        {
            Jugador jugador = juego.ObtenerJugador(Context.ConnectionId);
            string mensaje;
            if (confirmacion)
            {
                mensaje = "Jugador " + jugador.Nombre + " acepto la ACCION";
                Clients.All.mostrarmensaje(mensaje);
                //Mano mano = new Mano();
                switch (accion) //SEGÚN LA ACCIÓN VAN A CAMBIAR LOS PUNTOS NOMÁS
                {
                    case "Envido":
                        Clients.All.showmazo();
                        Clients.Client(jugador.IdConexion).habilitarMovimientos();
                        Jugador GanadorEnvido = Mano.jugarEnvido(juego.Jugadores); //Devuelve el ganador del envido
                        List<string> mensajes = juego.Mensajes(GanadorEnvido); //este metodo genera los mensajes: GANDOR ENVIDO Y PUNTAJES DE TODOS                  
                        Clients.All.mostrarpuntosenvido(mensajes[0]); //muestra los puntos de todos
                        Clients.All.mostrarganadorenvido(mensajes[1]);//muestra los puntos del ganador
                        //ACTUALIZAR PUNTAJES:
                        juego.ActualizarPuntajes(GanadorEnvido.Equipo, accion);
                        MostrarPuntajes();
                        //OCULTAR EL QUIERO CUANDO TERMINA      
                        Clients.All.ocultarOpcionesEnvido();
                        break;
                    case "EnvidoEnvido":
                        Clients.All.showmazo();
                        Clients.Client(jugador.IdConexion).habilitarMovimientos();
                        GanadorEnvido = Mano.jugarEnvido(juego.Jugadores); //Devuelve el ganador del envido
                        List<string> mensajes1 = juego.Mensajes(GanadorEnvido); //este metodo  genera los mensajes                   
                        Clients.All.mostrarpuntosenvido(mensajes1[0]); //muestra los puntos de todos
                        Clients.All.mostrarganadorenvido(mensajes1[1]);//muestra los puntos del ganador
                        //ACTUALIZAR PUNTAJES:
                        juego.ActualizarPuntajes(GanadorEnvido.Equipo, accion);
                        MostrarPuntajes();
                        //OCULTAR EL QUIERO CUANDO TERMINA      
                        Clients.All.ocultarOpcionesEnvido();
                        break;
                    case "RealEnvido":
                        Clients.All.showmazo();
                        Clients.Client(jugador.IdConexion).habilitarMovimientos();
                        GanadorEnvido = Mano.jugarEnvido(juego.Jugadores); //Devuelve el ganador del envido
                        List<string> mensajes2 = juego.Mensajes(GanadorEnvido); //este metodo  genera los mensajes                   
                        Clients.All.mostrarpuntosenvido(mensajes2[0]); //muestra los puntos de todos
                        Clients.All.mostrarganadorenvido(mensajes2[1]);//muestra los puntos del ganador
                        //ACTUALIZAR PUNTAJES:
                        juego.ActualizarPuntajes(GanadorEnvido.Equipo, accion);
                        MostrarPuntajes();
                        //OCULTAR EL QUIERO CUANDO TERMINA      
                        Clients.All.ocultarOpcionesEnvido();
                        break;
                    case "FaltaEnvido":
                        Clients.All.showmazo();
                        Clients.Client(jugador.IdConexion).habilitarMovimientos();
                        GanadorEnvido= Mano.jugarEnvido(juego.Jugadores); //Devuelve el ganador del envido
                        List<string> mensajes3 = juego.Mensajes(GanadorEnvido); //este metodo genera los mensajes                   
                        Clients.All.mostrarpuntosenvido(mensajes3[0]); //muestra los puntos de todos
                        Clients.All.mostrarganadorenvido(mensajes3[1]);//muestra los puntos del ganador
                        //ACTUALIZAR PUNTAJES:
                        juego.ActualizarPuntajes(GanadorEnvido.Equipo, accion);
                        MostrarPuntajes();
                        //OCULTAR EL QUIERO CUANDO TERMINA      
                        Clients.All.ocultarOpcionesEnvido();
                        break;
                    case "Truco":
                        break;
                    case "ReTruco":
                        break;
                    case "Vale4":
                        break;                   
                }
                //PARA QUE NO PUEDA CANTAR EL ENVIDO DE NUEVO (SIN IMPORTAR SI LO QUE SE JUGO FUE ENVIDO O TRUCO) -> VALIDAR QUE PUEDAN CANTAR ENVIDO ANTES DE TRUCO
                Clients.All.ocultarBotonesEnvido();
            }
            else
            {
                mensaje = "Jugador " + jugador.Nombre + " rechazo la ACCION";
                Clients.All.mostrarmensaje(mensaje);
                Clients.All.ocultarOpcionesEnvido();
            }

            //PARA QUE CUANDO FINALICE EL ENVIDO SE PUEDA CONTINUAR NORMALMENTE LA MANO
            HabilitarCartas();
        }

        public void JugarCarta(string codigoCarta) //quien le pasa este parámetro?
        {
            Jugador Jugador = juego.ObtenerJugador(Context.ConnectionId);
            var mano = juego.Manos[juego.Manos.Count() - 1];
            if (Jugador.Turno)
            {
                //VALORES PARA CREAR EL SELECTOR
                Carta carta = Jugador.Cartas.Find(x => x.Codigo == codigoCarta);
                //int CartaElegida = mano.NumeroDeRonda;
                int CartaElegida = juego.DevolverNumeroDeRonda(); //VER
                Clients.All.mostrarCarta(carta, Jugador.NombreInterno, CartaElegida);
                mano.AgregarCartaJugada(carta); //Añade la carta que juega ese jugador a una lista para compararla con la de los demás jugadores
                juego.PasarTurnoAlSiguiente(); //Pasa el turno al siguiente jugador
            }

            if (mano.ValidarCartasJugadas())
            {
                var jugadorGanador = mano.JugadorGanador(juego.Jugadores);
                juego.AsignarTurno(jugadorGanador);
                juego.Manos[juego.Manos.Count - 1].EquipoGanadorMano = jugadorGanador.Equipo; //Estable el equipo que gano esa mano en base al jugador que tenia la carta más alta

                if (juego.RondaGanada()) //Ver que hace este método
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
                    MostrarPuntajes(); 
                    Repartir();
                }
                Mano nuevamano = new Mano();
                nuevamano.NumeroDeRonda = mano.NumeroDeRonda + 1;      
                juego.Manos.Add(nuevamano);
                juego.numeroDeRonda = juego.numeroDeRonda + 1;
                //QUITAR LOS BOTONES DE ENVIDO DESPUES DE LA PRIMER MANO

                if (juego.DevolverNumeroDeRonda() > 1)
                {
                    Clients.All.ocultarBotonesEnvido();
                }
            }
            //Jugador.Cartas.Remove(carta);  //le saco la carta de la lista al jugador
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

