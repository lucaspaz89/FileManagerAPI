using FileManagerAPI.Helpers;
using FileManagerAPI.Models;
using FileManagerAPI.Repository.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace FileManagerAPI.Repository
{
    public class LoginRepository : ILoginRepository
    {
        //CODIGOS REUTILIZABLES, ESTAN EN LA CARPETA "HELPERS"
        SQLString sqlString = new(); //INSTANCIA PARA ACCEDER A LA CADENA DE CONEXION
        Encrypt encrypt = new(); //INSTANCIA PARA ENCRIPTAR LA CONTRASEÑA        
        JWTConfiguration jwtString = new();  //INSTANCIA PARA APLICAR EL JWT

        public async Task<string> Login(UserLogin user)
        {
            try
            {
                string res;

                user.UserPassword = encrypt.ConvertirSHA256(user.UserPassword);

                using (SqlConnection cn = new(sqlString.GetCadenaSQL()))
                {
                    cn.Open();
                    var cmd = new SqlCommand("SP_Login", cn);
                    cmd.Parameters.AddWithValue("Email", user.Email);
                    cmd.Parameters.AddWithValue("UserPassword", user.UserPassword);
                    cmd.CommandType = CommandType.StoredProcedure;

                    user.UserId = Convert.ToInt32(cmd.ExecuteScalar());

                }

                if (user.UserId != 0)
                {
                    //LLAMAMOS AL METODO QUE GENERO EL JWT Y LO ALMACENAMOS EN "res"
                    res = jwtString.token(user.UserId);
                }
                else
                {
                    res = "Error";
                }

                return await Task.FromResult(res);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message.ToString());
            }

        }

        public async Task<int> GetById(int user)
        {
            int id;
            UserLogin obUser = new();
            using (SqlConnection cn = new(sqlString.GetCadenaSQL()))
            {
                cn.Open();
                var cmd = new SqlCommand("SP_GetByID", cn);
                cmd.Parameters.AddWithValue("UserId", user);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();

                id = user;

            }

            return await Task.FromResult(id);
        }

    }
}


//res = user.UserId;

//return await Task.FromResult(res);




