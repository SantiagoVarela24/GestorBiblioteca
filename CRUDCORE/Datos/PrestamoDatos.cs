using CRUDCORE.Models;
using System.Data;
using System.Data.SqlClient;

namespace CRUDCORE.Datos
{
    public class PrestamoDatos
    {
        public List<PrestamoModel> Listar()
        {
            var oLista = new List<PrestamoModel>();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ListarPrestamo", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        oLista.Add(Mapear(dr));
                    }
                }
            }

            return oLista;
        }

        public PrestamoModel Obtener(int IdPrestamo)
        {
            var oPrestamo = new PrestamoModel();
            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ObtenerPrestamo", conexion);
                cmd.Parameters.AddWithValue("IdPrestamo", IdPrestamo);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        oPrestamo = Mapear(dr);
                    }
                }
            }

            return oPrestamo;
        }

        public bool Guardar(PrestamoModel oPrestamo)
        {
            bool rpta;
            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_GuardarPrestamo", conexion);
                    cmd.Parameters.AddWithValue("NombrePersona", oPrestamo.NombrePersona);
                    cmd.Parameters.AddWithValue("Telefono", (object?)oPrestamo.Telefono ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("TituloLibro", oPrestamo.TituloLibro);
                    cmd.Parameters.AddWithValue("Autor", (object?)oPrestamo.Autor ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("FechaPrestamo", oPrestamo.FechaPrestamo);
                    cmd.Parameters.AddWithValue("FechaDevolucion", (object?)oPrestamo.FechaDevolucion ?? DBNull.Value);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                rpta = true;
            }
            catch (Exception)
            {
                rpta = false;
            }

            return rpta;
        }

        public bool Editar(PrestamoModel oPrestamo)
        {
            bool rpta;
            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_EditarPrestamo", conexion);
                    cmd.Parameters.AddWithValue("IdPrestamo", oPrestamo.IdPrestamo);
                    cmd.Parameters.AddWithValue("NombrePersona", oPrestamo.NombrePersona);
                    cmd.Parameters.AddWithValue("Telefono", (object?)oPrestamo.Telefono ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("TituloLibro", oPrestamo.TituloLibro);
                    cmd.Parameters.AddWithValue("Autor", (object?)oPrestamo.Autor ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("FechaPrestamo", oPrestamo.FechaPrestamo);
                    cmd.Parameters.AddWithValue("FechaDevolucion", (object?)oPrestamo.FechaDevolucion ?? DBNull.Value);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                rpta = true;
            }
            catch (Exception)
            {
                rpta = false;
            }

            return rpta;
        }

        public bool Eliminar(int IdPrestamo)
        {
            bool rpta;
            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_EliminarPrestamo", conexion);
                    cmd.Parameters.AddWithValue("IdPrestamo", IdPrestamo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                rpta = true;
            }
            catch (Exception)
            {
                rpta = false;
            }

            return rpta;
        }

        public bool MarcarDevuelto(int IdPrestamo)
        {
            bool rpta;
            try
            {
                var cn = new Conexion();

                using (var conexion = new SqlConnection(cn.getCadenaSQL()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("sp_MarcarDevueltoPrestamo", conexion);
                    cmd.Parameters.AddWithValue("IdPrestamo", IdPrestamo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                rpta = true;
            }
            catch (Exception)
            {
                rpta = false;
            }

            return rpta;
        }

        private PrestamoModel Mapear(SqlDataReader dr)
        {
            return new PrestamoModel()
            {
                IdPrestamo = Convert.ToInt32(dr["IdPrestamo"]),
                NombrePersona = dr["NombrePersona"].ToString(),
                Telefono = dr["Telefono"] == DBNull.Value ? "" : dr["Telefono"].ToString(),
                TituloLibro = dr["TituloLibro"].ToString(),
                Autor = dr["Autor"] == DBNull.Value ? "" : dr["Autor"].ToString(),
                FechaPrestamo = Convert.ToDateTime(dr["FechaPrestamo"]),
                FechaDevolucion = dr["FechaDevolucion"] == DBNull.Value ? null : Convert.ToDateTime(dr["FechaDevolucion"]),
                Devuelto = Convert.ToBoolean(dr["Devuelto"])
            };
        }
    }
}
