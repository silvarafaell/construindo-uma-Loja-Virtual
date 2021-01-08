﻿$(document).ready(function () {
    MoverScrollOrdenacao();
    MudarOrdenacao();
});
function MoverScrollOrdenacao() {
    if (window.location.hash.length > 0) {
        var hash = window.location.hash;
        if (hash == "#posicaoproduto") {
            window.scrollBy(0, 474);
        }
    }
}
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
            Pesquisa = QueryString.get("pesquisa");
        }

        var URL = window.location.protocol + "//" + window.location.host + window.location.pathname;

        var URLComParametros = URL + "?pagina=" + Pagina + "&pesquisa=" + Pesquisa + "&ordenacao" + Ordenacao + "#myCarousel";
        window.location.href = URLComParametros;

    });
}