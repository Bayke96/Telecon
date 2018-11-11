// ---------------------------------------- CLEAN PRODUCTS LIST / LIMPIAR LISTA DE PRODUCTOS -------------------------- //

function LimpiarListaProductos(id) {
    var oplist = document.getElementById(id);
    for (var i = oplist.options.length - 1; i > 0; i--) {
        oplist.removeChild(oplist.options[i]);
    }
}

// ---------------------------------------- UPDATE PRODUCTS LIST / ACTUALIZAR LISTA DE PRODUCTOS -------------------------- //

function UpdateProductList() {
    $.ajax({
        type: "GET",
        url: "/json/listaproductos",
        success: function (data) {
            for (var item in data) {
                var select = document.getElementById('selector-productos');
                var opt = document.createElement('option');
                opt.value = data[item];
                opt.innerHTML = data[item];
                select.appendChild(opt);
            }
        },
        error: function (xhr, status, error) {

        }
    });
}

// -------------------------------------- AJAX CALL LOAD SELECTED ITEM DATA --------------------------------------------- //

function CargarProducto(item) {
    var product = item.value;

    $.ajax({
        type: "POST",
        url: "/json/cargarproducto",
        data: AddAntiForgeryToken({ productName: product }),
        success: function (data) {

            var filename = data.mainImage.replace(/^.*[\\\/]/, '');

            document.getElementById("nombre_producto").value = data.name;
            document.getElementById("descripcion_producto").value = data.description;
            document.getElementById("precio_producto").value = data.price

            if (data.secondaryImageA != null) var ImageA = data.secondaryImageA.replace(/^.*[\\\/]/, '');
            if (data.secondaryImageB != null) var ImageB = data.secondaryImageB.replace(/^.*[\\\/]/, '');
            if (data.secondaryImageC != null) var ImageC = data.secondaryImageC.replace(/^.*[\\\/]/, '');



            if (data.mainImage != null) {
                document.getElementById("load-main-img").style.backgroundImage = 'url("../../Style/Media/Product_Images/' + filename + '")';
            }
            if (data.secondaryImageA != null) {
                document.getElementById("load-second-imageA").style.backgroundImage = 'url("../../Style/Media/Product_Images/' + ImageA + '")';
            }
            if (data.secondaryImageB != null) {
                document.getElementById("load-second-imageB").style.backgroundImage = 'url("../../Style/Media/Product_Images/' + ImageB + '")';
            }
            if (data.secondaryImageC != null) {
                document.getElementById("load-second-imageC").style.backgroundImage = 'url("../../Style/Media/Product_Images/' + ImageC + '")';
            }

            if (data.secondaryImageA == null) {
                document.getElementById("load-second-imageA").style.backgroundImage = "none";
            }
            if (data.secondaryImageB == null) {
                document.getElementById("load-second-imageB").style.backgroundImage = "none";
            }
            if (data.secondaryImageC == null) {
                document.getElementById("load-second-imageC").style.backgroundImage = "none";
            }
        },
        error: function (request, status, error) {
            return false;
        }
    });
}

// --------------------------------------- LOAD LIST OF PRODUCTS FOR ELIMINATION ------------------------------- //

function LoadProductsListDelete(item) {
    var product = item.value;

    $.ajax({
        type: "POST",
        url: "/json/cargarproducto",
        data: AddAntiForgeryToken({ productName: product }),
        success: function (data) {

            var filename = data.mainImage.replace(/^.*[\\\/]/, '');

            document.getElementById("nombre_producto").textContent = data.name;
            document.getElementById("nombre_producto").value = data.name;
            document.getElementById("descripcion_producto").textContent = data.description;
            var value = data.price.toLocaleString(
                "en-US", // leave undefined to use the browser's locale,
                // or use a string like 'en-US' to override it.
                { minimumFractionDigits: 0, maximumFractionDigits: 2 }
            );
            document.getElementById("precio_producto").textContent = value + " BsS.";

            if (data.secondaryImageA != null) var ImageA = data.secondaryImageA.replace(/^.*[\\\/]/, '');
            if (data.secondaryImageB != null) var ImageB = data.secondaryImageB.replace(/^.*[\\\/]/, '');
            if (data.secondaryImageC != null) var ImageC = data.secondaryImageC.replace(/^.*[\\\/]/, '');

            if (data.mainImage != null) {
                document.getElementById("load-main-img").style.backgroundImage = 'url("../../Style/Media/Product_Images/' + filename + '")';
            }
            if (data.secondaryImageA != null) {
                document.getElementById("load-second-imageA").style.backgroundImage = 'url("../../Style/Media/Product_Images/' + ImageA + '")';
            }
            if (data.secondaryImageB != null) {
                document.getElementById("load-second-imageB").style.backgroundImage = 'url("../../Style/Media/Product_Images/' + ImageB + '")';
            }
            if (data.secondaryImageC != null) {
                document.getElementById("load-second-imageC").style.backgroundImage = 'url("../../Style/Media/Product_Images/' + ImageC + '")';
            }

            if (data.secondaryImageA == null) {
                document.getElementById("load-second-imageA").style.backgroundImage = "none";
            }
            if (data.secondaryImageB == null) {
                document.getElementById("load-second-imageB").style.backgroundImage = "none";
            }
            if (data.secondaryImageC == null) {
                document.getElementById("load-second-imageC").style.backgroundImage = "none";
            }

        },
        error: function (request, status, error) {
            return false;
        }
    });
}


