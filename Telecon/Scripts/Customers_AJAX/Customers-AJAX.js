// --------------------------------- VERIFICA SI EL CLIENTE YA EXISTE -------------------------------------- //

function ValidarCliente() {
    var cliente = document.getElementById("agregar-razon-social").value;
    var divtext = document.getElementById("validaciones-contenido");
    var timer = null;

    var selector = document.getElementById("SeleccionCliente");
    var oldName = selector.options[selector.selectedIndex].textContent;
    var newName = document.getElementById("editar-razon-social").value;

    $.ajax({
        type: "POST",
        url: "/json/validatecustomer",
        dataType: "json",
        data: AddAntiForgeryToken({ razonSocial: cliente }),
        success: function (json) {
            if (json != null) var data = JSON.parse(json);
            if (data == true) {
                document.getElementById("p-razon-social").textContent = "Razón Social";
                document.getElementById("p-razon-social").innerHTML += "<span style='padding-right: 5%; color: red; float: right;'> Ya tiene registrado un cliente con este nombre </span>";
                return false;
            }
            else {
                return AgregarCliente();
            }
        },
        error: function (data) {
            return false;
        }
    });
    return false;
}

// ---------------------------------------------------- VALIDAR EDICION CLIENTE ----------------------------------------- //

function ValidarEdicionCliente() {
    var divtext = document.getElementById("validaciones-contenido");
    var timer = null;

    var selector = document.getElementById("SeleccionCliente");
    var oldName = selector.options[selector.selectedIndex].textContent;
    var newName = document.getElementById("editar-razon-social").value;

    $.ajax({
        type: "POST",
        url: "/json/validatecustomer",
        dataType: "json",
        data: AddAntiForgeryToken({ razonSocial: newName }),
        success: function (json) {
            if (json != null) var data = JSON.parse(json);
            if (data == true && newName.trim().toUpperCase() != oldName.trim().toUpperCase()) {
                document.getElementById("pm-razon-social").textContent = "Razón Social";
                document.getElementById("pm-razon-social").innerHTML += "<span style='padding-right: 5%; color: red; float: right;'> Ya tiene registrado un cliente con este nombre </span>";
                return false;
            }
            else {
                return EditarCliente();
            }
        },
        error: function (data) {
            return false;
        }
    });
    return false;
}


// --------------------------------------------- LOAD CUSTOMER - CARGAR CLIENTE ----------------------------------------- //

function CargarCliente(item) {
    var customer = item.value;

    $.ajax({
        type: "POST",
        url: "/json/CargarCliente",
        data: AddAntiForgeryToken({ customerID: customer }),
        success: function (data) {
            document.getElementById("editar-razon-social").value = data.razonsocial;
            document.getElementById("editar-nombres").value = data.nombres;
            document.getElementById("editar-apellidos").value = data.apellidos;
            document.getElementById("editar-telefono").value = data.telefono;
            document.getElementById("editar-direccion").value = data.direccion;
            document.getElementById("editar-comentarios").value = data.comentarios;
        },
        error: function (request, status, error) {
            return false;
        }
    });
}

// -------------------------------------------- LOAD CUSTOMERS LIST - CARGAR LISTA DE CLIENTES ---------------------------- //

// ---------------------------------------- UPDATE PRODUCTS LIST / ACTUALIZAR LISTA DE PRODUCTOS -------------------------- //

function CargarListaClientes() {
    $.ajax({
        type: "GET",
        url: "/json/listaclientes",
        success: function (data) {
            for (var item in data) {
                var select = document.getElementById('SeleccionCliente');
                var opt = document.createElement('option');
                opt.value = data[item].ID;
                opt.innerHTML = data[item].razonsocial;
                select.appendChild(opt);
            }
        },
        error: function (xhr, status, error) {

        }
    });
}


// ------------------------------------------------- AGREGAR CLIENTE ---------------------------------------------------- //

function AgregarCliente() {

    var timer = null;
    var form = $('#add-customer-form')
    var divtext = document.getElementById("validaciones-contenido");
    var formData = new FormData(form.get(0)); //add .get(0)

    var customerModel = {
        razonsocial: document.getElementById("agregar-razon-social").value,
        nombres: document.getElementById("agregar-nombres").value,
        apellidos: document.getElementById("agregar-apellidos").value,
        telefono: document.getElementById("agregar-telefono").value,
        direccion: document.getElementById("agregar-direccion").value,
        comentarios: document.getElementById("agregar-comentarios").value,
    };

    formData.append('modelo', customerModel);

    $.ajax({
        type: "POST",
        url: "/json/addcustomer",
        data: AddAntiForgeryToken({ 'modelo': customerModel }),
        success: function (data) {
            document.getElementById("p-razon-social").textContent = "Razón Social";
            document.getElementById("p-nombres").textContent = "Nombres";
            document.getElementById("p-apellidos").textContent = "Apellidos";
            document.getElementById("p-telefono").textContent = "Teléfono";
            document.getElementById("p-direccion").textContent = "Dirección";
            document.getElementById("add-customer-title").textContent = "Cliente Agregado";
            document.getElementById("add-customer-title").style.color = "green";
            setTimeout(function () {
                $("#cerrar-validacion").click();
                document.getElementById("add-reset-button").click();
                document.getElementById("add-customer-title").textContent = "Agregar Nuevo Cliente";
                document.getElementById("add-customer-title").style.color = "lightslategrey";
            }, 2000); // <-- time in milliseconds
        },
        error: function (request, status, error) {
            return false;
        }
    });
}

