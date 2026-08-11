using Dapper;
using Microsoft.Data.SqlClient;

public static class BD
{
    private static string _connectionString =
        @"Server=localhost;DataBase=TurnosDB;Integrated Security=True;TrustServerCertificate=True;";
    
    public static bool ValidarLogin(string nombreUsuario, string contrasena)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            Console.WriteLine($"Validando login para usuario: {nombreUsuario}, contraseña: {contrasena}");
            var usuario=-1;
            string query = "SELECT ID FROM Usuarios WHERE NombreUsuario = @nombreUsuario AND Contrasena = @contrasena";
            usuario = connection.QueryFirstOrDefault<int>(query, new { nombreUsuario, contrasena });
            return usuario != -1;
        }
    }

    public static bool ValidarRegistro(Usuarios usuario)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            Console.WriteLine($"Validando registro para usuario: {usuario.NombreUsuario}, contraseña: {usuario.Contrasena}");
            var usuario=-1;
            string query = "SELECT ID FROM Usuarios WHERE NombreUsuario = @nombreUsuario AND Contrasena = @contrasena";
            usuario = connection.QueryFirstOrDefault<int>(query, new { nombreUsuario, contrasena });
            if (usuario!=-1)
            {
                return false;
            }
            else
            {
                string insertQuery = "INSERT INTO Usuarios (NombreUsuario, Contrasena) VALUES (@nombreUsuario, @contrasena)";
                connection.Execute(insertQuery, new { nombreUsuario, contrasena });
                return true;
            }
        }
    }
    
    
}