// ----------------------------- VALIDAR REGISTRO DE PRODUCTO --------------------------------------- //

function RegistrarProducto() {
    var product = document.getElementById("nombre_producto").value;
    var divtext = document.getElementById("validaciones-contenido");
    var pPrice = document.getElementById("precio_producto").value;
   
    $.ajax({
        type: "POST",
        url: "/json/verifyproduct",
        dataType: "json",
        data: AddAntiForgeryToken({ productName: product }),
        success: function (json) {
            if (json != null) var data = JSON.parse(json);
            if (data.name == true) {
                divtext.innerHTML = "<p>Ya se encuentra registrado un producto con este nombre.</p>\n";
                $('.modal-header').css('background-color', 'red');
                $('.modal-footer').css('background-color', 'red');
                $('#validacion').modal('show');
                window.clearTimeout(timer);
                timer = setTimeout(function () {
                    $("#cerrar-validacion").click();
                }, 5000); // <-- time in milliseconds
                return false;
            }
            if (pPrice < data.minimo && data.minimo != null && data.minimo != "") {
                divtext.innerHTML = "<p>El precio introducido se encuentra por debajo del precio minimo.</p>\n";
                divtext.innerHTML += "<p>Precio minimo: " + data.minimo + " BsS.</p>\n";
                $('.modal-header').css('background-color', 'red');
                $('.modal-footer').css('background-color', 'red');
                $('#validacion').modal('show');
                window.clearTimeout(timer);
                timer = setTimeout(function () {
                    $("#cerrar-validacion").click();
                }, 5000); // <-- time in milliseconds
                return false;
            }
            if (pPrice > data.maximo && data.maximo != null && data.maximo != "") {
                divtext.innerHTML = "<p>El precio introducido se encuentra por encima del preximo máximo.</p>\n";
                divtext.innerHTML += "<p>Precio máximo: " + data.maximo + " BsS.</p>\n";
                $('.modal-header').css('background-color', 'red');
                $('.modal-footer').css('background-color', 'red');
                $('#validacion').modal('show');
                window.clearTimeout(timer);
                timer = setTimeout(function () {
                    $("#cerrar-validacion").click();
                }, 5000); // <-- time in milliseconds
                return false;
            }
            else {
                ProcesarRegistro();
            }
        },
        error: function (data) {
            console.log(data.responseText);
            return false;
        }
    });
    return false;
}

// --------------------------------- PROCESAR REGISTRO DE PRODUCTO ----------------------------------------------- //

