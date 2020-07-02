//CLIC EN NEXT1
function enviar() {
    $(document).ready(function () {
        $('#block2A').removeClass("block2").addClass("activeblock2");
        $("#b2").removeClass("circulo").addClass("activecirculo");
        $('#p').addClass("progreso1");
        $('.icon1').addClass("fa fa-check");
        $('#next1').hide();
        $('#next2').show();

        $('#Paso1').hide();
        $('#Paso2').show();
    });

}

//CLIC EN NEXT2
function enviar2() {
    $(document).ready(function () {
        $('#p').addClass("progreso3");
        $('#block3A').removeClass("block3").addClass("activeblock3");
        $("#b3").removeClass("circulo").addClass("activecirculo");
        $('.icon2').addClass("fa fa-check");
        $('#next2').hide();

        $('#Paso2').hide();
        $('#Paso3').show();
    });
}
