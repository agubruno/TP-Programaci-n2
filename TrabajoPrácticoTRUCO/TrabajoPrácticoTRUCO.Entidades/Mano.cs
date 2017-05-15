using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TrabajoPrácticoTRUCO.Entidades
{
    public enum Participantes { jugador1, jugador2, jugador3, jugador4 }

    public class Mano
    {
        public Participantes TieneLaMano { get; set; }

        public Mano()
        {
            Inicia();
        }

        internal List<Jugador> Repartir(List<Carta> cartas, List<Jugador> jugadores)
        {
            int indice = 0;
            foreach (var jugador in jugadores)
            {               
                for (int i = 0; i < 2; i++)
                {
                    jugador.Cartas.Add(cartas[indice]);
                    indice++;
                }
            }
            return jugadores;
        }

        private void Inicia()
        {
            Random random = new Random();
            int indice = random.Next(0, 3);
            TieneLaMano = (Participantes)indice;
        } 

        internal void PasarMano()
        {
            var indJugador = (int)TieneLaMano;
            if (indJugador==3)
            {
                indJugador = 0;
            }
            else
            {
                indJugador++;
            }
            TieneLaMano = (Participantes)indJugador;
        } 
        
                    
    }
}