function ProcesarRegistro() {

    var divtext = document.getElementById("validaciones-contenido");

    var pName = document.getElementById("nombre_producto").value;
    var description = document.getElementById("descripcion_producto").value;
    var pPrice = document.getElementById("precio_producto").value;
    var pValue = parseFloat(pPrice).toFixed(2);
    
    var mainImage = null, secondImageA = null, secondImageB = null, secondImageC = null;

    if (document.getElementById("PImg1").files[0] != null) {
        mainImage = document.getElementById("PImg1").files[0];
    }
    if (document.getElementById("PImg2").files[0] != null) {
        secondImageA = document.getElementById("PImg2").files[0];
    }
    if (document.getElementById("PImg3").files[0] != null) {
        secondImageB = document.getElementById("PImg3").files[0];
    }
    if (document.getElementById("PImg4").files[0] != null) {
        secondImageC = document.getElementById("PImg4").files[0];
    }
    
    var model = {
        name: pName,
        description: description,
        price: pValue
    };

    var form = $('#add-product-form')
    var formData = new FormData(form.get(0)); //add .get(0)
    formData.append('PImg1', mainImage);
    formData.append('PImg2', secondImageA);
    formData.append('PImg3', secondImageB);
    formData.append('PImg4', secondImageC);

    $.ajax({
        type: "POST",
        url: "/json/agregarproducto",
        processData: false,
        contentType: false,
        data: formData,
        success: function (data) {
            divtext.innerHTML = "<p>Producto agregado exitosamente.</p>\n";
            $('.modal-header').css('background-color', 'blue');
            $('.modal-footer').css('background-color', 'blue');
            $('#validacion').modal('show');
            window.clearTimeout(timer);
            setTimeout(function () {
                $("#cerrar-validacion").click();
                document.getElementById("ResetBtn").click();
            }, 2000); // <-- time in milliseconds
        },
        error: function (request, status, error) {
            return false;
        }
    });
}


// ----------------------------------------------------------- VALIDAR EDITAR PRODUCTO ----------------------------- //

function ValidarEdicionProducto() {
    var product = document.getElementById("nombre_producto").value;
    var divtext = document.getElementById("validaciones-contenido");
    var pPrice = document.getElementById("precio_producto").value;
    var selectedProduct = document.getElementById("selector-productos").value;

    $.ajax({
        type: "POST",
        url: "/json/verifyproduct",
        dataType: "json",
        data: AddAntiForgeryToken({ productName: product }),
        success: function (json) {
            var data = JSON.parse(json)
            if (data.name == true && selectedProduct.toLowerCase() != product.toLowerCase()) {
                divtext.innerHTML = "<p>Ya se encuentra registrado un producto con este nombre.</p>\n";
                $('.modal-header').css('background-color', 'red');
                $('.modal-footer').css('background-color', 'red');
                $('#validacion').modal('show');
                window.clearTimeout(timer);
                timer = setTimeout(function () {
                    $("#cerrar-validacion").click();
                }, 3000); // <-- time in milliseconds
                return false;
            }
            if (pPrice < data.minimo && data.minimo != null && data.minimo != "") {
                divtext.innerHTML = "<p>El precio introducido se encuentra por debajo del precio minimo.</p>\n";
                divtext.innerHTML += "<p>Precio minimo: " + data.minimo + " BsS.</p>\n";
                $('.modal-header').css('background-color', 'red');
                $('.modal-footer').css('background-color', 'red');
                $('#validacion').modal('show');
                window.clearTimeout(timer);
                timer = setTimeout(function () {
                    $("#cerrar-validacion").click();
                }, 3000); // <-- time in milliseconds
                return false;
            }
            if (pPrice > data.maximo && data.maximo != null && data.maximo != "") {
                divtext.innerHTML = "<p>El precio introducido se encuentra por encima del preximo máximo.</p>\n";
                divtext.innerHTML += "<p>Precio máximo: " + data.maximo + " BsS.</p>\n";
                $('.modal-header').css('background-color', 'red');
                $('.modal-footer').css('background-color', 'red');
                $('#validacion').modal('show');
                window.clearTimeout(timer);
                timer = setTimeout(function () {
                    $("#cerrar-validacion").click();
                }, 3000); // <-- time in milliseconds
                return false;
            }
            else {
                ProcesarModificacionProducto();
            }

        },
        error: function (data) {
            alert("failure");
            return false;
        }
    });
    return false;
}

// ---------------------------------------- PROCESAR MODIFICACION PRODUCTO -------------------------------------------- //


