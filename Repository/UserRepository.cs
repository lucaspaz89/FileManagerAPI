using FileManagerAPI.Helpers;
using FileManagerAPI.Models;
using System.Data;
using System.Data.SqlClient;


namespace FileManagerAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        SQLString sqlString = new(); //INSTANCIA PARA ACCEDER A LA CADENA DE CONEXION
        Encrypt encrypt = new(); //INSTANCIA PARA ENCRIPTAR LA CONTRASEÑA

        public async Task<bool> CreateUser(UserRegistration user)
        {
            bool registrado;

            if (user.UserPassword == user.ConfirmPassword)
            {
                user.UserPassword = encrypt.ConvertirSHA256(user.UserPassword);
            }

            using (SqlConnection cn = new(sqlString.GetCadenaSQL()))
            {
                cn.Open();
                var cmd = new SqlCommand("SP_CreateUser", cn);
                cmd.Parameters.AddWithValue("Email", user.Email);
                cmd.Parameters.AddWithValue("UserPassword", user.UserPassword);
                cmd.Parameters.Add("MailExist", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();

                registrado = (bool)cmd.Parameters["MailExist"].Value;
            }

            if (registrado)
            {
                return await Task.FromResult(registrado);
            }
            else
            {
                return await Task.FromResult(registrado);
            }

        }

    }
}
