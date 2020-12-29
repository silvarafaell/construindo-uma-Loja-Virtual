﻿$(document).ready(function () {
    $(".btn-danger").click(function (e) {
        var resultado = confirm("Tem certeza que deseja realizar essa operação?");

        if (resultado == false) {
            e.preventDefault();
        }   
    });
    $('.dinheiro').mask('000.000.000.000.000,00', { reverse: true });
    AjaxUploadImagemProduto();
});

function AjaxUploadImagemProduto() {
    $(".img-upload").click(function () {
        $(this).parent().find(".input-file").click();
    });

    $(".input-file").change(function () {
        //Formulario de dados via javascript
        var Binario = $(this)[0].files[0];
        var Formulario = new FormData();
        Formulario.append("file", Binario);

        //Requisiçao Ajax enviado a formulario
        $.ajax({
            type: "POST",
            url: "Colaborador/Imagem/Armazenar",
            data: Formulario,
            contentType: false,
            processData: false,
            error: function () {
                alert: ("Erro no envio do arquivo!");
            },
            success: function (data) {
                alert: ("Arquivo enviado com sucesso!" + data.caminho)
            }
        });
    });
}

