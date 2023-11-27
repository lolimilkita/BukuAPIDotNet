using BukuAPI.Services.OrderService;
using BukuAPI.Services.LogService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BukuAPI.Services.BukuService;

namespace BukuAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly Logger _logger;

        public OrderController(Logger logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        public class OrderBuku 
        {
            public int BukuId { get; set; }
            public string Nama { get; set; }
            public Int16 Durasi { get; set; }
        }

        [HttpGet]
        public async Task<ActionResult<List<object>>> GetAllOrder()
        {
            var clientIp = HttpContext.Connection.RemoteIpAddress;
            var result = _orderService.GetAllOrders();
            await _logger.Log(clientIp, req: Request, res: Ok(result).StatusCode.ToString(), logMessage: "GetAllOrder is Successfully");
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<object>>> GetSingleOrder(int id)
        {
            var clientIp = HttpContext.Connection.RemoteIpAddress;
            var result = _orderService.GetSingleOrder(id);
            if (result is null)
            {
                await _logger.Log(clientIp, req: Request, res: NotFound().StatusCode.ToString(), logMessage: "GetSingleOrder Gagal karna order tidak ditemukan");
                return NotFound("Order not Found");
            }

            await _logger.Log(clientIp, req: Request, res: Ok(result).StatusCode.ToString(), logMessage: "GetSingleOrder is Successfully");
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<List<object>>> AddOrder([FromBody] OrderBuku orderBuku) 
        {
            var clientIp = HttpContext.Connection.RemoteIpAddress;

            Order newOrder = new()
            {
                BukuId = orderBuku.BukuId,
                Nama = orderBuku.Nama,
                Durasi = orderBuku.Durasi,
            };

            var result = _orderService.AddOrder(newOrder);
            if (result is null)
            {
                await _logger.Log(clientIp, req: Request, res: NotFound().StatusCode.ToString(), logMessage: "AddOrder Gagal, Coba lagi");
                return NotFound("Order Failed");
            }
            await _logger.Log(clientIp, req: Request, res: Ok(result).StatusCode.ToString(), logMessage: "AddOrder is Successfully");
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<object>>> UpdateOrder(int id, [FromBody] Order editOrder)
        {
            var clientIp = HttpContext.Connection.RemoteIpAddress;
            var result = _orderService.UpdateOrder(id, editOrder);
            if (result is null)
            {
                await _logger.Log(clientIp, req: Request, res: NotFound().StatusCode.ToString(), logMessage: "UpdateOrder Gagal karna order tidak ditemukan");
                return NotFound("Order not Found");
            }

            await _logger.Log(clientIp, req: Request, res: Ok(result).StatusCode.ToString(), logMessage: "UpddateOrder is Successfully");
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<object>>> DeleteOrder(int id)
        {
            var clientIp = HttpContext.Connection.RemoteIpAddress;
            var result = _orderService.DeleteOrder(id);
            if (result is null)
            {
                await _logger.Log(clientIp, req: Request, res: NotFound().StatusCode.ToString(), logMessage: "DeleteOrder Gagal karna order tidak ditemukan");
                return NotFound("Order not Found");
            }

            await _logger.Log(clientIp, req: Request, res: Ok(result).StatusCode.ToString(), logMessage: "DeleteOrder is Successfully");
            return Ok(result);
        }
    }
}
