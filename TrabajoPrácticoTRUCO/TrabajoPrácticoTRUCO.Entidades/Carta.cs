using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoPrácticoTRUCO.Entidades
{
    public enum palos { Espada, Basto, Oro, Copa }
    //public enum numeros -> ver

    public class Carta
    {
        public Carta(palos palo, int numero)
        {
            this.Palo = palo;
            this.Numero = numero;
        }

        internal palos Palo { get; }
        internal int Numero { get; }
    }
}
