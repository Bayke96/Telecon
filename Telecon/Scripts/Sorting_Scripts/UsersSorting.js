function BuscarUsuarioID() {

    var userID = document.getElementById("buscar_codigo").value;
    var table = document.getElementById("table-users-list");

    if (userID == "") {
        return false;
    }

    $.ajax({
        type: "POST",
        url: "/json/sort/userid",
        dataType: "json",
        data: AddAntiForgeryToken({ id: userID }),
        success: function (data) {
            if (data != null) {
                $("#table-users-list").find("tr:gt(0)").remove();
                $('#table-users-list tbody').append("<tr><td>" + userID + "</td>" +
                    "<td>" + data.userName + "</td>" +
                    "<td>" + data.firstName + "</td>" +
                    "<td>" + data.lastName + "</td>" +
                    "<td>" + data.email + "</td>" +
                    "<td>" + data.number + "</td>" +
                    "<td><button type='button' value=' " + userID + "' onclick='EditUser(this);'>Editar</button>" +
                    "<td><button type='button' value=' " + userID + "' onclick='DeleteUser(this);'>Borrar</button>");
                return true
            }
            if (data == null) {
                return false;
            }
        },
        error: function (data) {
            return false;
        }
    });
}

function BuscarUsuarioNombres() {

    var userName = document.getElementById("buscar_nombre").value;
    var table = document.getElementById("table-users-list");

    if (userName == "") {
        return false;
    }

    $.ajax({
        type: "POST",
        url: "/json/sort/usernames",
        dataType: "json",
        data: AddAntiForgeryToken({ names: userName }),
        success: function (data) {
            if (data != null) {
                $("#table-users-list").find("tr:gt(0)").remove();
                for (var i = 0; i < data.length; i++) {
                    $('#table-users-list tbody').append("<tr><td>" + data[i].userID + "</td>" +
                        "<td>" + data[i].userName + "</td>" +
                        "<td>" + data[i].firstName + "</td>" +
                        "<td>" + data[i].lastName + "</td>" +
                        "<td>" + data[i].email + "</td>" +
                        "<td>" + data[i].number + "</td>" +
                        "<td><button type='button' value=' " + data[i].userID + "' onclick='EditUser(this);'>Editar</button>" +
                        "<td><button type='button' value=' " + data[i].userID + "' onclick='DeleteUser(this);'>Borrar</button>");
                }
            }
            if (data == null) {
                return false;
            }
        },
        error: function (data) {
            return false;
        }
    });
}

function BuscarUsuarioCorreo() {

    var userEmail = document.getElementById("buscar_correo").value;
    var table = document.getElementById("table-users-list");

    if (userEmail == "") {
        return false;
    }

    $.ajax({
        type: "POST",
        url: "/json/sort/email",
        dataType: "json",
        data: AddAntiForgeryToken({ email: userEmail }),
        success: function (data) {
           
            if (data != null) {
                $("#table-users-list").find("tr:gt(0)").remove();
                for (var i = 0; i < data.length; i++) {
                    $('#table-users-list tbody').append("<tr><td>" + data[i].userID + "</td>" +
                        "<td>" + data[i].userName + "</td>" +
                        "<td>" + data[i].firstName + "</td>" +
                        "<td>" + data[i].lastName + "</td>" +
                        "<td>" + data[i].email + "</td>" +
                        "<td>" + data[i].number + "</td>" +
                        "<td><button type='button' value=' " + data[i].userID + "' onclick='EditUser(this);'>Editar</button>" +
                        "<td><button type='button' value=' " + data[i].userID + "' onclick='DeleteUser(this);'>Borrar</button>");
                }
                return true
            }
            if (data == null) {
                return false;
            }
        },
        error: function (data) {
            return false;
        }
    });
}

function BuscarUsuarioTelefono() {

    var userPhone = document.getElementById("buscar_numero").value;
    var table = document.getElementById("table-users-list");

    if (userPhone == "") {
        return false;
    }

    $.ajax({
        type: "POST",
        url: "/json/sort/phone",
        dataType: "json",
        data: AddAntiForgeryToken({ phone: userPhone }),
        success: function (data) {
            
            if (data != null) {
                $("#table-users-list").find("tr:gt(0)").remove();
                for (var i = 0; i < data.length; i++) {
                    $('#table-users-list tbody').append("<tr><td>" + data[i].userID + "</td>" +
                        "<td>" + data[i].userName + "</td>" +
                        "<td>" + data[i].firstName + "</td>" +
                        "<td>" + data[i].lastName + "</td>" +
                        "<td>" + data[i].email + "</td>" +
                        "<td>" + data[i].number + "</td>" +
                        "<td><button type='button' value=' " + data[i].userID + "' onclick='EditUser(this);'>Editar</button>" +
                        "<td><button type='button' value=' " + data[i].userID + "' onclick='DeleteUser(this);'>Borrar</button>");
                }
                return true
            }
            if (data == null) {
                return false;
            }
        },
        error: function (data) {
            return false;
        }
    });
}

