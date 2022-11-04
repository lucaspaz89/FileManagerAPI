using AutoMapper;
using FileManagerAPI.DTOs;
using FileManagerAPI.Models;

namespace FileManagerAPI.AutoMapper
{
    public class FileMap : Profile
    {        
        public FileMap()
        {
            //Se mapea de la Fuente al DTO
            CreateMap<Files, FileDTO>();
        }
    }
}