function ProcesarModificacionProducto() {

    var divtext = document.getElementById("validaciones-contenido");

    var mainImage = null, secondImageA = null, secondImageB = null, secondImageC = null;

    var oldName = document.getElementById("selector-productos").value;
    var pName = document.getElementById("nombre_producto").value;
    var pDescription = document.getElementById("descripcion_producto").value;
    var pPrice = document.getElementById("precio_producto").value;
    var pValue = parseFloat(pPrice).toFixed(2);

    if (document.getElementById("load-main-img").style.backgroundImage != "none") {
        mainImage = document.getElementById("load-main-img").style.backgroundImage;
    }
    if (document.getElementById("load-second-imageA").style.backgroundImage != "none") {
        secondImageA = document.getElementById("load-second-imageA").style.backgroundImage;
    }
    if (document.getElementById("load-second-imageB").style.backgroundImage != "none") {
        secondImageB = document.getElementById("load-second-imageB").style.backgroundImage;
    }
    if (document.getElementById("load-second-imageC").style.backgroundImage != "none") {
        secondImageC = document.getElementById("load-second-imageC").style.backgroundImage;
    }

    var pData = [oldName + "~", pName + "~", pDescription + "~", pValue + "~"];

    var form = $('#edit-product-form');
    var formData = new FormData(form.get(0)); //add .get(0)
    formData.append('productData', pData);
    formData.append('PImg1', mainImage);
    formData.append('PImg2', secondImageA);
    formData.append('PImg3', secondImageB);
    formData.append('PImg4', secondImageC);

    $.ajax({
        type: "POST",
        url: "/json/editarproducto",
        processData: false,
        contentType: false,
        data:
            formData
        ,
        success: function (data) {
            divtext.innerHTML = "<p>Producto actualizado exitosamente.</p>\n";
            $('.modal-header').css('background-color', 'blue');
            $('.modal-footer').css('background-color', 'blue');
            $('#validacion').modal('show');
            window.clearTimeout(timer);
            setTimeout(function () {
                $("#cerrar-validacion").click();
                document.getElementById("ResetBtn").click();
                document.getElementById("selector-productos").selectedIndex = 0;
                LimpiarListaProductos("selector-productos");
                document.getElementById("boton-editar").style.display = "none";
                document.getElementById("ResetBtn").style.display = "none";
                UpdateProductList();
            }, 2000); // <-- time in milliseconds
        },
        error: function (xhr, status, error) {

        }
    });
}

// ------------------------------------------------- ELIMINAR PRODUCTO ---------------------------------------------- //

function EliminarProducto() {

    var divtext = document.getElementById("validaciones-contenido");
    var timer = null;
    var pName = document.getElementById("nombre_producto").textContent;
    
    $.ajax({
        type: "POST",
        url: "/json/eliminarproducto",
        data: AddAntiForgeryToken({ productName: pName }),
        success: function (data) {
            divtext.innerHTML = "<p>Producto eliminado exitosamente.</p>\n";
            $('.modal-header').css('background-color', 'blue');
            $('.modal-footer').css('background-color', 'blue');
            $('#validacion').modal('show');
            window.clearTimeout(timer);
            timer = setTimeout(function () {
                $("#cerrar-validacion").click();
                document.getElementById("ResetBtn").click();
                LimpiarListaProductos("selector-productos");
                UpdateProductList();
            }, 2000); // <-- time in milliseconds
        },
        error: function (xhr, status, error) {

        }
    });
}

// ---------------------------------------------------- ENGLISH AJAX CALLS ----------------------------------------------- //

// --------------------------------------------- VALIDATE PRODUCT ADDITION ------------------------------------ //

function RegisterProduct() {
    var product = document.getElementById("nombre_producto").value;
    var divtext = document.getElementById("validaciones-contenido");
    var pPrice = document.getElementById("precio_producto").value;

    $.ajax({
        type: "POST",
        url: "/json/verifyproduct",
        dataType: "json",
        data: AddAntiForgeryToken({ productName: product }),
        success: function (json) {
            var data = JSON.parse(json)
            if (data.name == true) {
                divtext.innerHTML = "<p>There's already a product registered under this name.</p>\n";
                $('.modal-header').css('background-color', 'red');
                $('.modal-footer').css('background-color', 'red');
                $('#validacion').modal('show');
                window.clearTimeout(timer);
                timer = setTimeout(function () {
                    $("#cerrar-validacion").click();
                }, 5000); // <-- time in milliseconds
                return false;
            }
            if (pPrice < data.minimo && data.minimo != null && data.minimo != "") {
                divtext.innerHTML = "<p>The inserted price is under the minimum limit.</p>\n";
                divtext.innerHTML += "<p>Minimum Price: " + data.minimo + " BsS.</p>\n";
                $('.modal-header').css('background-color', 'red');
                $('.modal-footer').css('background-color', 'red');
                $('#validacion').modal('show');
                window.clearTimeout(timer);
                timer = setTimeout(function () {
                    $("#cerrar-validacion").click();
                }, 5000); // <-- time in milliseconds
                return false;
            }
            if (pPrice > data.maximo && data.maximo != null && data.maximo != "") {
                divtext.innerHTML = "<p>The inserted price is over the maximum limit.</p>\n";
                divtext.innerHTML += "<p>Maximum Price: " + data.maximo + " BsS.</p>\n";
                $('.modal-header').css('background-color', 'red');
                $('.modal-footer').css('background-color', 'red');
                $('#validacion').modal('show');
                window.clearTimeout(timer);
                timer = setTimeout(function () {
                    $("#cerrar-validacion").click();
                }, 5000); // <-- time in milliseconds
                return false;
            }
            else {
                ProcessAddition();
            }
        },
        error: function (data) {
            alert("failure");
            return false;
        }
    });
    return false;
}

