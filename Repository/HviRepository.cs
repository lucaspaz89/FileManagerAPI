using FileManagerAPI.Helpers;
using FileManagerAPI.Models;
using FileManagerAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace FileManagerAPI.Repository
{
    public class HviRepository : IHviRepository
    {
        //CODIGOS REUTILIZABLES, ESTAN EN LA CARPETA "HELPERS"
        SQLString sqlString = new(); //INSTANCIA PARA ACCEDER A LA CADENA DE CONEXION

        public async Task<string> UploadHVI(List<HVI> model)
        {
            try
            {

                //using (SqlConnection cn = new(sqlString.GetCadenaSQL()))
                //{
                //    cn.Open();
                //    var cmd = new SqlCommand("SP_GetData", cn);
                //cmd.Parameters.AddWithValue("UHML", model.UHML);
                //cmd.Parameters.AddWithValue("UI", model.UI);
                //cmd.Parameters.AddWithValue("STR", model.STR);
                //cmd.Parameters.AddWithValue("ELONG", model.ELONG);
                ////cmd.Parameters.AddWithValue("SFI", model.SFI);
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.ExecuteReader();

                //using (var rd = cmd.ExecuteReader())
                //{
                //    while (rd.Read())
                //    {
                //        model.UHML = (decimal)rd["UHML"];
                //        model.UI = (decimal)rd["UI"];
                //        model.STR = (decimal)rd["STR"];
                //        model.ELONG = (decimal)rd["ELONG"];
                //        model.SFI = (decimal)rd["SFI"];
                //    }
                //}

                return "se ha guardado con exito";
                //}


            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }

        }
    }
}
