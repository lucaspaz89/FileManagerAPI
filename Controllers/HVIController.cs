using FileManagerAPI.Models;
using FileManagerAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HVIController : ControllerBase
    {
        private readonly IHviRepository _hviRepository;

        public HVIController(IHviRepository hviRepository)
        {
            _hviRepository = hviRepository;
        }

        [HttpPost]
        public async Task<ActionResult<HVI>> TestingFileUploadToDB(ListHVI model)
        {
            try
            {
                var response = await _hviRepository.UploadHVI(model.HVIList);
                return Ok(response);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message.ToString());
            }
        }
    }
}
