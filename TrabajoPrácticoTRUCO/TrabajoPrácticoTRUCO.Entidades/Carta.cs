using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
            this.Codigo = numero.ToString() + palo.ToString();
            this.Imagen = "/Images/";
            switch (palo)
            {
                case palos.Espada:
                    this.Imagen = Imagen + "e" + numero.ToString() +".jpg";
                    break;
                case palos.Basto:
                    this.Imagen = Imagen + "b" + numero.ToString() + ".jpg";
                    break;
                case palos.Oro:
                    this.Imagen = Imagen + "o" + numero.ToString() + ".jpg";
                    break;
                case palos.Copa:
                    this.Imagen = Imagen + "c" + numero.ToString() + ".jpg";
                    break;
                default:
                    break;
            }
        }

        public palos Palo { get; }
        public int Numero { get;  }
        public string Codigo { get; set; }
        public string Imagen { get; set; }
    }
}