function TodosUsuarios() {

    var table = document.getElementById("table-users-list");

    $.ajax({
        type: "POST",
        url: "/json/sort/userid",
        dataType: "json",
        data: AddAntiForgeryToken({ id: 0 }),
        success: function (data) {

            $("#table-users-list").find("tr:gt(0)").remove();

            for (var i = 0; i < data.length; i++) {
                $('#table-users-list tbody').append("<tr><td>" + data[i].userID + "</td>" +
                    "<td>" + data[i].userName + "</td>" +
                    "<td>" + data[i].firstName + "</td>" +
                    "<td>" + data[i].lastName + "</td>" +
                    "<td>" + data[i].email + "</td>" +
                    "<td>" + data[i].number + "</td>" +
                    "<td><button type='button' value=' " + data[i].userID + "' onclick='EditUser(this);'>Editar</button>" +
                    "<td><button type='button' value=' " + data[i].userID + "' onclick='DeleteUser(this);'>Borrar</button>");
            }

        },
        error: function (data) {
            return false;
        }
    });
}

function OrdenarUsuariosPor(sortBy) {
    var table = document.getElementById("table-users-list");
    
    $.ajax({
        type: "POST",
        url: "/json/sort/orderusersby",
        dataType: "json",
        data: AddAntiForgeryToken({ orderBy: sortBy.text }),
        success: function (data) {
            $("#table-users-list").find("tr:gt(0)").remove();

            for (var i = 0; i < data.length; i++) {
                $('#table-users-list tbody').append("<tr><td>" + data[i].userID + "</td>" +
                    "<td>" + data[i].userName + "</td>" +
                    "<td>" + data[i].firstName + "</td>" +
                    "<td>" + data[i].lastName + "</td>" +
                    "<td>" + data[i].email + "</td>" +
                    "<td>" + data[i].number + "</td>" +
                    "<td><button type='button' value=' " + data[i].userID + "' onclick='EditUser(this);'>Editar</button>" +
                    "<td><button type='button' value=' " + data[i].userID + "' onclick='DeleteUser(this);'>Borrar</button>");
            }

        },
        error: function (data) {
            return false;
        }
    });
}

// ------------------------- ENGLISH USER SORTS -----------------------------------------


function SearchUserByID() {

    var userID = document.getElementById("buscar_codigo").value;
    var table = document.getElementById("table-users-list");

    if (userID == "") {
        return false;
    }

    $.ajax({
        type: "POST",
        url: "/json/sort/userid",
        dataType: "json",
        data: AddAntiForgeryToken({ id: userID }),
        success: function (data) {
            if (data != null) {
                $("#table-users-list").find("tr:gt(0)").remove();
                $('#table-users-list tbody').append("<tr><td>" + userID + "</td>" +
                    "<td>" + data.userName + "</td>" +
                    "<td>" + data.firstName + "</td>" +
                    "<td>" + data.lastName + "</td>" +
                    "<td>" + data.email + "</td>" +
                    "<td>" + data.number + "</td>" +
                    "<td><button type='button' value=' " + userID + "' onclick='EditUser(this);'>Edit</button>" +
                    "<td><button type='button' value=' " + userID + "' onclick='DeleteUser(this);'>Delete</button>");
                return true
            }
            if (data == null) {
                return false;
            }
        },
        error: function (data) {
            return false;
        }
    });
}

function SearchUserByNames() {

    var userName = document.getElementById("buscar_nombre").value;
    var table = document.getElementById("table-users-list");

    if (userName == "") {
        return false;
    }

    $.ajax({
        type: "POST",
        url: "/json/sort/usernames",
        dataType: "json",
        data: AddAntiForgeryToken({ names: userName }),
        success: function (data) {
            if (data != null) {
                $("#table-users-list").find("tr:gt(0)").remove();
                for (var i = 0; i < data.length; i++) {
                    $('#table-users-list tbody').append("<tr><td>" + data[i].userID + "</td>" +
                        "<td>" + data[i].userName + "</td>" +
                        "<td>" + data[i].firstName + "</td>" +
                        "<td>" + data[i].lastName + "</td>" +
                        "<td>" + data[i].email + "</td>" +
                        "<td>" + data[i].number + "</td>" +
                        "<td><button type='button' value=' " + data[i].userID + "' onclick='EditUser(this);'>Edit</button>" +
                        "<td><button type='button' value=' " + data[i].userID + "' onclick='DeleteUser(this);'>Delete</button>");
                }
            }
            if (data == null) {
                return false;
            }
        },
        error: function (data) {
            return false;
        }
    });
}

