// /Comun/js/comun.js
function initializeLogout() {
    // Uso event delegation para manejar elementos cargados dinámicamente
    $(document).on('click', '#logoutLink', async function (e) {
        e.preventDefault();

        const result = await Swal.fire({
            title: '¿Cerrar sesión?',
            text: "¿Está seguro que desea cerrar sesión?",
            icon: 'question',
            showCancelButton: true,
            confirmButtonColor: '#0d6efd',
            cancelButtonColor: '#6c757d',
            confirmButtonText: 'Sí, cerrar sesión',
            cancelButtonText: 'Cancelar'
        });

        if (result.isConfirmed) {
            try {
                await $.ajax({
                    url: "/api/Usuario/Logout",
                    type: "GET"
                });

                window.location.href = "/Login/Login.html";
            } catch (error) {
                console.error('Error al cerrar sesión:', error);
                await Swal.fire({
                    icon: 'error',
                    title: 'Error',
                    text: 'No se pudo cerrar la sesión correctamente',
                    confirmButtonColor: '#0d6efd'
                });
            }
        }
    });
}

// Checkeo de sesión
async function checkSession() {
    try {
        const response = await $.ajax({
            url: "/api/Usuario/CheckSession",
            type: "GET",
            dataType: "json"
        });

        if (!response) {
            await Swal.fire({
                icon: 'warning',
                title: 'Sesión expirada',
                text: 'Por favor, inicie sesión nuevamente',
                confirmButtonColor: '#0d6efd'
            });
            window.location.href = "/Login/Login.html";
        }
        return true;
    } catch (error) {
        window.location.href = "/Login/Login.html";
        return false;
    }
}

// Inicializo el navbar
function initializeNavbar() {
    return new Promise((resolve) => {
        $('#navbar-container').load('/Nav/Navbar.html', function () {
            // Después de cargar el navbar, verificamos permisos de admin
            setTimeout(async () => {
                try {
                    const isAdmin = await checkAdminRole();
                    if (isAdmin) {
                        $('#mantenimientoDropdown').show();
                    }
                } catch (error) {
                    console.log('Error verificando rol admin:', error);
                }
            }, 100);

            resolve();
        });
    });
}

// Inicializo funcionalidades comunes
async function initializeCommon() {
    // Checkeo si SweetAlert2 está cargado y funcando
    if (typeof Swal === 'undefined') {
        console.error('SweetAlert2 is not loaded. Toast functions will not work properly.');
    }

    const isLoggedIn = await checkSession();
    if (isLoggedIn) {
        await initializeNavbar();
        initializeLogout();
        initializeToasts();

        // Verificar permisos de admin para páginas específicas
        const currentPath = window.location.pathname;
        if (currentPath.includes('/Mantenimiento/')) {
            const isAdmin = await checkAdminRole();
            if (!isAdmin) {
                await Swal.fire({
                    icon: 'error',
                    title: 'Acceso Denegado',
                    text: 'No tiene permisos para acceder a esta página',
                    confirmButtonColor: '#0d6efd'
                });
                window.location.href = "/Inicio.html";
                return false;
            }
        }
    }
    return isLoggedIn;
}

// Sistema de gestión de cola de toasts
let toastQueue = [];
let isToastDisplaying = false;
let toastTracker = {}; // Objeto para trackear tipos de toast específicos o estados

// Initialize toast system
function initializeToasts() {
    // Reset toast state
    toastQueue = [];
    isToastDisplaying = false;
    toastTracker = {};

    // Log toast initialization
    console.log('Toast system initialized');
}

// Funciones para mostrar toasts globales
function toastTopDerecha(texto, titulo, options = {}) {
    if (typeof Swal === 'undefined') {
        console.error('SweetAlert2 not loaded. Cannot show toast.');
        return false;
    }

    // Trackeo de un toast específico si se pide
    if (options.trackId) {
        toastTracker[options.trackId] = true;
    }

    // Añado el toast a la cola
    toastQueue.push({
        icon: 'warning',
        title: titulo,
        text: texto,
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: options.timer || 3500,
        timerProgressBar: true
    });

    // Si no hay ningún toast mostrandose, muestro el próximo
    if (!isToastDisplaying) {
        displayNextToast();
    }

    // devolver un flag que se pueda chequear en otro lado
    return true;
}

function toastExito(texto, titulo, options = {}) {
    if (typeof Swal === 'undefined') {
        console.error('SweetAlert2 not loaded. Cannot show toast.');
        return false;
    }

    // Resetear el tracking de un toast específico si se pide
    if (options.resetTrackIds && Array.isArray(options.resetTrackIds)) {
        options.resetTrackIds.forEach(id => {
            toastTracker[id] = false;
        });
    }

    // Añado el toast a la cola con los parámetros por defecto
    toastQueue.push({
        icon: 'success',
        title: titulo,
        text: texto,
        toast: true,
        position: 'top-end',
        showConfirmButton: options.showConfirmButton !== false,
        timer: options.timer || 3500,
        timerProgressBar: true
    });

    // Si no hay ningún toast mostrandose, muestro el próximo
    if (!isToastDisplaying) {
        displayNextToast();
    }

    return true;
}

