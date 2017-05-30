﻿using System;
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
            if (indJugador == 3)
            {
                indJugador = 0;
            }
            else
            {
                indJugador++;
            }
            TieneLaMano = (Participantes)indJugador;
        }

        public Carta CompararCartas(Carta carta1, Carta carta2)
        {
            int numero1 = carta1.Numero;
            int numero2 = carta2.Numero;

            List<Carta> cartas = new List<Carta>();

            Carta macho = new Carta(palos.Espada, 1);
            cartas.Add(macho);
            Carta hembra = new Carta(palos.Basto, 1);
            cartas.Add(hembra);
            Carta anchoE = new Carta(palos.Espada, 7);
            cartas.Add(anchoE);
            Carta anchoO = new Carta(palos.Oro, 7);
            cartas.Add(anchoO);

            List<Carta> cartas2 = new List<Carta>();
            cartas2.Add(carta1);
            cartas2.Add(carta2);

            foreach (var carta in cartas)
            {
                foreach (var cart2 in cartas2)
                {
                    if (carta == carta2)
                    {
                        return carta;
                    }
                }
            }

            if ((numero1 >= 1 && numero1 <= 3) || (numero2 >= 1 && numero2 <= 3))
            {
                if (numero1 < numero2)
                {
                    return carta1;
                }
                else
                {
                    return carta2;
                }
            }

            if (numero1 > numero2)
            {
                return carta1;
            }

            return carta2;
        }
                    
    }
}
