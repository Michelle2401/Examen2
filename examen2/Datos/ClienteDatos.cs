using Clases;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Datos
{
    public class ClienteDatos
    {

        public async Task<DataTable> DevolverClientesAsync()
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT * FROM Cliente;";
                using (MySqlConnection _Conexion = new MySqlConnection(CadenaConexion.cadena))
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

        public async Task<bool> InsertarNuevoClienteAsync(Cliente cliente)
        {
            bool insert = false;
            try
            {
                string sql = "INSERT INTO Cliente VALUES (@Identidad, @Nombre, @Direccion, @Correo);";
                using (MySqlConnection _Conexion = new MySqlConnection(CadenaConexion.cadena))
                {
                    await _Conexion.OpenAsync();
                    using (MySqlCommand comando = new MySqlCommand(sql, _Conexion))
                    {
                        comando.CommandType = System.Data.CommandType.Text;
                        comando.Parameters.Add("Identidad", MySqlDbType.VarChar, 25).Value = cliente.Identidad;
                        comando.Parameters.Add("Nombre", MySqlDbType.VarChar, 60).Value = cliente.Nombre;
                        comando.Parameters.Add("Direccion", MySqlDbType.VarChar, 120).Value = cliente.Direccion;
                        comando.Parameters.Add("Correo", MySqlDbType.VarChar, 40).Value = cliente.Correo;
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

        public async Task<bool> EliminarClienteAsync(string identidad)
        {
            bool elimino = false;
            try
            {
                string sql = "DELETE FROM Cliente WHERE Identidad = @Identidad;";
                using (MySqlConnection _Conexion = new MySqlConnection(CadenaConexion.cadena))
                {
                    await _Conexion.OpenAsync();
                    using (MySqlCommand comando = new MySqlCommand(sql, _Conexion))
                    {
                        comando.CommandType = System.Data.CommandType.Text;
                        comando.Parameters.Add("Identidad", MySqlDbType.VarChar, 25).Value = identidad;

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

        public async Task<Cliente> GetPorIdentidadAsync(string identidad)
        {
            Cliente cliente = new Cliente();
            try
            {
                string sql = "SELECT * FROM Cliente WHERE Identidad = @Identidad;";
                using (MySqlConnection _Conexion = new MySqlConnection(CadenaConexion.cadena))
                {
                    await _Conexion.OpenAsync();
                    using (MySqlCommand comando = new MySqlCommand(sql, _Conexion))
                    {
                        comando.CommandType = System.Data.CommandType.Text;
                        comando.Parameters.Add("@Identidad", MySqlDbType.VarChar, 25).Value = identidad;

                        MySqlDataReader dr = (MySqlDataReader)await comando.ExecuteReaderAsync();
                        if (dr.Read())
                        {
                            cliente.Identidad = dr["Identidad"].ToString();
                            cliente.Nombre = dr["Nombre"].ToString();
                            cliente.Direccion = dr["Direccion"].ToString();
                            cliente.Correo = dr["correo"].ToString();
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            return cliente;
        }
    }
}