// --------------------------------------------------- PROCESS PRODUCT ADDITION ------------------------------------------ //

function ProcessAddition() {

    var divtext = document.getElementById("validaciones-contenido");

    var pName = document.getElementById("nombre_producto").value;
    var description = document.getElementById("descripcion_producto").value;
    var pPrice = document.getElementById("precio_producto").value;
    var pValue = parseFloat(pPrice).toFixed(2);

    var mainImage = null, secondImageA = null, secondImageB = null, secondImageC = null;

    if (document.getElementById("PImg1").files[0] != null) {
        mainImage = document.getElementById("PImg1").files[0];
    }
    if (document.getElementById("PImg2").files[0] != null) {
        secondImageA = document.getElementById("PImg2").files[0];
    }
    if (document.getElementById("PImg3").files[0] != null) {
        secondImageB = document.getElementById("PImg3").files[0];
    }
    if (document.getElementById("PImg4").files[0] != null) {
        secondImageC = document.getElementById("PImg4").files[0];
    }

    var model = {
        name: pName,
        description: description,
        price: pValue
    };

    var form = $('#add-product-form')
    var formData = new FormData(form.get(0)); //add .get(0)
    formData.append('PImg1', mainImage);
    formData.append('PImg2', secondImageA);
    formData.append('PImg3', secondImageB);
    formData.append('PImg4', secondImageC);

    $.ajax({
        type: "POST",
        url: "/json/agregarproducto",
        processData: false,
        contentType: false,
        data: formData,
        success: function (data) {
            divtext.innerHTML = "<p>Product added successfully.</p>\n";
            $('.modal-header').css('background-color', 'blue');
            $('.modal-footer').css('background-color', 'blue');
            $('#validacion').modal('show');
            window.clearTimeout(timer);
            setTimeout(function () {
                $("#cerrar-validacion").click();
                document.getElementById("ResetBtn").click();
            }, 2000); // <-- time in milliseconds
        },
        error: function (request, status, error) {
            return false;
        }
    });
}




// --------------------------------------------------- VALIDATE PRODUCT EDIT ---------------------------------------- //

function ValidateProductEdit() {
    var product = document.getElementById("nombre_producto").value;
    var divtext = document.getElementById("validaciones-contenido");
    var pPrice = document.getElementById("precio_producto").value;
    var selectedProduct = document.getElementById("selector-productos").value;

    $.ajax({
        type: "POST",
        url: "/json/verifyproduct",
        dataType: "json",
        data: AddAntiForgeryToken({ productName: product }),
        success: function (json) {
            var data = JSON.parse(json)
            if (data.name == true && selectedProduct.toLowerCase() != product.toLowerCase()) {
                divtext.innerHTML = "<p>There's already a product registered under this name.</p>\n";
                $('.modal-header').css('background-color', 'red');
                $('.modal-footer').css('background-color', 'red');
                $('#validacion').modal('show');
                window.clearTimeout(timer);
                timer = setTimeout(function () {
                    $("#cerrar-validacion").click();
                }, 3000); // <-- time in milliseconds
                return false;
            }
            if (pPrice < data.minimo && data.minimo != null && data.minimo != "") {
                divtext.innerHTML = "<p>The inserted price is under the minimum limit.</p>\n";
                divtext.innerHTML += "<p>Minimum Price: " + data.minimo + " BsS.</p>\n";
                $('.modal-header').css('background-color', 'red');
                $('.modal-footer').css('background-color', 'red');
                $('#validacion').modal('show');
                window.clearTimeout(timer);
                timer = setTimeout(function () {
                    $("#cerrar-validacion").click();
                }, 3000); // <-- time in milliseconds
                return false;
            }
            if (pPrice > data.maximo && data.maximo != null && data.maximo != "") {
                divtext.innerHTML = "<p>The inserted price is over the maximum limit.</p>\n";
                divtext.innerHTML += "<p>Maximum Price: " + data.maximo + " BsS.</p>\n";
                $('.modal-header').css('background-color', 'red');
                $('.modal-footer').css('background-color', 'red');
                $('#validacion').modal('show');
                window.clearTimeout(timer);
                timer = setTimeout(function () {
                    $("#cerrar-validacion").click();
                }, 3000); // <-- time in milliseconds
                return false;
            }
            else {
                ProcessProductEdit();
            }

        },
        error: function (data) {
            alert("failure");
            return false;
        }
    });
    return false;
}

