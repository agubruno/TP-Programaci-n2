using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoPrácticoTRUCO.Entidades
{
    public class GanadorEventArgs: EventArgs
    {
        public int Equipo { get; set; } 
        public int Puntos { get; set; }
        public GanadorEventArgs(int equipo, int puntos)
        {
            this.Equipo = equipo;
            this.Puntos = puntos;
        }
    }
}
