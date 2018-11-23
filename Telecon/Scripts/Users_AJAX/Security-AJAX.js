// ----------------------------------------- VALIDAR LA CONTRASEÑA ANTIGUA -------------------------------------------- //

function ValidarContraseña() {

    var divtext = document.getElementById("validaciones-contenido");
    var timer = null;
    var oldPassword = document.getElementById("vieja_contraseña").value;
    $.ajax({
        type: "POST",
        url: "/json/verifypassword",
        data: AddAntiForgeryToken({ rawPass: oldPassword }),
        success: function (data) {
            if (data == true) {
                divtext.innerHTML = "<p>Contraseña actualizada exitosamente.</p>\n";
                divtext.innerHTML += "<p>Ahora debe acceder a su cuenta utilizando su nueva contraseña.</p>\n";
                $('.modal-header').css('background-color', 'blue');
                $('.modal-footer').css('background-color', 'blue');
                $('#validacion').modal('show');
                window.clearTimeout(timer);
                timer = setTimeout(function () {
                    $("#cerrar-validacion").click();
                    document.getElementById("change-password-form").submit();
                }, 3000); // <-- time in milliseconds
            } else {
                divtext.innerHTML = "<p>Contraseña de usuario incorrecta.</p>\n";
                $('.modal-header').css('background-color', 'red');
                $('.modal-footer').css('background-color', 'red');
                $('#validacion').modal('show');
                window.clearTimeout(timer);
                timer = setTimeout(function () {
                    $("#cerrar-validacion").click();
                }, 2000); // <-- time in milliseconds
                return false;
            }
        },
        error: function (xhr, status, error) {

        }
    });
}

// ----------------------------------------- VALIDATE THE OLD PASSWORD -------------------------------------------- //

function ValidatePass() {

    var divtext = document.getElementById("validaciones-contenido");
    var timer = null;
    var oldPassword = document.getElementById("vieja_contraseña").value;
    $.ajax({
        type: "POST",
        url: "/json/verifypassword",
        data: AddAntiForgeryToken({ rawPass: oldPassword }),
        success: function (data) {
            if (data == true) {
                divtext.innerHTML = "<p>Password changed successfully.</p>\n";
                divtext.innerHTML += "<p>You must now log into your account using your new password.</p>\n";
                $('.modal-header').css('background-color', 'blue');
                $('.modal-footer').css('background-color', 'blue');
                $('#validacion').modal('show');
                window.clearTimeout(timer);
                timer = setTimeout(function () {
                    $("#cerrar-validacion").click();
                    document.getElementById("change-password-form").submit();
                }, 3000); // <-- time in milliseconds
            } else {
                divtext.innerHTML = "<p>Incorrect account password.</p>\n";
                $('.modal-header').css('background-color', 'red');
                $('.modal-footer').css('background-color', 'red');
                $('#validacion').modal('show');
                window.clearTimeout(timer);
                timer = setTimeout(function () {
                    $("#cerrar-validacion").click();
                }, 2000); // <-- time in milliseconds
                return false;
            }
        },
        error: function (xhr, status, error) {

        }
    });
}