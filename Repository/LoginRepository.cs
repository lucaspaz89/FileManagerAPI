using FileManagerAPI.Helpers;
using FileManagerAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace FileManagerAPI.Repository
{
    public class LoginRepository : ILoginRepository
    {
        SQLString sqlString = new(); //INSTANCIA PARA ACCEDER A LA CADENA DE CONEXION
        Encrypt encrypt = new(); //INSTANCIA PARA ENCRIPTAR LA CONTRASEÑA

        public async Task<int> Login(UserLogin user)
        {
            int res;

            user.UserPassword = encrypt.ConvertirSHA256(user.UserPassword);

            using (SqlConnection cn = new(sqlString.GetCadenaSQL()))
            {
                cn.Open();
                var cmd = new SqlCommand("SP_Login", cn);
                cmd.Parameters.AddWithValue("Email", user.Email);
                cmd.Parameters.AddWithValue("UserPassword", user.UserPassword);
                cmd.CommandType = CommandType.StoredProcedure;

                user.UserId = Convert.ToInt32(cmd.ExecuteScalar());

                res = user.UserId;

                return await Task.FromResult(res);

            }

        }

    }
}