// --------------------------------------------------- EDITAR CLIENTE ---------------------------------------------------- //

function EditarCliente() {

    var timer = null;
    var form = $('#edit-customer-form')
    var divtext = document.getElementById("validaciones-contenido");
    var formData = new FormData(form.get(0)); //add .get(0)

    var selector = document.getElementById("SeleccionCliente");
    var cID = selector.options[selector.selectedIndex].value;

    var customerModel = {
        razonsocial: document.getElementById("editar-razon-social").value,
        nombres: document.getElementById("editar-nombres").value,
        apellidos: document.getElementById("editar-apellidos").value,
        telefono: document.getElementById("editar-telefono").value,
        direccion: document.getElementById("editar-direccion").value,
        comentarios: document.getElementById("editar-comentarios").value,
    };

    formData.append('modelo', customerModel);
    $.ajax({
        type: "POST",
        url: "/json/editcustomer",
        data: AddAntiForgeryToken({ 'customerID': cID, 'modelo': customerModel }),
        success: function (data) {
            document.getElementById("pm-razon-social").textContent = "Razón Social";
            document.getElementById("pm-nombres").textContent = "Nombres";
            document.getElementById("pm-apellidos").textContent = "Apellidos";
            document.getElementById("pm-telefono").textContent = "Teléfono";
            document.getElementById("pm-direccion").textContent = "Dirección";
            document.getElementById("edit-customer-title").textContent = "Cliente Actualizado";
            document.getElementById("edit-customer-title").style.color = "green";
            LimpiarListaProductos("SeleccionCliente");
            setTimeout(function () {
                $("#cerrar-validacion").click();
                document.getElementById("edit-reset").click();
                document.getElementById("edit-customer-title").textContent = "Modificar Cliente Existente";
                document.getElementById("edit-customer-title").style.color = "lightslategrey";
                CargarListaClientes();
            }, 2000); // <-- time in milliseconds
        },
        error: function (request, status, error) {
            return false;
        }
    });
}

// ----------------------------------------------------- ELIMINAR CLIENTES --------------------------------------- //

function EliminarClientes(listaClientes) {

    var timer = null;
    var divtext = document.getElementById("validaciones-contenido");

    $.ajax({
        type: "POST",
        url: "/json/deletecustomers",
        data: AddAntiForgeryToken({ customersList: listaClientes }),
        success: function (data) {
            document.getElementById("delete-customer-title").textContent = "Cliente(s) Eliminados";
            document.getElementById("delete-customer-title").style.color = "red";
            $("#delete-customers-table").find("tr:gt(1)").remove();
            setTimeout(function () {
                document.getElementById("CerrarVentanaEliminarC").click();
            }, 2000); // <-- time in milliseconds
        },
        error: function (request, status, error) {
            return false;
        }
    });
}

// --------------------------------------------------- VERIFY IF USER ALREADY EXISTS ----------------------------------- //

function ValidateCustomer(customersList) {
    var cliente = document.getElementById("agregar-razon-social").value;
    var divtext = document.getElementById("validaciones-contenido");
    var timer = null;

    $.ajax({
        type: "POST",
        url: "/json/validatecustomer",
        dataType: "json",
        data: AddAntiForgeryToken({ razonSocial: cliente }),
        success: function (json) {
            if (json != null) var data = JSON.parse(json);
            if (data == true) {
                document.getElementById("p-razon-social").textContent = "Company's Name";
                document.getElementById("p-razon-social").innerHTML += "<span style='padding-right: 5%; color: red; float: right;'> This customer is already registered </span>";
                return false;
            }
            else {
                return AddCustomer();
            }
        },
        error: function (data) {
            return false;
        }
    });
    return false;
}

// ---------------------------------------------------- VALIDATE CLIENT UPDATE ----------------------------------------- //

function ValidateCustomerEdit() {
    var divtext = document.getElementById("validaciones-contenido");
    var timer = null;

    var selector = document.getElementById("SeleccionCliente");
    var oldName = selector.options[selector.selectedIndex].textContent;
    var newName = document.getElementById("editar-razon-social").value;

    $.ajax({
        type: "POST",
        url: "/json/validatecustomer",
        dataType: "json",
        data: AddAntiForgeryToken({ razonSocial: newName }),
        success: function (json) {
            if (json != null) var data = JSON.parse(json);
            if (data == true && newName.trim().toUpperCase() != oldName.trim().toUpperCase()) {
                document.getElementById("pm-razon-social").textContent = "Company's Name";
                document.getElementById("pm-razon-social").innerHTML += "<span style='padding-right: 5%; color: red; float: right;'>  This customer is already registered  </span>";
                return false;
            }
            else {
                return EditCustomer();
            }
        },
        error: function (data) {
            return false;
        }
    });
    return false;
}

