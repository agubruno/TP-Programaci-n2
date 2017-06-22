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
            this.Imagen1 = "/Images/Clasicas/";
            switch (palo)
            {
                case palos.Espada:
                    this.Imagen1 = Imagen1 + "e" + numero.ToString() +".jpg";
                    break;
                case palos.Basto:
                    this.Imagen1 = Imagen1 + "b" + numero.ToString() + ".jpg";
                    break;
                case palos.Oro:
                    this.Imagen1 = Imagen1 + "o" + numero.ToString() + ".jpg";
                    break;
                case palos.Copa:
                    this.Imagen1 = Imagen1 + "c" + numero.ToString() + ".jpg";
                    break;
                default:
                    break;
            }
            this.Imagen2 = "/Images/Avengers/";
            switch (palo)
            {
                case palos.Espada:
                    this.Imagen2 = Imagen2 + numero.ToString()+"EspadaA" + ".jpg";
                    break;
                case palos.Basto:
                    this.Imagen2 = Imagen2 +  numero.ToString()+"BastoA" + ".jpg";
                    break;
                case palos.Oro:
                    this.Imagen2 = Imagen2 +  numero.ToString()+"OroA" + ".jpg";
                    break;
                case palos.Copa:
                    this.Imagen2 = Imagen2 +  numero.ToString()+"CopaA" + ".jpg";
                    break;
                default:
                    break;
            }
            this.Imagen3 = "/Images/Pokemon/";
            switch (palo)
            {
                case palos.Espada:
                    this.Imagen3 = Imagen3 + numero.ToString() + "EspadaP" + ".jpg";
                    break;
                case palos.Basto:
                    this.Imagen3 = Imagen3 + numero.ToString() + "BastoP" + ".jpg";
                    break;
                case palos.Oro:
                    this.Imagen3 = Imagen3 + numero.ToString() + "OroP" + ".jpg";
                    break;
                case palos.Copa:
                    this.Imagen3 = Imagen3 + numero.ToString() + "CopaP" + ".jpg";
                    break;
                default:
                    break;
            }

        }

        public palos Palo { get; }
        public int Numero { get;  }
        public string Codigo { get; set; }
        public string Imagen1 { get; set; }
        public string Imagen2 { get; set; }
        public string Imagen3 { get; set; }
    }
}
