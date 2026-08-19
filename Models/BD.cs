using Dapper;
using Microsoft.Data.SqlClient;
using tp05.Models;

public static class BD
{
    private static string _connectionString =
        @"Server=localhost;DataBase=tp05;Integrated Security=True;TrustServerCertificate=True;";

    public static Usuarios ObtenerUsuarioPorNombre(string nombreUsuario)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            string query = @"
                SELECT ID, nombreUsuario, contraseña, salt, nombre, apellido, tipoUsuario
                FROM Usuarios
                WHERE nombreUsuario = @nombreUsuario";

            return connection.QueryFirstOrDefault<Usuarios>(query, new { nombreUsuario });
        }
    }

    public static bool ValidarLogin(string nombreUsuario, string contrasena)
    {
        var usuario = ObtenerUsuarioPorNombre(nombreUsuario);

        if (usuario == null || string.IsNullOrWhiteSpace(usuario.contraseña) || string.IsNullOrWhiteSpace(usuario.salt))
        {
            return false;
        }

        return PasswordHelper.VerificarPassword(contrasena, usuario.contraseña, usuario.salt);
    }

    public static bool ValidarRegistro(Usuarios u)
    {
        if (ObtenerUsuarioPorNombre(u.nombreUsuario) != null)
        {
            return false;
        }

        var (hash, salt) = PasswordHelper.CrearHash(u.contraseña);

        u.contraseña = hash;
        u.salt = salt;

        using (var connection = new SqlConnection(_connectionString))
        {
            string insertQuery = @"
                INSERT INTO Usuarios
                    (nombreUsuario, contraseña, salt, nombre, apellido, tipoUsuario)
                VALUES
                    (@nombreUsuario, @contrasena, @salt, @nombre, @apellido, @tipoUsuario)";

            connection.Execute(insertQuery, new
            {
                nombreUsuario = u.nombreUsuario,
                contrasena = u.contraseña,
                salt = u.salt,
                nombre = u.nombre,
                apellido = u.apellido,
                tipoUsuario = u.tipoUsuario
            });

            return true;
        }
    }
}