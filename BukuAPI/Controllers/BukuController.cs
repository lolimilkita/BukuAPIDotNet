using BukuAPI.Services.BukuService;
using BukuAPI.Services.LogService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BukuAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BukuController : ControllerBase
    {
        private readonly IBukuService _bukuService;
        private readonly Logger _logger;

        public BukuController(Logger logger, IBukuService bukuService)
        {
            _logger = logger;
            _bukuService = bukuService;
        }

        [HttpGet]
        public async Task<ActionResult<List<object>>> GetAllBuku(string? search)
        {
            var clientIp = HttpContext.Connection.RemoteIpAddress;
            var result = _bukuService.GetAllBukus(search);
            await _logger.Log(clientIp, req: Request, res: Ok(result).StatusCode.ToString(), logMessage: "GetAllBuku is Successfully");
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<object>>> GetSingleBuku(int id)
        {
            var clientIp = HttpContext.Connection.RemoteIpAddress;
            var result = _bukuService.GetSingleBuku(id);
            if (result is null)
            {
                await _logger.Log(clientIp, req: Request, res: NotFound().StatusCode.ToString(), logMessage: "GetSingleBuku Gagal karna buku tidak ditemukan");
                return NotFound("Buku not Found");
            }

            await _logger.Log(clientIp, req: Request, res: Ok(result).StatusCode.ToString(), logMessage: "GetSingleBuku is Successfully");
            return Ok(result);
        }

        [HttpPost]
        public  async Task<ActionResult<List<object>>> AddBuku([FromBody] Buku newBuku)
        {
            var clientIp = HttpContext.Connection.RemoteIpAddress;
            var result = _bukuService.AddBuku(newBuku);
            if (result is null)
            {
                await _logger.Log(clientIp, req: Request, res: NotFound().StatusCode.ToString(), logMessage: "AddBuku Gagal, Coba lagi");
                return NotFound("Buku not Found");
            }
            await _logger.Log(clientIp, req: Request, res: Ok(result).StatusCode.ToString(), logMessage: "AddBuku is Successfully");
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<object>>> UpdateBuku(int id, [FromBody] Buku editBuku)
        {
            var clientIp = HttpContext.Connection.RemoteIpAddress;
            var result = _bukuService.UpdateBuku(id, editBuku);
            if(result is null) 
            { 
                await _logger.Log(clientIp, req: Request, res: NotFound().StatusCode.ToString(), logMessage: "UpdateBuku Gagal karna buku tidak ditemukan"); 
                return NotFound("Buku not Found"); 
            }
            
            await _logger.Log(clientIp, req: Request, res: Ok(result).StatusCode.ToString(), logMessage: "UpddateBuku is Successfully");
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<object>>> DeleteBuku(int id)
        {
            var clientIp = HttpContext.Connection.RemoteIpAddress;
            var result = _bukuService.DeleteBuku(id);
            if (result is null)
            {
                await _logger.Log(clientIp, req: Request, res: NotFound().StatusCode.ToString(), logMessage: "DeleteBuku Gagal karna buku tidak ditemukan");
                return NotFound("Buku not Found");
            }

            await _logger.Log(clientIp, req: Request, res: Ok(result).StatusCode.ToString(), logMessage: "DeleteBuku is Successfully");
            return Ok(result);
        }
    }
}
