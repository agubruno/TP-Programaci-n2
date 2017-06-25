using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoPrácticoTRUCO.Entidades
{
    public enum users { user1, user2, user3, user4 }
    public class Partida
    {
        public Mazo mazo { get; set; }
        public List<Jugador> Jugadores { get; set; }
        private int puntajesEquipo1;
        private int puntajesEquipo2;
        public int numeroDeRonda { get; set; }
        public int PuntajesEquipo1 { get { return puntajesEquipo1; } }
        public int PuntajesEquipo2 { get { return puntajesEquipo2; } }
        public Jugador ManoJugador { get; set; }
        public List<Mano> Manos { get; set; }
        private bool HayGanador { get; set; }

        public Partida()
        {
            Manos = new List<Mano>();
            mazo = new Mazo();
            Jugadores = new List<Jugador>();
            HayGanador = false;
            numeroDeRonda = 1;
        }
   
        //DEVUELVE EL MENSAJE CON EL GANADOR DEL ENVIDO Y CON LOS PUNTAJES DE TODOS LOS JUGADORES
        public List<string> Mensajes(Jugador GanadorEnvido) //PUEDE RECCIBIR COMO PARAMETRO QUE SE JUGO, ASÍ DETERMINA LOS PUNTAJES
        {
            List<string> mensajes = new List<string>();
            int[] puntos = new int[4];
            string[] nombres = new string[4];
            for (int i = 0; i < Jugadores.Count; i++)
            {
                puntos[i] = Jugadores[i].PuntosEnvido;
                nombres[i] = Jugadores[i].Nombre;
            }
            //MENSAJE PUNTOS Y NOMBRES DE TODOS LOS JUGADORES
            string mensaje = "Los puntos son: " + nombres[0] + " : " + puntos[0].ToString() + " - " + nombres[1] + " : " + puntos[1].ToString() + " - " + nombres[2] + " : " + puntos[2].ToString() + " - " + nombres[3] + " : " + puntos[3].ToString();
            mensajes.Add(mensaje);  
            //MENSAJE CON LOS PUNTOS DEL GANADOR DEL ENVIDO      
            string mensaje1 = "El ganador del envido es: " + GanadorEnvido.Nombre + " , sus puntos son: " + GanadorEnvido.PuntosEnvido.ToString();
            mensajes.Add(mensaje1);
            return mensajes;

        }
        //ACTUALIZAR PUNTAJES
        public void ActualizarPuntajes(int equipo, string accion)
        {        
            switch (accion)
            {
                case "Envido":
                    if (equipo == 1)
                    {
                        puntajesEquipo1 = puntajesEquipo1 + 2;
                    }
                    else
                    {
                        puntajesEquipo2 = puntajesEquipo2 + 2;
                    }
                    break;
                case "EnvidoEnvido":
                    if (equipo == 1)
                    {
                        puntajesEquipo1 = puntajesEquipo1 + 4;
                    }
                    else
                    {
                        puntajesEquipo2 = puntajesEquipo2 + 4;
                    }
                    break;
                case "RealEnvido": //VER CUANDO SUMA 5
                    if (equipo == 1)
                    {
                        puntajesEquipo1 = puntajesEquipo1 + 3;
                    }
                    else
                    {
                        puntajesEquipo2 = puntajesEquipo2 + 3;
                    }
                    break;
                //VER FALTA ENVIDO
                default:
                    //if (equipo == 1)
                    //{
                    //    PuntajesEquipo1 = PuntajesEquipo1 + 2;
                    //}
                    //else
                    //{
                    //    PuntajesEquipo2 = PuntajesEquipo2 + 2;
                    break;

            }
        }

        public bool VerificarDisponibilidad()
        {
            if (Jugadores.Count == 4)
            {
                    return false;
            }
            return true;
        } 
        
        public void AsignarEquipos()
        {
            Jugadores[0].Equipo = 1;
            Jugadores[0].Turno = true;
            Jugadores[1].Equipo = 2;
            Jugadores[2].Equipo = 1;
            Jugadores[3].Equipo = 2;
        }

        public void InsertarJugador(string nombre, string IdConexion)
        {
            int cont = 1;
            foreach (var jugador1 in Jugadores)
            {
                if (jugador1.Nombre != null)
                {
                    cont = cont + 1;
                }
            }
            Jugador jugador = new Jugador();
            jugador.IdConexion = IdConexion;
            jugador.Nombre = nombre;
            jugador.NombreInterno = "user" + cont.ToString();
            Jugadores.Add(jugador);

        }

        public void PasarTurnoAlSiguiente()
        {
            for (int i = 0; i < 4; i++)
            {
                if (Jugadores[i].Turno)
                {
                    Jugadores[i].Turno = false;
                    if (i == 3)
                    {
                        Jugadores[0].Turno = true;
                        break;
                    }
                    else
                    {
                        Jugadores[i + 1].Turno = true;
                        break;
                    }
                }
            }           
        }

        public void AsignarTurno(Jugador jugadorGanador)
        {
            for (int i = 0; i < 4; i++)
            {
                if (Jugadores[i].Turno)
                {
                    Jugadores[i].Turno = false;
                }
                if (jugadorGanador.IdConexion == Jugadores[i].IdConexion)
                {
                    Jugadores[i].Turno = true;
                }
            }
        }

        public void NuevaMano()
        {
            if (HayGanador==false)
            {
                Mano nuevamano = new Mano();
                nuevamano.PasarMano();
                this.Jugadores = nuevamano.Repartir(mazo.MezclarMazo(),this.Jugadores);   
            }
        }

        public void Puntajes (Jugador GanadorMano)
        {
            if (GanadorMano.Equipo == 1)
            {
                puntajesEquipo1 = puntajesEquipo1 + 1;
            }
            else
            {
                puntajesEquipo2 = puntajesEquipo2 + 1;
            }
        }

        public bool RondaGanada ()
        {
            if (Manos[Manos.Count -1].NumeroDeRonda == 2)
            {
                if (Manos[Manos.Count - 1].EquipoGanadorMano == Manos[Manos.Count - 2].EquipoGanadorMano)
                {
                    RemoverCartas();
                    return true;
                }
            }
            else
            {
                if (Manos[Manos.Count - 1].NumeroDeRonda == 3)
                {
                    if (Manos[Manos.Count - 1].EquipoGanadorMano == Manos[Manos.Count - 2].EquipoGanadorMano)
                    {
                        RemoverCartas();
                        return true;
                    }
                    if (Manos[Manos.Count -1].EquipoGanadorMano == Manos[Manos.Count - 3].EquipoGanadorMano)
                    {
                        RemoverCartas();
                        return true;
                    }
                 
                }
            }
            return false;
        }

        public void RemoverCartas()
        {
            foreach (var jugador in Jugadores)
            {
                jugador.Cartas.RemoveAll(x => x.Numero > 0);

            }
        }

        public Jugador ObtenerJugador(string ConnectionId)
        {
            return  Jugadores.Find(x => x.IdConexion == ConnectionId);
        }

        public int DevolverNumeroDeRonda()
        {
            if (this.numeroDeRonda>3)
            {
                numeroDeRonda = 1;
            }
            return numeroDeRonda;
        }

        public bool PuedeCantarEnvido (Jugador jugador)
        {
            int indice = Jugadores.FindIndex(x => x.IdConexion == jugador.IdConexion);

            if (jugador.IdConexion == ManoJugador.IdConexion)
            {
                return false;
            }

            if (indice == 0)
            {
                if (ManoJugador.IdConexion == Jugadores[3].IdConexion)
                {
                    return false;
                }
            }
            else
            {
                if (ManoJugador.IdConexion == Jugadores[indice - 1].IdConexion)
                {
                    return false;
                }
            }

            return true;
            
        }
    }
}
