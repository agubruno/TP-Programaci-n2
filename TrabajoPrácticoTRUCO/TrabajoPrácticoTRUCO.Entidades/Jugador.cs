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

        public string NombreInterno { get; set; } //user1, user2, user3, user4

        public string IdConexion { get; set; }

        public bool Turno { get; set; }

        //public int Orden { get; set; }

        //private Estadistica estadisticas;
        //public Estadistica Estadisticas { get { return estadisticas; } }
        public int PuntosEnvido { get; set; }
        public bool EsMano { get; set; }
        public List<Carta> Cartas { get; set; }
        public int Equipo { get; set; }

        public Jugador()
        {
            Cartas = new List<Carta>();
        }

    }
}
