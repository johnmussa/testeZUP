function alteraLayout() {
    $(".body-content").find("[for='name']").text("Nome");
    $(".body-content").find("[for='pos_x']").text("Posição X");
    $(".body-content").find("[for='pos_y']").text("Posição Y");
    $(".body-content").find("h4").hide();
    
}

function validaCampos() {
    
    var camposOK = true;
    var nomeOK = true;

    if ($("input[type=text]")[0].value == "") {
        nomeOK = false;
    }

    if ($("input[type=number]")[0].value == "" || isNaN($("input[type=number]")[0].value) || ($("input[type=number]")[0].value * 1) < 0) {
        camposOK = false;
    }

    if ($("input[type=number]")[1].value == "" || isNaN($("input[type=number]")[1].value) || ($("input[type=number]")[1].value * 1) < 0) {
        camposOK = false;
    }
    
    if (camposOK == false) {
        alert("Todos as posições devem ser numéricos e maior que zero.");

        return false;
    } else if (nomeOK == false) {
        alert("O Nome não pode estar em branco.");

        return false;
    } else {
        $('.btn-default').attr('onclick', '');
        $('.btn-default').click();
    }

}

function alterarSubmit() {
    $('.btn-default').attr('onclick', 'event.preventDefault(); validaCampos();');
}

alteraLayout();
alterarSubmit();


