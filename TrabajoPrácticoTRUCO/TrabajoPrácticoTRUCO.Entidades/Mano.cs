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
        private Jugador ganadorEnvido;
        public Jugador GanadorEnvido { get { return ganadorEnvido; } }
        public List<Carta> CartasEspeciales { get; set; }


        public Mano()
        {
            Inicia();
            CartasEspeciales = new List<Carta>();
            Carta macho = new Carta(palos.Espada, 1);
            CartasEspeciales.Add(macho);
            Carta hembra = new Carta(palos.Basto, 1);
            CartasEspeciales.Add(hembra);
            Carta anchoE = new Carta(palos.Espada, 7);
            CartasEspeciales.Add(anchoE);
            Carta anchoO = new Carta(palos.Oro, 7);
            CartasEspeciales.Add(anchoO);
        }

        public List<Jugador> Repartir(List<Carta> cartas, List<Jugador> jugadores)
        {
            int indice = 0;
            foreach (var jugador in jugadores)
            {
                for (int i = 0; i < 3; i++)
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

            List<Carta> cartasacomparar = new List<Carta>();
            cartasacomparar.Add(carta1);
            cartasacomparar.Add(carta2);

            foreach (var carta in CartasEspeciales)
            {
                foreach (var cart2 in cartasacomparar)
                {
                    if (carta == carta2)
                    {
                        return carta;
                    }
                }
            }

            if (numero1 >= 1 && numero1 <= 3)
            {
                if (numero2 >= 1 && numero2 <= 3)
                {
                    if (numero1 > numero2)
                    {
                        return carta1;
                    }
                    else
                    {
                        return carta2;
                    }
                }
                return carta1;

            }

            if (numero2 >= 1 && numero2 <= 3)
            {
                if (numero1 >= 1 && numero1 <= 3)
                {
                    if (numero1 > numero2)
                    {
                        return carta1;
                    }
                    else
                    {
                        return carta2;
                    }
                }
                return carta2;

            }



            if (numero1 > numero2)
            {
                return carta1;
            }

            return carta2;
        }



        public void /*Jugador - si queremos que devuelva jug */jugarEnvido(List<Jugador> jugadores)
        {
            int max = 0;
            Jugador ganador = jugadores[0];

            //LE ASIGNA A CADA JUGADOR SUS PUNTOS DEL ENVIDO
            foreach (var jugador in jugadores)
            {
                jugador.PuntosEnvido = CalcularPuntosEnvido(jugador.Cartas);
            }

            //BUSCA EL JUGADOR CON MAYOR PUNTAJE
            foreach (var jugador in jugadores)
            {
                if (jugador.PuntosEnvido > max)
                {
                    max = jugador.PuntosEnvido;
                    ganador = jugador;
                }
            }

            //EN CASO DE QUE HAYA MAS DE UN JUGADOR CON EL MAXIMO PUNTAJE BUSCA EL GANADOR POR LA MANO
            List<Jugador> ganadores = jugadores.FindAll(x => x.PuntosEnvido == ganador.PuntosEnvido);
            if (ganadores.Count() > 1)
            {
                foreach (var jugador in ganadores)
                {
                    if (jugador.EsMano)
                    {
                        ganador = jugador;
                    }
                }
            }

            ganadorEnvido = ganador;
            //return ganador;//esta línea no va, solo para probar
        }

        public int CalcularPuntosEnvido(List<Carta> cartas) //lo calcula por cada jugador, deberia almacenarse en un arreglo los resultados parciales de cada jugador y determinar el ganador
        {
            int[] puntos = new int[3];

            for (int i = 0; i < 3; i++)
            {
                foreach (var carta in cartas)
                {
                    if (cartas[i] != carta)
                    {
                        if (cartas[i].Palo == carta.Palo)
                        {
                            puntos[i] = 20;
                            if (cartas[i].Numero < 10)
                            {
                                puntos[i] = puntos[i] + cartas[i].Numero;
                            }
                            if (carta.Numero < 10)
                            {
                                puntos[i] = puntos[i] + carta.Numero;
                            }
                        }
                    }
                }
            }
            return puntos.Max();
        }
    }
}