// ------------------------------------------------- ADD CUSTOMER --------------------------------------------------------- //

function AddCustomer() {

    var timer = null;
    var form = $('#add-customer-form')
    var divtext = document.getElementById("validaciones-contenido");
    var formData = new FormData(form.get(0)); //add .get(0)

    var customerModel = {
        razonsocial: document.getElementById("agregar-razon-social").value,
        nombres: document.getElementById("agregar-nombres").value,
        apellidos: document.getElementById("agregar-apellidos").value,
        telefono: document.getElementById("agregar-telefono").value,
        direccion: document.getElementById("agregar-direccion").value,
        comentarios: document.getElementById("agregar-comentarios").value,
    };

    formData.append('modelo', customerModel);

    $.ajax({
        type: "POST",
        url: "/json/addcustomer",
        data: AddAntiForgeryToken({ 'modelo': customerModel }),
        success: function (data) {
            document.getElementById("p-razon-social").textContent = "Company's Name";
            document.getElementById("p-nombres").textContent = "Names";
            document.getElementById("p-apellidos").textContent = "Lastnames";
            document.getElementById("p-telefono").textContent = "Phone Number";
            document.getElementById("p-direccion").textContent = "Address";
            document.getElementById("add-customer-title").textContent = "Customer Registered";
            document.getElementById("add-customer-title").style.color = "green";
            setTimeout(function () {
                $("#cerrar-validacion").click();
                document.getElementById("add-reset-button").click();
                document.getElementById("add-customer-title").textContent = "Add New Customer";
                document.getElementById("add-customer-title").style.color = "lightslategrey";
            }, 2000); // <-- time in milliseconds
        },
        error: function (request, status, error) {
            return false;
        }
    });
}

// --------------------------------------------------- EDIT CUSTOMER ---------------------------------------------------- //

function EditCustomer() {

    var timer = null;
    var form = $('#edit-customer-form')
    var divtext = document.getElementById("validaciones-contenido");
    var formData = new FormData(form.get(0)); //add .get(0)

    var selector = document.getElementById("SeleccionCliente");
    var cID = selector.options[selector.selectedIndex].value;

    var customerModel = {
        razonsocial: document.getElementById("editar-razon-social").value,
        nombres: document.getElementById("editar-nombres").value,
        apellidos: document.getElementById("editar-apellidos").value,
        telefono: document.getElementById("editar-telefono").value,
        direccion: document.getElementById("editar-direccion").value,
        comentarios: document.getElementById("editar-comentarios").value,
    };

    formData.append('modelo', customerModel);
    $.ajax({
        type: "POST",
        url: "/json/editcustomer",
        data: AddAntiForgeryToken({ 'customerID': cID, 'modelo': customerModel }),
        success: function (data) {
            document.getElementById("p-razon-social").textContent = "Company's Name";
            document.getElementById("p-nombres").textContent = "Names";
            document.getElementById("p-apellidos").textContent = "Lastnames";
            document.getElementById("p-telefono").textContent = "Phone Number";
            document.getElementById("p-direccion").textContent = "Address";
            document.getElementById("edit-customer-title").textContent = "Customer Updated";
            document.getElementById("edit-customer-title").style.color = "green";
            LimpiarListaProductos("SeleccionCliente");
            setTimeout(function () {
                $("#cerrar-validacion").click();
                document.getElementById("edit-reset").click();
                document.getElementById("edit-customer-title").textContent = "Edit Customer";
                document.getElementById("edit-customer-title").style.color = "lightslategrey";
                CargarListaClientes();
            }, 2000); // <-- time in milliseconds
        },
        error: function (request, status, error) {
            return false;
        }
    });
}

// ----------------------------------------------------- DELETE CUSTOMERS --------------------------------------- //

function DeleteCustomers(listaClientes) {

    var timer = null;
    var divtext = document.getElementById("validaciones-contenido");

    $.ajax({
        type: "POST",
        url: "/json/deletecustomers",
        data: AddAntiForgeryToken({ customersList: listaClientes }),
        success: function (data) {
            document.getElementById("delete-customer-title").textContent = "Customer(s) Eliminated";
            document.getElementById("delete-customer-title").style.color = "red";
            $("#delete-customers-table").find("tr:gt(1)").remove();
            setTimeout(function () {
                document.getElementById("CerrarVentanaEliminarC").click();
            }, 2000); // <-- time in milliseconds
        },
        error: function (request, status, error) {
            return false;
        }
    });
}
