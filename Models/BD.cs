using Dapper;
using Microsoft.Data.SqlClient;

public static class BD
{
    private static string _connectionString =
        @"Server=localhost;DataBase=tp05;Integrated Security=True;TrustServerCertificate=True;";
    
    public static bool ValidarLogin(string nombreUsuario, string contrasena)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            Console.WriteLine($"Validando login para usuario: {nombreUsuario}, contraseña: {contrasena}");
            string query = "SELECT ID FROM Usuarios WHERE nombreUsuario = @nombreUsuario AND contraseña = @contrasena";
            var usuario = connection.QueryFirstOrDefault<int>(query, new { nombreUsuario, contrasena });
            Console.WriteLine(usuario);
            return usuario > 0;
        }
    }

    public static bool ValidarRegistro(Usuarios u)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            Console.WriteLine($"Validando registro para usuario: {u.nombreUsuario}, contraseña: {u.contraseña}");
            string query = "SELECT ID FROM Usuarios WHERE nombreUsuario = @nombreUsuario AND contraseña = @contrasena";
            var usuario = connection.QueryFirstOrDefault<int>(query, new { nombreUsuario = u.nombreUsuario, contrasena = u.contraseña });
            if (usuario > 0)
            {
                return false;
            }
            else
            {
                string insertQuery = @"
                    INSERT INTO Usuarios
                        (nombreUsuario, contraseña, nombre, apellido, tipoUsuario)
                    VALUES
                        (@nombreUsuario, @contrasena, @nombre, @apellido, @tipoUsuario)";
                connection.Execute(insertQuery, new
                {
                    nombreUsuario = u.nombreUsuario,
                    contrasena = u.contraseña,
                    nombre = u.nombre,
                    apellido = u.apellido,
                    tipoUsuario = u.tipoUsuario
                });
                return true;
            }
        }
    }
    
    
}