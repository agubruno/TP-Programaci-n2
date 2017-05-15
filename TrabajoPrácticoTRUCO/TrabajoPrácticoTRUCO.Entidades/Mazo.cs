using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabajoPrácticoTRUCO.Entidades
{
    public class Mazo
    {
        //private  List<Carta> cartas;
        private List<Carta> Cartas { get; set;/* { return cartas; }*/ }

        public Mazo()
        {

            Cartas = new List<Carta>();
            //cartas = this.MezclarMazo(); //ver
            CrearCarta();

        }

        public List<Carta> ObtenerMazo()
        {
            return Cartas;
        }

        private void CrearCarta()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 1; j < 13; j++)
                {
                    if (j != 8 && j != 9)
                    {
                        Carta carta = new Carta();
                        carta.Palo = (palos)i;
                        carta.Numero = j;
                        Cartas.Add(carta);
                    }
                }
            }
        }

        public List<Carta> MezclarMazo() //ver
        {
            Random random = new Random();
            int indice = 0;
            int indice1 = 0;
            Carta carta = new Carta();
            for (int i = 0; i < 40; i++)
            {
                indice = random.Next(0, 39);
                indice1 = random.Next(0, 39);
                carta = Cartas[indice];
                Cartas[indice] = Cartas[indice1];
                Cartas[indice1] = carta;
            }
            return Cartas;
        }
    }

}
