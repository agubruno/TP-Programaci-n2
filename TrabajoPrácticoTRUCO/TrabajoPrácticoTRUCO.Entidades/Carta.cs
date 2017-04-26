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
        private Carta()
        {
            Cartas = new List<Carta>();
        }

        public palos Palo { get; set; }
        public int Numero { get; set; }
        private static List<Carta> Cartas { get; set; }

        public static Carta CrearCarta(palos palo, int numero)
        {
            if ((numero >= 1 && numero <= 7) || (numero >= 10 && numero <= 12))
            {
                return Carta.ObtenerCarta(palo, numero);
            }

            throw new NotSupportedException("El valor ingresado no corresponde a un valor valido."); //ver tipo de excepcion          
        }

        private static Carta ObtenerCarta(palos palo, int numero) //ver si tiene que ser estatico
        {
            foreach (var carta in Cartas)
            {
                if (carta.Palo == palo && carta.Numero == numero)
                {
                    //throw new NotSupportedException("No se puede agregar un objeto duplicado.");
                    return carta;
                }
            }
            Carta nuevaCarta = new Carta();
            nuevaCarta.Numero = numero;
            nuevaCarta.Palo = palo;
            Cartas.Add(nuevaCarta);
            return nuevaCarta;
        }
    }
}
