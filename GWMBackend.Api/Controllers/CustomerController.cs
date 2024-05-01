using GWMBackend.Core.Model.Base;
using GWMBackend.Service.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using static GWMBackend.Domain.DTOs.CustomerDTO;

namespace GWMBackend.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        IServiceWrapper _service;
        private readonly AppSettings _appSettings;
        private static Random random = new Random();

        public CustomerController(IServiceWrapper service, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _service = service;
        }

        [HttpGet("BOGetAllNewCustomers")]
        public IActionResult BOGetAllNewCustomers()
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


                var res = _service.customer.BOGetAllNewCustomers();

                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.OK,
                    Message = "New customer list send successfully",
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
        [HttpGet("BOGetAllRegisteredCustomers")]
        public IActionResult BOGetAllRegisteredCustomers()
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


                var res = _service.customer.BOGetAllRegisteredCustomers();

                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.OK,
                    Message = "Registered customer list send successfully",
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
        [HttpPost("AddCustomer")]
        public IActionResult AddCustomer([FromBody] AddCustomer addCustomer)
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

                if (string.IsNullOrEmpty(addCustomer.FirstName))
                {
                    return BadRequest(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "FirstName is required",
                        Data = new { },
                        Error = new { }
                    });
                }

                if (string.IsNullOrEmpty(addCustomer.LastName))
                {
                    return BadRequest(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "LastName is required",
                        Data = new { },
                        Error = new { }
                    });
                }

                if (string.IsNullOrEmpty(addCustomer.PickupDate))
                {
                    return BadRequest(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "FirstName is required",
                        Data = new { },
                        Error = new { }
                    });
                }

                if (string.IsNullOrEmpty(addCustomer.PickupDate))
                {
                    return BadRequest(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "FirstName is required",
                        Data = new { },
                        Error = new { }
                    });
                }

                if (string.IsNullOrEmpty(addCustomer.PickupDate))
                {
                    return BadRequest(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "FirstName is required",
                        Data = new { },
                        Error = new { }
                    });
                }

                if (string.IsNullOrEmpty(addCustomer.PickupDate))
                {
                    return BadRequest(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "FirstName is required",
                        Data = new { },
                        Error = new { }
                    });
                }

                if (string.IsNullOrEmpty(addCustomer.PickupDate))
                {
                    return BadRequest(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "FirstName is required",
                        Data = new { },
                        Error = new { }
                    });
                }



                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.OK,
                    Message = "New customer had added successfully",
                    Data = new { },
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
