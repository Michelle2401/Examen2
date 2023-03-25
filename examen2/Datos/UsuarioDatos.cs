using Clases;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Datos
{
    public class UsuarioDatos
    {
        public async Task<bool> ValidarUsuarioAsync(string codigo, string clave)
        {
            bool Valido = false;

            try
            {
                string sql = "SELECT 1 FROM Usuario WHERE Codigo = @Codigo AND Contraseña = @Contraseña";
                using (MySqlConnection _Conexion = new MySqlConnection(CadenaConexion.Cadena))
                {
                    await _Conexion.OpenAsync();
                    using (MySqlCommand comando = new MySqlCommand(sql, _Conexion))
                    {
                        comando.CommandType = System.Data.CommandType.Text;
                        comando.Parameters.Add("@Codigo", MySqlDbType.VarChar, 50).Value = codigo;
                        comando.Parameters.Add("@Clave", MySqlDbType.VarChar, 45).Value = clave;

                        Valido = Convert.ToBoolean(await comando.ExecuteScalarAsync());
                    }
                }

            }
            catch (Exception ex)
            {
            }
            return Valido;
        }

        public async Task<DataTable> DevolverUsuariosAsync()
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT * FROM Usuario;";
                using (MySqlConnection _Conexion = new MySqlConnection(CadenaConexion.Cadena))
                {
                    await _Conexion.OpenAsync();
                    using (MySqlCommand comando = new MySqlCommand(sql, _Conexion))
                    {
                        comando.CommandType = System.Data.CommandType.Text;

                        MySqlDataReader dr = (MySqlDataReader)await comando.ExecuteReaderAsync();
                        dt.Load(dr);
                    }
                }

            }
            catch (Exception)
            {
            }
            return dt;
        }

        public async Task<bool> InsertarNuevoUsuarioAsync(Usuario usuario)
        {
            bool insert = false;
            try
            {
                string sql = "INSERT INTO Usuario VALUES (@Codigo, @Nombre, @Correo, @Contraseña);";
                using (MySqlConnection _Conexion = new MySqlConnection(CadenaConexion.Cadena))
                {
                    await _Conexion.OpenAsync();
                    using (MySqlCommand comando = new MySqlCommand(sql, _Conexion))
                    {
                        comando.CommandType = System.Data.CommandType.Text;
                        comando.Parameters.Add("Codigo", MySqlDbType.VarChar, 50).Value = usuario.Codigo;
                        comando.Parameters.Add("Nombre", MySqlDbType.VarChar, 60).Value = usuario.Nombre;
                        comando.Parameters.Add("Email", MySqlDbType.VarChar, 40).Value = usuario.Correo;
                        comando.Parameters.Add("Clave", MySqlDbType.VarChar, 45).Value = usuario.Contraseña;
                        await comando.ExecuteNonQueryAsync();
                        insert = true;

                    }
                }
            }
            catch (Exception)
            {
            }
            return insert;
        }

        public async Task<bool> EliminarUsuarioAsync(string codigo)
        {
            bool elimino = false;
            try
            {
                string sql = "DELETE FROM Usuario WHERE Codigo = @Codigo;";
                using (MySqlConnection _Conexion = new MySqlConnection(CadenaConexion.Cadena))
                {
                    await _Conexion.OpenAsync();
                    using (MySqlCommand comando = new MySqlCommand(sql, _Conexion))
                    {
                        comando.CommandType = System.Data.CommandType.Text;
                        comando.Parameters.Add("Codigo", MySqlDbType.VarChar, 50).Value = codigo;

                        await comando.ExecuteNonQueryAsync();
                        elimino = true;

                    }
                }
            }
            catch (Exception)
            {
            }
            return elimino;
        }
    }

}