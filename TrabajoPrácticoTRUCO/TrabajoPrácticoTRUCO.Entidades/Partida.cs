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
                if (i == 0 || i == 2)
                {
                    jugador.Equipo = 1;
                }
                else
                {
                    jugador.Equipo = 2;
                }
                Jugadores.Add(jugador);
            }
            HayGanador = false;     
        }

        public bool AgregarJugador(string nombre)
        {
            int cont = 0;
            foreach (var jugador in Jugadores)
            {
                if (jugador.Nombre == "")
                {
                    Random random = new Random();
                    int indice = random.Next(0, 3);
                    while (Jugadores[indice].Nombre != "")
                    {
                        indice = random.Next(0, 3);
                    }
                    Jugadores[indice].Nombre = nombre;
                    Jugadores[indice].NombreInterno = "user" + cont.ToString();
                    return true;
                }
            }
            return false;
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
