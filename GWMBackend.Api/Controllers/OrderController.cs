using GWMBackend.Core.Model.Base;
using GWMBackend.Domain.Models;
using GWMBackend.Service.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using System.Security.Claims;
using static GWMBackend.Domain.DTOs.OrderDTO;

namespace GWMBackend.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        IServiceWrapper _service;
        private readonly AppSettings _appSettings;

        public OrderController(IServiceWrapper service, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _service = service;
        }

        [HttpPost("AddOrder")]
        public IActionResult AddOrder([FromBody] AddOrder order)
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
                if (order.PickupDate == null)
                {
                    return BadRequest(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Pickup date is not valid",
                        Data = new { },
                        Error = new { }
                    });
                }

                if (string.IsNullOrEmpty(order.BucketAmont))
                {
                    return BadRequest(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "BucketAmont is not valid",
                        Data = new { },
                        Error = new { }
                    });
                }


                if (order.Products.Count > 0)
                {
                    foreach (var item in order.Products)
                    {
                        if (item.Id == 0 || item.Quantity == 0)
                        {
                            return BadRequest(new
                            {
                                TimeStamp = DateTime.Now,
                                ResponseCode = HttpStatusCode.BadRequest,
                                Message = "Product detail is not valid",
                                Data = new { },
                                Error = new { }
                            });
                        }
                    }
                }

                    var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

                    var res = new Domain.Models.Order()
                    {
                        CustomerId = Convert.ToInt32(userId),
                        BucketAmont = order.BucketAmont,
                        PickupDate = order.PickupDate,
                    };

                    var orderId = _service.order.AddOrder(res);
                    var shopItem = new ShopItem();

                    foreach (var item in order.Products)
                    {
                        shopItem = new ShopItem();
                        shopItem.OrderId = orderId;
                        shopItem.ProductId = item.Id;
                        shopItem.Amont = item.Quantity;
                        _service.shopItem.Add(shopItem);
                    }

                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.OK,
                        Message = "Order has been submitted succesfully!",
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
