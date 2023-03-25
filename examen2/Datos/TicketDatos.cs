using Clases;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Datos
{
    public class TicketDatos
    {
        public async Task<DataTable> DevolverTicketsAsync()
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "SELECT * FROM Ticket;";
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

        public async Task<bool> InsertarNuevoTicketAsync(Tickets tickets)
        {
            bool insert = false;
            try
            {
                string sql = "INSERT INTO Ticket VALUES (@Id, @IdentidadCliente, @NombreCliente, @Fecha, @TipoSoporte, @DescripcionProblema, @Costo, @DescripcionSolucion);";
                using (MySqlConnection _Conexion = new MySqlConnection(CadenaConexion.Cadena))
                {
                    await _Conexion.OpenAsync();
                    using (MySqlCommand comando = new MySqlCommand(sql, _Conexion))
                    {
                        comando.CommandType = System.Data.CommandType.Text;
                        comando.Parameters.Add("Id", MySqlDbType.Int64).Value = tickets.Id;
                        comando.Parameters.Add("IdentidadCliente", MySqlDbType.VarChar, 25).Value = tickets.IdentidadCliente;
                        comando.Parameters.Add("NombreCliente", MySqlDbType.VarChar, 60).Value = tickets.NombreCliente;
                        comando.Parameters.Add("Fecha", MySqlDbType.DateTime).Value = tickets.Fecha;
                        comando.Parameters.Add("TipoSoporte", MySqlDbType.VarChar, 150).Value = tickets.TipoSoporte;
                        comando.Parameters.Add("DescripcionProblema", MySqlDbType.VarChar, 250).Value = tickets.DescripcionProblema;
                        comando.Parameters.Add("Costo", MySqlDbType.Decimal).Value = tickets.Costo;
                        comando.Parameters.Add("DescripcionSolucion", MySqlDbType.VarChar, 250).Value = tickets.DescripcionSolucion;
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

        public async Task<bool> ActualizarTicketAsync(Tickets tickets)
        {
            bool actualizo = false;
            try
            {
                string sql = "UPDATE Ticket SET Id = @Id, IdentidadCliente = @IdentidadCliente, NombreCliente = @NombreCliente, Fecha = @Fecha, " +
                    "TipoSoporte = @TipoSoporte, TipoEquipo = @TipoEquipo, DescripcionProblema = @DescripcionProblema, " +
                    "Costo = @Costo, DescripcionSolucion = @DescripcionSolucion);";
                using (MySqlConnection _Conexion = new MySqlConnection(CadenaConexion.Cadena))
                {
                    await _Conexion.OpenAsync();
                    using (MySqlCommand comando = new MySqlCommand(sql, _Conexion))
                    {
                        comando.CommandType = System.Data.CommandType.Text;
                        comando.Parameters.Add("Id", MySqlDbType.Int32).Value = tickets.Id;
                        comando.Parameters.Add("IdentidadCliente", MySqlDbType.VarChar, 25).Value = tickets.IdentidadCliente;
                        comando.Parameters.Add("NombreCliente", MySqlDbType.VarChar, 60).Value = tickets.NombreCliente;
                        comando.Parameters.Add("Fecha", MySqlDbType.DateTime).Value = tickets.Fecha;
                        comando.Parameters.Add("TipoSoporte", MySqlDbType.VarChar, 150).Value = tickets.TipoSoporte;
                        comando.Parameters.Add("DescripcionProblema", MySqlDbType.VarChar, 250).Value = tickets.DescripcionProblema;
                        comando.Parameters.Add("Costo", MySqlDbType.Decimal).Value = tickets.Costo;
                        comando.Parameters.Add("DescripcionSolucion", MySqlDbType.VarChar, 250).Value = tickets.DescripcionSolucion;
                        await comando.ExecuteNonQueryAsync();
                        actualizo = true;

                    }
                }
            }
            catch (Exception)
            {
            }
            return actualizo;
        }

        public async Task<bool> EliminarTicketAsync(string id)
        {
            bool elimino = false;
            try
            {
                string sql = "DELETE FROM Ticket WHERE Id = @Id;";
                using (MySqlConnection _Conexion = new MySqlConnection(CadenaConexion.Cadena))
                {
                    await _Conexion.OpenAsync();
                    using (MySqlCommand comando = new MySqlCommand(sql, _Conexion))
                    {
                        comando.CommandType = System.Data.CommandType.Text;
                        comando.Parameters.Add("Id", MySqlDbType.Int64).Value = id;

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