using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoPrácticoTRUCO.Entidades
{
    public class Partida
    {
        public Mazo mazo { get; set; }
        public List<Jugador> Jugadores { get; set; }
        public List<int> Puntajes { get; set; } //PUNTAJE DE CADA EQUIPO (1 Y 2) Y TERCERO TOPE.
        public List<Mano> Manos { get; set; }
        private bool HayGanador { get; set; }

        public Partida()
        {
            mazo = new Mazo();
            Jugadores = new List<Jugador>();
            for (int i = 0; i < 4; i++)
            {
                Jugador jugador = new Jugador();
                Jugadores.Add(jugador);
            }
            HayGanador = false;     
        }
        public void ArmarEquipos()
        {
            Random random = new Random();
            int indice1 = random.Next(0, 3);
            int indice2 = random.Next(0, 3);
            if (indice1!=indice2)
            {
                Jugadores[indice1].Equipo= 1;
                Jugadores[indice2].Equipo = 1;
                foreach (var jugador in Jugadores)
                {
                    if (jugador.Equipo==0)
                    {
                        jugador.Equipo = 2;
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
