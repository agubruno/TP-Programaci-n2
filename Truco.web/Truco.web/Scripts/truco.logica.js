var trucoHub = {};

//ocultar botones de envido (yo)
function ocultarBotonesTruco() { //LLAMAR AL PRINCIPIO (AL INICIALIZAR)
    //$("#bottonCartasdePokemon").hide();
    $("#bottonTruco").hide();
    $("#bottonRetruco").hide();
    $("#bottonVale4").hide();
}

//ocultar botones de envido (yo)
function ocultarBotonesEnvido() { //LLAMAR AL PRINCIPIO (AL INICIALIZAR)
    //$("#bottonCartasdePokemon").hide();
    $("#bottonEnvido").hide();
    $("#bottonEnvidoEnvido").hide();
    $("#bottonRealEnvido").hide();
    $("#bottonFaltaEnvido").hide();
}

//Habilitar botones truco (yo)
function habilitarBotonesTruco1() { //LLAMAR SIEMRE QUE SEA EL JUGADOR NUMERO 3 Y 4
    //$("#bottonCartasdePokemon").hide();
    $("#bottonTruco").show();
}
//Habilitar botones REtruco (yo)
function habilitarBotonesRetruco() { //LLAMAR SIEMRE QUE SEA EL JUGADOR NUMERO 3 Y 4
    //$("#bottonCartasdePokemon").hide();
    $("#bottonReTruco").show();
}
//Habilitar botones VALEcuarto (yo)
function habilitarBotonesVale4() { //LLAMAR SIEMRE QUE SEA EL JUGADOR NUMERO 3 Y 4
    //$("#bottonCartasdePokemon").hide();
    $("#bottonVale4").show();
}

//Habilitar botones envido (yo)
function habilitarBotonesEnvido() { //LLAMAR SIEMRE QUE SEA EL JUGADOR NUMERO 3 Y 4
    //$("#bottonCartasdePokemon").hide();
    $("#bottonEnvido").show();
    $("#bottonEnvidoEnvido").show();
    $("#bottonRealEnvido").show();
    $("#bottonFaltaEnvido").show();
}
// Oculta las acciones del envido.
function ocultarSeccionesEnvido() {
    $("#envidoRegion").hide();
    $("#envidoEnvidoRegion").hide();
    $("#faltaEnvidoRegion").hide();
    $("#quieroEnvidoRegion").hide();
    $("#realEnvidoRegion").hide();
}

// Oculta las acciones del truco.
function ocultarSeccionesTruco() {
    $("#trucoRegion").hide();
    $("#retrucoRegion").hide();
    $("#vale4Region").hide();
}

// Oculta todas las acciones.
function ocultarMovimientos() {
    $("#movements").hide();
}

// Visualiza todas las acciones.
function mostrarMovimientos() {
    $("#movements").show();
}

// Limpia el tablero.
function Limpiar() {
    // Ocultamos las acciones.
    ocultarSeccionesEnvido();
    ocultarSeccionesTruco();

    // Limpiamos las cartas del jugador.
    $("#cards").html("");

    // Volteamos las cartas de la mesa.
    $("[id*=user][id*=card]").attr("src", "Images/BACK.JPG");
}

// Inicializamos los componentes.
function inicializarComponentes() {
    $("#cards").hide();
    $("#movements").hide();
    $("#rightPanel").niceScroll();

    ocultarSeccionesEnvido();
    ocultarSeccionesTruco();
}

