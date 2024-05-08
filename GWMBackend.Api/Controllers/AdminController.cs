using DocumentFormat.OpenXml.Presentation;
using GWMBackend.Core.Model.Base;
using GWMBackend.Domain.Models;
using GWMBackend.Service.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using System.Security.Claims;
using static GWMBackend.Domain.DTOs.CustomerDTO;
using static GWMBackend.Domain.DTOs.ProductDTO;

namespace GWMBackend.Api.Controllers
{
    //[Authorize]
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
        [HttpGet("Order/BOGetAllOrders")]
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
        [HttpGet("Customer/BOGetAllNewCustomers")]
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
        [HttpGet("Customer/BOGetAllRegisteredCustomers")]
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
        [HttpPost("Customer/AddCustomer")]
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
                        Message = "First name is required",
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
                        Message = "Last name is required",
                        Data = new { },
                        Error = new { }
                    });
                }

                if (string.IsNullOrEmpty(addCustomer.PhoneNumber))
                {
                    return BadRequest(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Phone number is required",
                        Data = new { },
                        Error = new { }
                    });
                }

                if (addCustomer.PhoneNumber.Length != 11)
                {
                    return BadRequest(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Phone number is not valid",
                        Data = new { },
                        Error = new { }
                    });
                }

                if (string.IsNullOrEmpty(addCustomer.RestaurantName))
                {
                    return BadRequest(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Restaurant name is required",
                        Data = new { },
                        Error = new { }
                    });
                }

                if (string.IsNullOrEmpty(addCustomer.ZipCode))
                {
                    return BadRequest(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "ZipCode is required",
                        Data = new { },
                        Error = new { }
                    });
                }

                if (string.IsNullOrEmpty(addCustomer.Address))
                {
                    return BadRequest(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Address is required",
                        Data = new { },
                        Error = new { }
                    });
                }


                var customer = new Customer()
                {
                    FirstName = addCustomer.FirstName,
                    LastName = addCustomer.LastName,
                    PhoneNumber = addCustomer.PhoneNumber,
                    Email = addCustomer.Email,
                    RestaurantName = addCustomer.RestaurantName,
                    ZipCode = addCustomer.ZipCode,
                    Address = addCustomer.Address
                };

                _service.customer.Add(customer);

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
        [HttpPost("Customer/EditCustomer")]
        public IActionResult EditCustomer([FromBody] EditCustomer editCustomer)
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

                if (editCustomer.Id == 0)
                {
                    return BadRequest(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Customer ID is required",
                        Data = new { },
                        Error = new { }
                    });
                }

                var customer = _service.customer.GetById(editCustomer.Id);

                if (string.IsNullOrEmpty(editCustomer.FirstName))
                {
                    return BadRequest(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "First name is required",
                        Data = new { },
                        Error = new { }
                    });
                }

                if (string.IsNullOrEmpty(editCustomer.LastName))
                {
                    return BadRequest(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Last name is required",
                        Data = new { },
                        Error = new { }
                    });
                }

                if (string.IsNullOrEmpty(editCustomer.PhoneNumber))
                {
                    return BadRequest(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Phone number is required",
                        Data = new { },
                        Error = new { }
                    });
                }

                if (editCustomer.PhoneNumber.Length != 11)
                {
                    return BadRequest(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Phone number is not valid",
                        Data = new { },
                        Error = new { }
                    });
                }

                if (string.IsNullOrEmpty(editCustomer.RestaurantName))
                {
                    return BadRequest(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Restaurant name is required",
                        Data = new { },
                        Error = new { }
                    });
                }

                if (string.IsNullOrEmpty(editCustomer.ZipCode))
                {
                    return BadRequest(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "ZipCode is required",
                        Data = new { },
                        Error = new { }
                    });
                }

                if (string.IsNullOrEmpty(editCustomer.Address))
                {
                    return BadRequest(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Address is required",
                        Data = new { },
                        Error = new { }
                    });
                }


                customer.FirstName = editCustomer.FirstName;
                customer.LastName = editCustomer.LastName;
                customer.PhoneNumber = editCustomer.PhoneNumber;
                customer.Email = editCustomer.Email;
                customer.RestaurantName = editCustomer.RestaurantName;
                customer.ZipCode = editCustomer.ZipCode;
                customer.Address = editCustomer.Address;
                customer.IsActive = editCustomer.IsActive;
                customer.IsRegister = (bool)editCustomer.IsRegister;

                _service.customer.Edit(customer);

                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.OK,
                    Message = $"Customer {customer.Id} had edited successfully",
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
        [HttpGet("Product/BOGetAllProducts")]
        public IActionResult BOGetAllProducts()
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

                var res = _service.product.BOGetAll();

                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.OK,
                    Message = "Products list send succesfully!",
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
        [HttpPost("Product/AddProduct")]
        public IActionResult AddProduct([FromForm] AddProduct addProduct)
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


                if (string.IsNullOrEmpty(addProduct.Title))
                {
                    return BadRequest(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Title is required",
                        Data = new { },
                        Error = new { }
                    });
                }

                if (string.IsNullOrEmpty(addProduct.Description))
                {
                    return BadRequest(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Description is required",
                        Data = new { },
                        Error = new { }
                    });
                }

                if (addProduct.Inventory == null)
                {
                    return BadRequest(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Inventory is required",
                        Data = new { },
                        Error = new { }
                    });
                }

                if (string.IsNullOrEmpty(addProduct.Price))
                {
                    return BadRequest(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Price is required",
                        Data = new { },
                        Error = new { }
                    });
                }

                var product = new Product()
                {
                    Title = addProduct.Title,
                    Inventory = addProduct.Inventory,
                    Price = addProduct.Price,
                    Description = addProduct.Description,
                };

                if (addProduct.Photo != null)
                {
                    _service.photo.Upload(addProduct.Photo);
                    product.Photo = _appSettings.LIARA_ENDPOINT + "/" + _appSettings.LIARA_BUCKET_NAME + "/Products/" + addProduct.Photo.FileName;
                }

                _service.product.Add(product);

                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.OK,
                    Message = "New product had added successfully",
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
        [HttpPost("Product/EditProduct")]
        public IActionResult EditProduct([FromForm] EditProduct editProduct)
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

                if (editProduct.Id == 0)
                {
                    return BadRequest(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Product ID is required",
                        Data = new { },
                        Error = new { }
                    });
                }

                var product = _service.product.GetById(editProduct.Id);

                if (string.IsNullOrEmpty(editProduct.Title))
                {
                    return BadRequest(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Title is required",
                        Data = new { },
                        Error = new { }
                    });
                }

                if (string.IsNullOrEmpty(editProduct.Description))
                {
                    return BadRequest(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Description is required",
                        Data = new { },
                        Error = new { }
                    });
                }

                if (editProduct.Inventory == null)
                {
                    return BadRequest(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Inventory is required",
                        Data = new { },
                        Error = new { }
                    });
                }

                if (string.IsNullOrEmpty(editProduct.Price))
                {
                    return BadRequest(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Price is required",
                        Data = new { },
                        Error = new { }
                    });
                }

                product.Title = editProduct.Title;
                product.Inventory = (int)editProduct.Inventory;
                product.Price = editProduct.Price;  
                product.Description = editProduct.Description;
                product.IsActive = editProduct.IsActive;

                if(editProduct.Photo != null)
                {
                    if (!string.IsNullOrEmpty(product.Photo))
                    {
                        _service.photo.Delete(product.Photo);
                    }
                    _service.photo.Upload(editProduct.Photo);
                    product.Photo = _appSettings.LIARA_ENDPOINT + "/" + _appSettings.LIARA_BUCKET_NAME + "/Products/" + editProduct.Photo.FileName;
                }

                _service.product.Edit(product);

                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.OK,
                    Message = $"Product {product.Id} had edited successfully",
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
