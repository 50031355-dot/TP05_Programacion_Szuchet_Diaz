// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function VerificarUsuario(){
    let nombreUsuario = document.getElementById("nombreUsuario").value;
    let contraseña = document.getElementById("contraseña").value;
    

    if (nombreUsuario.length < 8 || contraseña.length < 8 || nombreUsuario.trim() === "" || contraseña.trim() === "" || !caracteresValidos.test(nombreUsuario) || !caracteresValidos.test(contraseña)) {
        console.error("Error: El nombre de usuario y la contraseña deben tener al menos 8 caracteres.");
        return false;
    }

    console.log("Éxito: El nombre de usuario y la contraseña cumplen con todos los requisitos.");
    return true;
}