function toastError(texto, titulo, options = {}) {
    if (typeof Swal === 'undefined') {
        console.error('SweetAlert2 not loaded. Cannot show toast.');
        return false;
    }

    // Añado el toast a la cola con los parámetros por defecto
    toastQueue.push({
        icon: 'error',
        title: titulo,
        text: texto,
        toast: true,
        position: 'top-end',
        showConfirmButton: options.showConfirmButton !== false,
        timer: options.timer || 5000, // Longer timer for errors
        timerProgressBar: true
    });

    // Si no hay ningún toast mostrandose, muestro el próximo
    if (!isToastDisplaying) {
        displayNextToast();
    }

    return true;
}

function toastInfo(texto, titulo, options = {}) {
    if (typeof Swal === 'undefined') {
        console.error('SweetAlert2 not loaded. Cannot show toast.');
        return false;
    }

    // Añado el toast a la cola con los parámetros por defecto
    toastQueue.push({
        icon: 'info',
        title: titulo,
        text: texto,
        toast: true,
        position: 'top-end',
        showConfirmButton: options.showConfirmButton !== false,
        timer: options.timer || 3500,
        timerProgressBar: true
    });

    // Si no hay ningún toast mostrandose, muestro el próximo
    if (!isToastDisplaying) {
        displayNextToast();
    }

    return true;
}

function displayNextToast() {
    if (toastQueue.length === 0) {
        isToastDisplaying = false;
        return;
    }

    isToastDisplaying = true;
    const toastConfig = toastQueue.shift();

    // Me fijo que el Swal 2 esté cargado y funcando
    if (typeof Swal === 'undefined') {
        console.error('SweetAlert2 not loaded. Cannot display toast.');
        isToastDisplaying = false;
        return;
    }

    Swal.fire(toastConfig).then(() => {
        // un delay entre toasts para una mejor UX
        setTimeout(() => {
            displayNextToast();
        }, 300);
    }).catch(error => {
        console.error('Error displaying toast:', error);
        isToastDisplaying = false;
    });
}

// Con esta checkeo si un toast específico ya se mostró
function isToastShown(trackId) {
    return !!toastTracker[trackId];
}

// Con esta función reseteo el tracking de toasts
function resetToastTracking(trackId = null) {
    if (trackId) {
        toastTracker[trackId] = false;
    } else {
        toastTracker = {};
    }
}

document.addEventListener('DOMContentLoaded', function () {
    console.log('Common.js loaded - initializing common functionality');
    initializeCommon().catch(error => {
        console.error('Error initializing common functionality:', error);
    });
});

function validarCampoNumerico(valor, nombreCampo = "campo") {
    const soloNumeros = valor.replace(/\D/g, "");

    if (soloNumeros !== valor) {
        toastTopDerecha(`El ${nombreCampo} debe contener solo números`, "Sólo números");
        return { isValid: false, cleanValue: soloNumeros };
    }

    if (soloNumeros === "") {
        return { isValid: false, cleanValue: soloNumeros };
    }

    return { isValid: true, cleanValue: soloNumeros };
}

function validarCodigo(codigo) {
    return validarCampoNumerico(codigo, "código");
}

function validarExistencia(existencia) {
    return validarCampoNumerico(existencia, "existencia");
}

function validarPrecio(precioOriginal, nombreCampo = "precio") {
    // Detectar si hay caracteres inválidos
    const contieneCaracteresInvalidos = /[^0-9.,]/.test(precioOriginal);

    // Limpiar el valor dejando solo números y los separadores
    let limpio = precioOriginal.replace(/[^0-9.,]/g, "");

    // Solo permitir un separador decimal (el primero encontrado entre punto o coma)
    const partes = limpio.split(/[,\.]/);
    if (partes.length > 2) {
        limpio = partes[0] + "," + partes[1]; // Uso coma como separador
    }

    // Mostrar alerta solo si hubo caracteres inválidos
    if (contieneCaracteresInvalidos) {
        toastTopDerecha(`El ${nombreCampo} debe contener solo números y/o decimales.`, "Sólo números");
    }

    // Unificamos el separador como coma (para visualización)
    limpio = limpio.replace('.', ',');

    // Si el resultado está vacío, devolver información de validación
    if (limpio.trim() === "") {
        return { isValid: false, cleanValue: limpio, numericValue: null };
    }

    // Obtener valor numérico para cálculos (reemplazando coma por punto)
    const numericValue = parseFloat(limpio.replace(',', '.'));

    return {
        isValid: !isNaN(numericValue),
        cleanValue: limpio,
        numericValue: isNaN(numericValue) ? null : numericValue
    };
}

