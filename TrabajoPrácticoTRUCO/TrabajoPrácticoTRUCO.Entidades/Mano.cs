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
        private List<Carta> Caartas { get; set; } //VER

        public Mano(List<Carta> cartas)
        {
            Caartas = cartas; //ver
        }


        
    }
}