// --------------------------------------------------- PROCESS PRODUCT EDIT --------------------------------------------- //

function ProcessProductEdit() {

    var divtext = document.getElementById("validaciones-contenido");

    var mainImage = null, secondImageA = null, secondImageB = null, secondImageC = null;

    var oldName = document.getElementById("selector-productos").value;
    var pName = document.getElementById("nombre_producto").value;
    var pDescription = document.getElementById("descripcion_producto").value;
    var pPrice = document.getElementById("precio_producto").value;
    var pValue = parseFloat(pPrice).toFixed(2);

    if (document.getElementById("load-main-img").style.backgroundImage != "none") {
        mainImage = document.getElementById("load-main-img").style.backgroundImage;
    }
    if (document.getElementById("load-second-imageA").style.backgroundImage != "none") {
        secondImageA = document.getElementById("load-second-imageA").style.backgroundImage;
    }
    if (document.getElementById("load-second-imageB").style.backgroundImage != "none") {
        secondImageB = document.getElementById("load-second-imageB").style.backgroundImage;
    }
    if (document.getElementById("load-second-imageC").style.backgroundImage != "none") {
        secondImageC = document.getElementById("load-second-imageC").style.backgroundImage;
    }

    var pData = [oldName + "~", pName + "~", pDescription + "~", pValue + "~"];

    var form = $('#edit-product-form');
    var formData = new FormData(form.get(0)); //add .get(0)
    formData.append('productData', pData);
    formData.append('PImg1', mainImage);
    formData.append('PImg2', secondImageA);
    formData.append('PImg3', secondImageB);
    formData.append('PImg4', secondImageC);

    $.ajax({
        type: "POST",
        url: "/json/editarproducto",
        processData: false,
        contentType: false,
        data:
            formData
        ,
        success: function (data) {
            divtext.innerHTML = "<p>Product updated successfully.</p>\n";
            $('.modal-header').css('background-color', 'blue');
            $('.modal-footer').css('background-color', 'blue');
            $('#validacion').modal('show');
            window.clearTimeout(timer);
            setTimeout(function () {
                $("#cerrar-validacion").click();
                document.getElementById("ResetBtn").click();
                document.getElementById("selector-productos").selectedIndex = 0;
                LimpiarListaProductos("selector-productos");
                document.getElementById("boton-editar").style.display = "none";
                document.getElementById("ResetBtn").style.display = "none";
                UpdateProductList();
            }, 2000); // <-- time in milliseconds
        },
        error: function (xhr, status, error) {

        }
    });
}

// ------------------------------------------------- DELETE PRODUCT ---------------------------------------------- //

function DeleteProduct() {

    var divtext = document.getElementById("validaciones-contenido");
    var timer = null;
    var pName = document.getElementById("nombre_producto").textContent;

    $.ajax({
        type: "POST",
        url: "/json/eliminarproducto",
        data: AddAntiForgeryToken({ productName: pName }),
        success: function (data) {
            divtext.innerHTML = "<p>Product deleted successfully.</p>\n";
            $('.modal-header').css('background-color', 'blue');
            $('.modal-footer').css('background-color', 'blue');
            $('#validacion').modal('show');
            window.clearTimeout(timer);
            timer = setTimeout(function () {
                $("#cerrar-validacion").click();
                document.getElementById("ResetBtn").click();
                LimpiarListaProductos("selector-productos");
                UpdateProductList();
            }, 2000); // <-- time in milliseconds
        },
        error: function (xhr, status, error) {

        }
    });
}