function validarPrecioLista(precioOriginal) {
    return validarPrecio(precioOriginal, "precio lista");
}

function calcularPrecios(precioStr, callback = null) {
    // Reemplaza la coma por punto para parseo
    const precio = parseFloat(precioStr.replace(',', '.'));

    if (!isNaN(precio)) {
        const precioCalculado = (precio * 1.15).toFixed(2).replace('.', ',');
        const contadoCalculado = precio.toFixed(2).replace('.', ',');

        const result = {
            precio: precioCalculado,
            contado: contadoCalculado,
            precioNumeric: precio * 1.15,
            contadoNumeric: precio
        };

        // Si se proporciona un callback, ejecutarlo con los resultados
        if (callback && typeof callback === 'function') {
            callback(result);
        }

        return result;
    } else {
        const result = {
            precio: "",
            contado: "",
            precioNumeric: null,
            contadoNumeric: null
        };

        if (callback && typeof callback === 'function') {
            callback(result);
        }

        return result;
    }
}

function setupNumericFieldValidation(selector, validationFunction, onValidCallback = null) {
    $(document).on("input", selector, function () {
        const originalValue = $(this).val();
        const validation = validationFunction(originalValue);

        if (validation.cleanValue !== originalValue) {
            $(this).val(validation.cleanValue);
        }

        if (onValidCallback && typeof onValidCallback === 'function' && validation.isValid) {
            onValidCallback(validation, $(this));
        }
    });
}

function setupPriceFieldValidation(selector, targetPriceSelector = null, targetContadoSelector = null) {
    $(document).on("input", selector, function () {
        const originalValue = $(this).val();
        const validation = validarPrecioLista(originalValue);

        if (validation.cleanValue !== originalValue) {
            $(this).val(validation.cleanValue);
        }

        if (validation.isValid && validation.numericValue !== null) {
            const precios = calcularPrecios(validation.cleanValue);

            if (targetPriceSelector) {
                $(targetPriceSelector).val(precios.precio);
            }
            if (targetContadoSelector) {
                $(targetContadoSelector).val(precios.contado);
            }
        } else {
            if (targetPriceSelector) {
                $(targetPriceSelector).val("");
            }
            if (targetContadoSelector) {
                $(targetContadoSelector).val("");
            }
        }
    });

}

function validarPorcentaje(porcentajeOriginal) {
    const contieneCaracteresInvalidos = /[^0-9.,]/.test(porcentajeOriginal);

    let limpio = porcentajeOriginal.replace(/[^0-9.,]/g, "");

    const partes = limpio.split(/[,\.]/);
    if (partes.length > 2) {
        limpio = partes[0] + "," + partes[1];
    }

    if (contieneCaracteresInvalidos && porcentajeOriginal !== '') {
        toastTopDerecha('El porcentaje debe contener solo números y/o decimales.', "Sólo números");
    }

    limpio = limpio.replace('.', ',');

    if (limpio.trim() === "") {
        return { isValid: true, cleanValue: '0', numericValue: 0 };
    }

    const numericValue = parseFloat(limpio.replace(',', '.'));

    if (numericValue > 100) {
        toastTopDerecha('El porcentaje no puede ser mayor a 100%', "Porcentaje inválido");
        return { isValid: false, cleanValue: '100', numericValue: 100 };
    }

    return {
        isValid: !isNaN(numericValue),
        cleanValue: limpio,
        numericValue: isNaN(numericValue) ? 0 : numericValue
    };
}

async function checkAdminRole() {
    try {
        const response = await $.ajax({
            url: "/api/Usuario/CheckAdminRole",
            type: "GET",
            dataType: "json"
        });
        return response === true;
    } catch (error) {
        console.log('Error verificando rol admin:', error);
        return false;
    }
}

window.checkAdminRole = checkAdminRole;
window.toastTopDerecha = toastTopDerecha;
window.toastExito = toastExito;
window.toastError = toastError;
window.toastInfo = toastInfo;
window.isToastShown = isToastShown;
window.resetToastTracking = resetToastTracking;
window.initializeCommon = initializeCommon; 

window.validarCampoNumerico = validarCampoNumerico;
window.validarCodigo = validarCodigo;
window.validarExistencia = validarExistencia;
window.validarPrecio = validarPrecio;
window.validarPrecioLista = validarPrecioLista;
window.calcularPrecios = calcularPrecios;
window.setupNumericFieldValidation = setupNumericFieldValidation;
window.setupPriceFieldValidation = setupPriceFieldValidation;

