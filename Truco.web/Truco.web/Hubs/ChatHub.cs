using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Truco.web.Hubs
{
    //public class ChatHub : Hub
    //{
    //    public void EnviarMensaje(string nombre, string texto)
    //    {
    //        if ((texto != "") && (nombre != "")) //PARA QUE ESTE CORRECTO DEBERÍA DISPARAR EVENTO CUANDO NO SE PUEDE ENVIAR
    //        {
    //            Clients.All.mostrarMensaje(nombre, texto);
    //        }
            
    //    }

    //    public void EnviarMensajePrivado(string nombre, string texto)
    //    {
    //        if ((texto != "") && (nombre != ""))
    //        {
    //            Clients.Caller.mostrarMensaje(nombre, texto);
    //        }
    //    }
    //}
}