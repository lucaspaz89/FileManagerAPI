using AutoMapper;
using FileManagerAPI.DTOs;
using FileManagerAPI.Models;
using FileManagerAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileRepository _fileRepository;
        private readonly IMapper _mapper;

        public FileController(IFileRepository fileRepository, IMapper mapper)
        {
            _fileRepository = fileRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<string>> FileUpload([FromForm] Files file)
        {
            try
            {
                var response = await _fileRepository.UploadFile(file);

                //ACA SE MAPEA DE DESTINO A FUENTE, CONTRARIO AL ARCHIVO FileMap
                 //var fileDTO =  _mapper.Map<FileDTO>(response);

                return Ok(response);          
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
        }
    }
}
