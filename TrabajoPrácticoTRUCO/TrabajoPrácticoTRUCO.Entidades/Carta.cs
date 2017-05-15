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
        internal palos Palo { get; set; }
        internal int Numero { get; set; }
    }
}
