$(document).ready(function () {
    $(".btn-danger").click(function (e) {
        var resultado = confirm("Tem certeza que deseja realizar essa operação?");

        if (resultado == false) {
            e.preventDefault();
        }   
    });
    $('.dinheiro').mask('000.000.000.000.000,00', { reverse: true });

});

function AjaxUploadImagemProduto() {
    $(".img-upload").click(function () {
        $(this).paren().find(".input-file").click();
    });
}