function SearchUserByEmail() {

    var userEmail = document.getElementById("buscar_correo").value;
    var table = document.getElementById("table-users-list");

    if (userEmail == "") {
        return false;
    }

    $.ajax({
        type: "POST",
        url: "/json/sort/email",
        dataType: "json",
        data: AddAntiForgeryToken({ email: userEmail }),
        success: function (data) {
            
            if (data != null) {
                $("#table-users-list").find("tr:gt(0)").remove();
                for (var i = 0; i < data.length; i++) {
                    $('#table-users-list tbody').append("<tr><td>" + data[i].userID + "</td>" +
                        "<td>" + data[i].userName + "</td>" +
                        "<td>" + data[i].firstName + "</td>" +
                        "<td>" + data[i].lastName + "</td>" +
                        "<td>" + data[i].email + "</td>" +
                        "<td>" + data[i].number + "</td>" +
                        "<td><button type='button' value=' " + data[i].userID + "' onclick='EditUser(this);'>Edit</button>" +
                        "<td><button type='button' value=' " + data[i].userID + "' onclick='DeleteUser(this);'>Delete</button>");
                }
                return true
            }
            if (data == null) {
                return false;
            }
        },
        error: function (data) {
            return false;
        }
    });
}

function SearchUserByNumber() {

    var userPhone = document.getElementById("buscar_numero").value;
    var table = document.getElementById("table-users-list");

    if (userPhone == "") {
        return false;
    }

    $.ajax({
        type: "POST",
        url: "/json/sort/phone",
        dataType: "json",
        data: AddAntiForgeryToken({ phone: userPhone }),
        success: function (data) {
           
            if (data != null) {
                $("#table-users-list").find("tr:gt(0)").remove();
                for (var i = 0; i < data.length; i++) {
                    $('#table-users-list tbody').append("<tr><td>" + data[i].userID + "</td>" +
                        "<td>" + data[i].userName + "</td>" +
                        "<td>" + data[i].firstName + "</td>" +
                        "<td>" + data[i].lastName + "</td>" +
                        "<td>" + data[i].email + "</td>" +
                        "<td>" + data[i].number + "</td>" +
                        "<td><button type='button' value=' " + data[i].userID + "' onclick='EditUser(this);'>Edit</button>" +
                        "<td><button type='button' value=' " + data[i].userID + "' onclick='DeleteUser(this);'>Delete</button>");
                }
                return true
            }
            if (data == null) {
                return false;
            }
        },
        error: function (data) {
            return false;
        }
    });
}

function AllUsers() {

    var table = document.getElementById("table-users-list");

    $.ajax({
        type: "POST",
        url: "/json/sort/userid",
        dataType: "json",
        data: AddAntiForgeryToken({ id: 0 }),
        success: function (data) {

            $("#table-users-list").find("tr:gt(0)").remove();

            for (var i = 0; i < data.length; i++) {
                $('#table-users-list tbody').append("<tr><td>" + data[i].userID + "</td>" +
                    "<td>" + data[i].userName + "</td>" +
                    "<td>" + data[i].firstName + "</td>" +
                    "<td>" + data[i].lastName + "</td>" +
                    "<td>" + data[i].email + "</td>" +
                    "<td>" + data[i].number + "</td>" +
                    "<td><button type='button' value=' " + data[i].userID + "' onclick='EditUser(this);'>Edit</button>" +
                    "<td><button type='button' value=' " + data[i].userID + "' onclick='DeleteUser(this);'>Delete</button>");
            }

        },
        error: function (data) {
            return false;
        }
    });
}

function OrderUsers(sortBy) {
    var table = document.getElementById("table-users-list");

    $.ajax({
        type: "POST",
        url: "/json/sort/orderusersby",
        dataType: "json",
        data: AddAntiForgeryToken({ orderBy: sortBy.text }),
        success: function (data) {
            $("#table-users-list").find("tr:gt(0)").remove();

            for (var i = 0; i < data.length; i++) {
                $('#table-users-list tbody').append("<tr><td>" + data[i].userID + "</td>" +
                    "<td>" + data[i].userName + "</td>" +
                    "<td>" + data[i].firstName + "</td>" +
                    "<td>" + data[i].lastName + "</td>" +
                    "<td>" + data[i].email + "</td>" +
                    "<td>" + data[i].number + "</td>" +
                    "<td><button type='button' value=' " + data[i].userID + "' onclick='EditUser(this);'>Edit</button>" +
                    "<td><button type='button' value=' " + data[i].userID + "' onclick='DeleteUser(this);'>Delete</button>");
            }

        },
        error: function (data) {
            return false;
        }
    });
}