// Initialize logout functionality
/*
function initializeLogout() {
    // Use event delegation to handle dynamically loaded elements
    $(document).on('click', '#logoutLink', async function (e) {
        e.preventDefault();

        const result = await Swal.fire({
            title: '¿Cerrar sesión?',
            text: "¿Está seguro que desea cerrar sesión?",
            icon: 'question',
            showCancelButton: true,
            confirmButtonColor: '#0d6efd',
            cancelButtonColor: '#6c757d',
            confirmButtonText: 'Sí, cerrar sesión',
            cancelButtonText: 'Cancelar'
        });

        if (result.isConfirmed) {
            try {
                await $.ajax({
                    url: "/api/Usuario/Logout",
                    type: "GET"
                });

                window.location.href = "/Login/Login.html";
            } catch (error) {
                console.error('Error al cerrar sesión:', error);
                await Swal.fire({
                    icon: 'error',
                    title: 'Error',
                    text: 'No se pudo cerrar la sesión correctamente',
                    confirmButtonColor: '#0d6efd'
                });
            }
        }
    });
}

// Check session function
async function checkSession() {
    try {
        const response = await $.ajax({
            url: "/api/Usuario/CheckSession",
            type: "GET",
            dataType: "json"
        });

        if (!response) {
            await Swal.fire({
                icon: 'warning',
                title: 'Sesión expirada',
                text: 'Por favor, inicie sesión nuevamente',
                confirmButtonColor: '#0d6efd'
            });
            window.location.href = "/Login/Login.html";
        }
        return true;
    } catch (error) {
        window.location.href = "/Login/Login.html";
        return false;
    }
}

// Initialize navbar
function initializeNavbar() {
    return new Promise((resolve) => {
        $('#navbar-container').load('/Nav/Navbar.html', function () {
            resolve();
        });
    });
}

// Initialize common functionality
async function initializeCommon() {
    const isLoggedIn = await checkSession();
    if (isLoggedIn) {
        await initializeNavbar();
        initializeLogout();
    }
    return isLoggedIn;
}

// Toast queue management system
let toastQueue = [];
let isToastDisplaying = false;
let toastTracker = {}; // Object to track specific toast types or states

// Global toast functions
function toastTopDerecha(texto, titulo, options = {}) {
    // Track specific types of toasts if tracking ID is provided
    if (options.trackId) {
        toastTracker[options.trackId] = true;
    }

    // Add toast to queue
    toastQueue.push({
        icon: 'warning',
        title: titulo,
        text: texto,
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: options.timer || 3500,
        timerProgressBar: true
    });

    // If no toast is currently displaying, start displaying
    if (!isToastDisplaying) {
        displayNextToast();
    }

    // Return a flag that can be checked elsewhere
    return true;
}

function toastExito(texto, titulo, options = {}) {
    // Reset specific toast tracking if needed
    if (options.resetTrackIds && Array.isArray(options.resetTrackIds)) {
        options.resetTrackIds.forEach(id => {
            toastTracker[id] = false;
        });
    }

    // Add toast to queue
    toastQueue.push({
        icon: 'success',
        title: titulo,
        text: texto,
        showConfirmButton: options.showConfirmButton !== false
    });

    // If no toast is currently displaying, start displaying
    if (!isToastDisplaying) {
        displayNextToast();
    }
}

function toastError(texto, titulo, options = {}) {
    // Add toast to queue
    toastQueue.push({
        icon: 'error',
        title: titulo,
        text: texto,
        showConfirmButton: options.showConfirmButton !== false
    });

    // If no toast is currently displaying, start displaying
    if (!isToastDisplaying) {
        displayNextToast();
    }
}

function toastInfo(texto, titulo, options = {}) {
    // Add toast to queue
    toastQueue.push({
        icon: 'info',
        title: titulo,
        text: texto,
        showConfirmButton: options.showConfirmButton !== false
    });

    // If no toast is currently displaying, start displaying
    if (!isToastDisplaying) {
        displayNextToast();
    }
}

function displayNextToast() {
    if (toastQueue.length === 0) {
        isToastDisplaying = false;
        return;
    }

    isToastDisplaying = true;
    const toastConfig = toastQueue.shift();

    Swal.fire(toastConfig).then(() => {
        // Small delay between toasts for better UX
        setTimeout(() => {
            displayNextToast();
        }, 300);
    });
}

// Function to check if a specific toast has been shown
function isToastShown(trackId) {
    return !!toastTracker[trackId];
}

// Function to reset toast tracking
function resetToastTracking(trackId = null) {
    if (trackId) {
        toastTracker[trackId] = false;
    } else {
        toastTracker = {};
    }
}

// Export the toast functions to window for global access
window.toastTopDerecha = toastTopDerecha;
window.toastExito = toastExito;
window.toastError = toastError;
window.toastInfo = toastInfo;
window.isToastShown = isToastShown;
window.resetToastTracking = resetToastTracking;
*/