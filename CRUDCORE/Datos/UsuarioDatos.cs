using CRUDCORE.Models;
using System.Data;
using System.Data.SqlClient;

namespace CRUDCORE.Datos
{
    public class UsuarioDatos
    {
        // Devuelve el usuario si el usuario/clave son correctos, o null si no lo son.
        public UsuarioModel? ValidarLogin(string nombreUsuario, string clave)
        {
            UsuarioModel? oUsuario = null;
            var claveHash = Seguridad.GenerarHash(clave);

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ValidarLogin", conexion);
                cmd.Parameters.AddWithValue("NombreUsuario", nombreUsuario);
                cmd.Parameters.AddWithValue("ClaveHash", claveHash);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        oUsuario = new UsuarioModel()
                        {
                            IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                            NombreUsuario = dr["NombreUsuario"].ToString(),
                            NombreCompleto = dr["NombreCompleto"].ToString()
                        };
                    }
                }
            }

            return oUsuario;
        }
    }
}