$(function () {
    // Inicializo los componentes.
    inicializarComponentes();

    // Creamos el hub.
    trucoHub = $.connection.truco;

    // Evento click del boton "ingresar jugador".
    $("#botonAgregarJugador").click(function () {
        var userName = $("#userName").val();
        $("#userRegion").hide()

        // Habilitar para el trabajo práctico.
        trucoHub.server.agregarJugador(userName);
    });

    $("#bottonMazo").click(function () {
        trucoHub.server.cantar("me voy al mazo");
    });

    // Visualizar nombre en el tablero.
    trucoHub.client.mostrarNombre = function (data) {
        var selector = "#" + data.NombreInterno;
        $(selector).html(data.Nombre);
    };

    // Visualizar nombre en el chat.
    trucoHub.client.mostrarnuevousuario = function (data) {
        $("#messages").prepend("<p>" + data + " se ha unido al juego!</p>");
    };
    //Deshabilito los botones del envido para que solo estén disponibles para los 2 últimos
    //trucoHub.client.ocultarBotonesEnvido = function () {
    //    ocultarBotonesEnvido();
    //}

    trucoHub.client.mostrarMensajeFinal = function (data) {
        if (data === true) {
            bootbox.alert("GANASTE!");
        } else {
            bootbox.alert("PERDISTE!");
        }
    };

    //habilita la opcion del REtruco (ver como hacer para que se habilte cuando yo quiero) POR MI
    trucoHub.client.habilitarBotonesTruco1 = function () {
        $("#bottonTruco").show();
    }
    //habilita la opcion del REtruco (ver como hacer para que se habilte cuando yo quiero) POR MI
    trucoHub.client.habilitarBotonesRetruco = function () {
        $("#bottonReTruco").show();
    }
    //habilita la opcion del VALECUATRO (ver como hacer para que se habilte cuando yo quiero) POR MI
    trucoHub.client.habilitarBotonesVale4 = function () {
        $("#bottonVale4").show();
    }

    // VER
    //deshabilita las opciones del truco (ver como hacer para que se habilte cuando yo quiero) POR MI
    trucoHub.client.ocultarBotonesTruco = function () {
        $("#bottonTruco").hide();
        $("#bottonReTruco").hide();
        $("#bottonVale4").hide();
    }
    //
    // VER
    //Habilita las opciones del truco (ver como hacer para que se habilte cuando yo quiero) POR MI
    trucoHub.client.ocultarOpcionesTruco = function () {
        $("#trucoRegion").hide();
        $("#retrucoRegion").hide();
        $("#vale4Region").hide();
    }
    //
    // VER
    //Habilita las opciones del envido (ver como hacer para que se habilte cuando yo quiero) POR MI
    trucoHub.client.ocultarOpcionesEnvido = function () {
        $("#envidoRegion").hide();
        $("#envidoEnvidoRegion").hide();
        $("#faltaEnvidoRegion").hide();
        $("#quieroEnvidoRegion").hide();
        $("#realEnvidoRegion").hide();
    }
    //
    // VER
    //Habilita las opciones del envido (ver como hacer para que se habilte cuando yo quiero) POR MI
    trucoHub.client.ocultarOpcionesTruco = function () {
        $("#trucoRegion").hide();
        $("#retrucoRegion").hide();
        $("#vale4Region").hide();
    }
    //
    trucoHub.client.mostrarOpcionesEnvido = function () {
        $("#envidoRegion").show();
        $("#envidoEnvidoRegion").show();
        $("#faltaEnvidoRegion").show();
        $("#quieroEnvidoRegion").show();
        $("#realEnvidoRegion").show();
    }
    //POR MI


    trucoHub.client.mostrarOpcionesTruco = function () {
        $("#trucoRegion").show();
        $("#retrucoRegion").show();
        $("#vale4Region").show();
    }


    //Mostrar ganador envido - POR MI
    trucoHub.client.mostrarganadorenvido = function (data) {
        alert(data);
    };

    //Mostrar puntos envido - POR MI
    trucoHub.client.mostrarpuntosenvido = function (data) {
        alert(data);
    };
    //oculta botones envido despues de la primer mano
    trucoHub.client.ocultarBotonesEnvido = function () {
        ocultarBotonesEnvido(); //o poner la definicion de una acá
    }
    //
    $("#bottonEnvido").click(function () {
        //$("#bottonEnvido").hide();
        trucoHub.server.cantar("envido");
    });

    $("#bottonEnvidoEnvido").click(function () {
        //$("#bottonEnvidoEnvido").hide();
        trucoHub.server.cantar("envidoenvido");
    });

    $("#bottonFaltaEnvido").click(function () {
        //$("#bottonFaltaEnvido").hide();
        trucoHub.server.cantar("faltaenvido");
    });

    $("#bottonRealEnvido").click(function () {
        //$("#bottonRealEnvido").hide();
        trucoHub.server.cantar("realenvido");
    });

    $("#bottonQuieroEnvido").click(function () {
        //$("#bottonQuieroEnvido").hide();

        trucoHub.server.ejecutarAccion("Envido", true);
    });

    $("#bottonNoQuieroEnvido").click(function () {
        //$("#bottonNoQuieroEnvido").hide();
        trucoHub.server.ejecutarAccion("Envido", false);
    });

    $("#bottonQuieroEnvidoEnvido").click(function () {
        //$("#bottonQuieroEnvidoEnvido").hide();
        trucoHub.server.ejecutarAccion("EnvidoEnvido", true);
    });

    $("#bottonNoQuieroEnvidoEnvido").click(function () {
        //$("#bottonNoQuieroEnvidoEnvido").hide();
        trucoHub.server.ejecutarAccion("EnvidoEnvido", false);
    });


    $("#bottonQuieroFaltaEnvido").click(function () {
        //$("#bottonQuieroFaltaEnvido").hide();
        trucoHub.server.ejecutarAccion("FaltaEnvido", true);
    });

    $("#bottonNoQuieroFaltaEnvido").click(function () {
        //$("#bottonNoQuieroFaltaEnvido").hide();
        trucoHub.server.ejecutarAccion("FaltaEnvido", false);
    });

    $("#bottonQuieroRealEnvido").click(function () {
        //$("#bottonQuieroRealEnvido").hide();
        trucoHub.server.ejecutarAccion("RealEnvido", true);
    });

    $("#bottonNoQuieroRealEnvido").click(function () {
        //$("#bottonNoQuieroRealEnvido").hide();
        trucoHub.server.ejecutarAccion("RealEnvido", false);
    });

    $("#bottonCantarPuntosEnvido").click(function () {
        trucoHub.server.cantarPuntos(lastAction, $("#envidoPoints").val());
    });

    $("#bottonTruco").click(function () {
        $("#bottonTruco").hide();
        trucoHub.server.cantar("truco");
    });

    $("#bottonReTruco").click(function () {
        $("#bottonReTruco").hide();
        trucoHub.server.cantar("retruco");
    });

    $("#bottonVale4").click(function () {
        $("#bottonVale4").hide();
        trucoHub.server.cantar("vale4");
    });

    $("#bottonQuieroTruco").click(function () {
        $("#bottonQuieroTruco").hide();
        trucoHub.server.ejecutarAccion("Truco", true);
    });

    $("#bottonNoQuieroTruco").click(function () {
        $("#bottonNoQuieroTruco").hide();
        trucoHub.server.ejecutarAccion("Truco", false);
    });

    $("#bottonQuieroReTruco").click(function () {
        $("#bottonQuieroReTruco").hide();
        trucoHub.server.ejecutarAccion("ReTruco", true);
    });

    $("#bottonNoQuieroReTruco").click(function () {
        $("#bottonNoQuieroReTruco").hide();
        trucoHub.server.ejecutarAccion("ReTruco", false);
    });

    $("#bottonQuieroVale4").click(function () {
        $("#bottonQuieroVale4").hide();
        trucoHub.server.ejecutarAccion("Vale4", true);
    });

    $("#bottonNoQuieroVale4").click(function () {
        $("#bottonNoQuieroVale4").hide();
        trucoHub.server.ejecutarAccion("Vale4", false);
    });

    trucoHub.client.mostrarmensaje = function (data) {
        $("#messages").prepend("<p>" + data + "</p>");
    };

    trucoHub.client.mostrarpuntos = function (equipo, puntos) {
        $("#punt").prepend("<p>" + puntos + "</p>");
        $("#punt").prepend("<p>" + equipo + "</p>");
    };

    trucoHub.client.limpiarpuntos = function () {
        $("#punt").html("");
    };

    trucoHub.client.mostrarCarta = function (carta, nombreInterno, cartaElegida) {
        var selector = "#" + nombreInterno + "_card" + cartaElegida;
        $(carta.Selector).attr("src", "");        
        $(selector).attr("src", carta.Imagen1);
    };

    trucoHub.client.mostrarCartas = function (data) {

        $.each(data,
            function (index, value) {

                console.log("mostrarCarta:");
                console.log(value);
                var img = $("<img width='100' />").attr({
                    'id': value.Codigo,
                    'src':value.Imagen1, 
                    'alt': value.Codigo,
                    'cc': value.Codigo
                }).appendTo($("#cards"));
            });

        $("#cards").show();
        $("#movements").show();
    };

    trucoHub.client.limpiarTablero = function () {
        Limpiar();
    };

    trucoHub.client.ocultarBottonTruco = function () {
        ocbtTruco();
    };

    trucoHub.client.habilitarMovimientos = function (data) {

        console.log("habilitarMovimientos");

        $("[cc]").bind("click",
            function (evt) {
                console.log("jugar carta");
                $(this).hide();
                trucoHub.server.jugarCarta($(this).attr("cc"));
            });
    };

    trucoHub.client.deshabilitarMovimientos = function (data) {

        console.log("deshabilitarMovimientos");
        $("[cc]").unbind("click");
    };

    trucoHub.client.showFaltaEnvidoOptions = function (data) {
        $("#faltaEnvidoRegion").show();
    };

    trucoHub.client.hideFaltaEnvidoOptions = function (data) {
        $("#faltaEnvidoRegion").hide();
    };

    trucoHub.client.showFaltaEnvidoBotton = function (data) {
        $("#bottonFaltaEnvido").show();
    };

    trucoHub.client.showEnvidoEnvidoOptions = function (data) {
        $("#envidoEnvidoRegion").show();
        $("#bottonRealEnvido").show();
        $("#bottonFaltaEnvido").show();
    };

    trucoHub.client.hideEnvidoEnvidoOptions = function (data) {
        $("#envidoEnvidoRegion").hide();
        $("#bottonRealEnvido").hide();
        $("#bottonFaltaEnvido").hide();
    };

    trucoHub.client.hideEnvidoBotton = function (data) {
        $("#bottonEnvido").hide();
    };
    trucoHub.client.hideRealEnvidoBotton = function (data) {
        $("#bottonRealEnvido").hide();
    };
    trucoHub.client.showRealEnvidoBotton = function (data) {
        $("#bottonRealEnvido").show();
    };
    trucoHub.client.showEnvidoBotton = function (data) {
        $("#bottonEnvido").show();
    };
    trucoHub.client.showEnvidoEnvidoBotton = function (data) {
        $("#bottonEnvidoEnvido").show();
    };
    trucoHub.client.showFaltaEnvidoBotton = function (data) {
        $("#bottonFaltaEnvido").show();
    };
    trucoHub.client.hideEnvidoEnvidoBotton = function (data) {
        $("#bottonEnvidoEnvido").hide();
    };
    trucoHub.client.hideEnvidoOptions = function (data) {
        $("#envidoRegion").hide();
        $("#bottonEnvido").hide();
        $("#bottonEnvidoEnvido").hide();
        $("#bottonFaltaEnvido").hide();
        $("#bottonRealEnvido").hide();
    };
    trucoHub.client.hideRealEnvidoOptions = function (data) {
        $("#realEnvidoRegion").hide()
        $("#bottonFaltaEnvido").hide();
    };
    trucoHub.client.showRealEnvidoOptions = function (data) {
        $("#realEnvidoRegion").show()
        $("#bottonFaltaEnvido").show();
    };
    trucoHub.client.showEnvidoOptions = function (data) {
        $("#envidoRegion").show()
        $("#bottonEnvido").hide();
        $("#bottonEnvidoEnvido").show();
        $("#bottonFaltaEnvido").show();
        $("#bottonRealEnvido").show();
    };

    trucoHub.client.showQuieroEnvido = function (data) {
        lastAction = data;
        $("#envidoRegion").hide()
        $("#quieroEnvidoRegion").show();
    };

    trucoHub.client.hidemazo = function (data) {
        $("#bottonMazo").hide();
    };
    trucoHub.client.showmazo = function (data) {
        $("#bottonMazo").show();
    };
    trucoHub.client.hideTrucoBotton = function (data) {
        $("#bottonTruco").hide();
    };

    trucoHub.client.hideReTrucoBotton = function (data) {
        $("#bottonReTruco").hide();
    };

    trucoHub.client.hideVale4Botton = function (data) {
        $("#bottonVale4").hide();
    };

    trucoHub.client.showTrucoBotton = function (data) {
        $("#bottonTruco").show();
    };

    trucoHub.client.showReTrucoBotton = function (data) {
        $("#bottonReTruco").show();
    };

    trucoHub.client.showVale4Botton = function (data) {
        $("#bottonVale4").show();
    };
    trucoHub.client.showTrucoOptions = function (data) {
        $("#trucoRegion").show();
        $("#bottonReTruco").show();
    };

    trucoHub.client.hideTrucoOptions = function (data) {
        $("#trucoRegion").hide();
        $("#bottonReTruco").hide();
    };
    trucoHub.client.hideTrucoRegion = function (data) {
        $("#trucoRegion").hide();
    };
    trucoHub.client.showReTrucoOptions = function (data) {
        $("#retrucoRegion").show();
        $("#bottonVale4").show();
    };
    trucoHub.client.hideReTrucoOptions = function (data) {
        $("#retrucoRegion").hide();
        $("#bottonVale4").hide();
    };
    trucoHub.client.hideReTrucoRegion = function (data) {
        $("#retrucoRegion").hide();
    };
    trucoHub.client.showVale4Options = function (data) {
        $("#vale4Region").show();
    };
    trucoHub.client.hideVale4Options = function (data) {
        $("#vale4Region").hide();
    };

    $("#bottonCartasEspañolas").click(function (carta) {
         //$("#bottonCartasEspañolas").hide();
         $("[id*=user]").attr("src", "Images/Clasicas/Back1.jpg");
         $("[id*=user]").attr("src", carta.Imagen1);              
        });
    $("#bottonCartasdeAvengers").click(function (carta) {
        //$("#bottonCartasdeAvengers").hide();
        $("[id*=user]").attr("src", "Images/Avengers/Back.jpg");
        $("[id*=user]").attr("src", carta.Imagen2);      
        });
    $("#bottonCartasdePokemon").click(function (carta) {
        //$("#bottonCartasdePokemon").hide();
        $("[id*=user]").attr("src", "Images/Pokemon/Back.jpg");
        $("[id*=user]").attr("src", carta.Imagen3);

        });

    //    $("#bottonCartasEspañolas").click(function (carta, nombreInterno, tipoMazo) {
    //    $("#bottonCartasEspañolas").hide();
    //    nombreInterno = $("[id*=user]");
    //    $("[id*=user]").attr("src", "Images/Clasicas/Back1.jpg");
    //    tipoMazo = "/Images/Clasicas";
    //    $("[id*=user]").attr("src", tipoMazo + carta.Imagen);
       
    //});
    //$("#bottonCartasdeAvengers").click(function (carta,nombreInterno,tipoMazo) {
    //    $("#bottonCartasdeAvengers").hide();
    //    nombreInterno = $("[id*=user]");
    //    $("[id*=user]").attr("src", "Images/Avengers/Back.jpg");
    //    tipoMazo = "/Images/Avengers";
    //    $("[id*=user]").attr("src", tipoMazo + carta.Imagen);
       
    //});
    //$("#bottonCartasdePokemon").click(function (carta,nombreInterno,tipoMazo) {
    //    $("#bottonCartasdePokemon").hide();
    //    nombreInterno = $("[id*=user]");
    //    $("[id*=user]").attr("src", "Images/Pokemon/Back.jpg");
    //    tipoMazo = "/Images/Pokemon";
    //    $("[id*=user]").attr("src", tipoMazo+carta.Imagen);
    //});
    $.connection.hub.start(function () { });
});