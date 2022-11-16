using FileManagerAPI.Helpers;
using FileManagerAPI.Models;
using FileManagerAPI.Repository.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace FileManagerAPI.Repository
{
    public class FileRepository : IFileRepository
    {
        //CODIGOS REUTILIZABLES, ESTAN EN LA CARPETA "HELPERS"
        SQLString sqlString = new(); //INSTANCIA PARA ACCEDER A LA CADENA DE CONEXION


        private readonly string _fileRoute; //INSTANCIA PARA ACCEDER A LA RUTA DONDE SE GUARADARA EL ARCHIVO
        public FileRepository(IConfiguration config)
        {
            _fileRoute = config.GetSection("Settings").GetSection("fileRoute").Value;
        }


        public async Task<bool> UploadFile(Files file)
        {
            bool fileNameExists;
            string fileRoute = Path.Combine(_fileRoute, file.Archivo.FileName);//CREAMOS EL NOMBRE/RUTA ARCHIVO
            string fileName = file.Archivo.FileName;//ALMACENAMOS EL NOMBRE DEL ARCHIVO EN UNA VARIABLE
            using FileStream newFile = File.Create(fileRoute);//CREAMOS UN NUEVO ARCHIVO CON EL NOMBRE/RUTA CREADOS ANTERIORMENTE; 
                                                              //EN ESTE PUNTO EL ARCHIVO ESTA EN BLANCO, SOLO CONTIENE UN NOMBRE/RUTA
            file.Archivo.CopyTo(newFile);//COPIA EL ARCHIVO AL NOMBRE/RUTA CREADOS
            newFile.Flush();//LIMPIAMOS EL "FileStream" PARA FUTUROS USOS

            using (SqlConnection cn = new(sqlString.GetCadenaSQL()))
            {
                cn.Open();
                var cmd = new SqlCommand("SP_UploadFile", cn);
                cmd.Parameters.AddWithValue("Ruta", fileRoute);
                cmd.Parameters.AddWithValue("NombreArchivo", fileName);
                cmd.Parameters.Add("Registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
                fileNameExists = (bool)cmd.Parameters["Registrado"].Value;
            }

            if (fileNameExists)
            {
                return await Task.FromResult(fileNameExists);
            }
            else
            {
                return await Task.FromResult(fileNameExists);
            }

        }
    }
}
