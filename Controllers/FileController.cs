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


        public FileController(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;

        }

        //public async Task<IActionResult> getS()
        //{
        //    try
        //    {
        //        var 
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        [HttpPost]
        public async Task<ActionResult<Files>> FileUpload([FromForm] Files file)
        {
            try
            {
                var response = await _fileRepository.UploadFile(file);


                if (response)
                {
                    return Ok("Se ha cargado el archivo correctamente");
                }
                else
                {
                    return BadRequest("El nombre del archivo ya existe");
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
        }
    }
}
