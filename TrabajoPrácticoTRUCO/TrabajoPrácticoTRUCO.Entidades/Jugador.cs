using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoPrácticoTRUCO.Entidades
{
    public class Jugador
    {
        public string Nombre { get; set; }
        public string Nombre_Usuario { get; set; }
        //private Estadistica estadisticas;
        //public Estadistica Estadisticas { get { return estadisticas; } }
        public List<Carta> Cartas { get; set; }
        public int Equipo { get; set; }

        public Jugador()
        {
            Cartas = new List<Carta>();
        }

    }
}
