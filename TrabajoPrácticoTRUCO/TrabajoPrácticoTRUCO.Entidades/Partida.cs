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
        public int PuntajesEquipo1 { get; set; }
        public int PuntajesEquipo2 { get; set; }
        //public List<int> Puntajes { get; set; } //PUNTAJE DE CADA EQUIPO (1 Y 2) Y TERCERO TOPE.
        public List<Mano> Manos { get; set; }
        private bool HayGanador { get; set; }

        public Partida()
        {
            Manos = new List<Mano>();
            mazo = new Mazo();
            Jugadores = new List<Jugador>();
            HayGanador = false;     
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

        public void AsignarTurno()
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





        public void NuevaMano()
        {
            if (HayGanador==false)
            {
                Mano nuevamano = new Mano();
                nuevamano.PasarMano();
                this.Jugadores = nuevamano.Repartir(mazo.MezclarMazo(),this.Jugadores);   
            }
        }
    }
}
