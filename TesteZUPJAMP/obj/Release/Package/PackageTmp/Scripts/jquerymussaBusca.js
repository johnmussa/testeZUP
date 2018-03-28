function alteraLayout() {
    $(".body-content").find("[for='name']").text("Distância Máxima");
    $(".body-content").find("[for='pos_x']").text("Posição X");
    $(".body-content").find("[for='pos_y']").text("Posição Y");
    $(".body-content").find("h2").text("Buscar por Proximidade");
    $(".body-content").find("h4").hide();
    $('.btn-default')[0].value = "Buscar pontos de interesse";
}

function validaCampos() {
    
    var camposOK = true;

    if ($("input[type=text]")[0].value == "" || isNaN($("input[type=text]")[0].value) || ($("input[type=text]")[0].value * 1) < 0) {
        camposOK = false;
    }

    if ($("input[type=number]")[0].value == "" || isNaN($("input[type=number]")[0].value) || ($("input[type=number]")[0].value * 1) < 0) {
        camposOK = false;
    }

    if ($("input[type=number]")[1].value == "" || isNaN($("input[type=number]")[1].value) || ($("input[type=number]")[1].value * 1) < 0) {
        camposOK = false;
    }
    
    if (camposOK == false) {
        alert("Todos os campos devem ser numéricos e maior que zero.");
        
        return false;
    }

    $('.btn-default').attr('onclick', '');
    $('.btn-default').click();
}

function alterarSubmit() {
    $('.btn-default').attr('onclick', 'event.preventDefault(); validaCampos();');
}

alteraLayout();
alterarSubmit();


