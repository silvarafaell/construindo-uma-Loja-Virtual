﻿$(document).ready(function () {
    MudarOrdenacao();
});
function MudarOrdenacao() {
    $("#ordenacao").change(function () {
        //TODO - Redirecionar para a tela Home/Index passando as QueryString de Ordenação e mantendo a Pagina e a pesquisa.
        var Pagina = 1;
        var Pesquisa = "";
        var Ordenacao = $(this).val();

        var QueryString = new URLSearchParams(window.location.search);
        if (QueryString.has("pagina")) {
            Pagina = QueryString.get("pagina");
        }
        if (QueryString.has("pesquisa")) {
            Pagina = QueryString.get("pesquisa");
        }

        var URL = window.location.protocol + "//" + window.location.host +  window.location.pathname;

        var URLComParametros = URL + "?pagina=" + Pagina + "&pesquisa=" + Pesquisa + "&ordenacao" + Ordenacao + "#ordenacao";
        window.location.href = URLComParametros;

    });
}