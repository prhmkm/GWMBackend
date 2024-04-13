using GWMBackend.Core.Model.Base;
using GWMBackend.Service.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using System.Security.Claims;

namespace GWMBackend.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        IServiceWrapper _service;
        private readonly AppSettings _appSettings;
        private static Random random = new Random();

        public AdminController(IServiceWrapper service, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _service = service;
        }
        [HttpGet("BOGetAllOrders")]
        public IActionResult GetAllOrders()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Unknown error",
                        Data = new { },
                        Error = new { ErrorMsg = ModelState }
                    });
                }


                var res = _service.order.BOGetAll();

                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.OK,
                    Message = "Order list send successfully",
                    Data = new { res },
                    Error = new { }
                });


            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.InternalServerError,
                    Message = "An internal server error has occurred",
                    Data = new { },
                    Error = new { Response = ex.ToString() }
                });
            }
        }
    }
}
