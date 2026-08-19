// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function VerificarCaracteres() {
    const nombreUsuario = document.querySelector('input[name="nombreUsuario"]')?.value.trim() || "";
    const contraseña = document.querySelector('input[name="contrasena"], input[name="contraseña"]')?.value.trim() || "";
    const caracteresValidos = /^[a-zA-Z0-9_.-]+$/;

    if (nombreUsuario.length < 8 || 
        contraseña.length < 8 || 
        nombreUsuario.trim() === "" || 
        contraseña.trim() === "" || 
        !caracteresValidos.test(nombreUsuario) || 
        !caracteresValidos.test(contraseña)) {
        document.getElementById("secreto").innerText = "Error: El nombre de usuario y la contraseña deben tener al menos 8 caracteres y no tener caracteres inválidos.";
        console.error("Error: El nombre de usuario y la contraseña deben tener al menos 8 caracteres y no tener caracteres inválidos.");
        
        return false;
    }

    console.log("Éxito: El nombre de usuario y la contraseña cumplen con todos los requisitos.");
    return true;
}
