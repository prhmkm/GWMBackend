using DocumentFormat.OpenXml.Presentation;
using GWMBackend.Api.Utilities;
using GWMBackend.Core.Model.Base;
using GWMBackend.Domain.Models;
using GWMBackend.Service.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Nancy.Diagnostics;
using System.Net;
using System.Security.Claims;
using static GWMBackend.Domain.DTOs.CustomerDTO;
using static GWMBackend.Domain.DTOs.EmailDTO;
using static GWMBackend.Domain.DTOs.ProductDTO;
using static GWMBackend.Domain.DTOs.UserDTO;

namespace GWMBackend.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : Controller
    {
        IServiceWrapper _service;
        GWM_DBContext _repositoryContext;
        private readonly AppSettings _appSettings;
        private static Random random = new Random();

        public AdminController(IServiceWrapper service, IOptions<AppSettings> appSettings, GWM_DBContext repositoryContext)
        {
            _appSettings = appSettings.Value;
            _service = service;
            _repositoryContext = repositoryContext;
        }
        [MyAthurizeFilter]
        [HttpGet("Order/BOGetAllOrders")]
        public IActionResult GetAllOrders()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Unknown error",
                        Value = new { },
                        Error = new { ErrorMsg = ModelState }
                    });
                }


                var res = _service.order.BOGetAll();

                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.OK,
                    Message = "Order list send successfully",
                    Value = new { Response = res },
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
                    Value = new { },
                    Error = new { Response = ex.ToString() }
                });
            }
        }
        [MyAthurizeFilter]
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
                        Value = new { },
                        Error = new { ErrorMsg = ModelState }
                    });
                }


                var res = _service.customer.BOGetAllNewCustomers();

                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.OK,
                    Message = "New customer list send successfully",
                    Value = new { Response = res },
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
                    Value = new { },
                    Error = new { Response = ex.ToString() }
                });
            }
        }
        [MyAthurizeFilter]
        [HttpGet("Customer/BOGetAllRegisteredCustomers")]
        public IActionResult BOGetAllRegisteredCustomers()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Unknown error",
                        Value = new { },
                        Error = new { ErrorMsg = ModelState }
                    });
                }


                var res = _service.customer.BOGetAllRegisteredCustomers();

                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.OK,
                    Message = "Registered customer list send successfully",
                    Value = new { Response = res },
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
                    Value = new { },
                    Error = new { Response = ex.ToString() }
                });
            }
        }
        [MyAthurizeFilter]
        [HttpPost("Customer/BOAddCustomer")]
        public IActionResult AddCustomer([FromBody] AddCustomer addCustomer)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Unknown error",
                        Value = new { },
                        Error = new { ErrorMsg = ModelState }
                    });
                }

                if (string.IsNullOrEmpty(addCustomer.FirstName))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "First name is required",
                        Value = new { },
                        Error = new { }
                    });
                }

                if (string.IsNullOrEmpty(addCustomer.LastName))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Last name is required",
                        Value = new { },
                        Error = new { }
                    });
                }

                if (string.IsNullOrEmpty(addCustomer.PhoneNumber))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Phone number is required",
                        Value = new { },
                        Error = new { }
                    });
                }

                if (addCustomer.PhoneNumber.Length != 11)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Phone number is not valid",
                        Value = new { },
                        Error = new { }
                    });
                }

                if (string.IsNullOrEmpty(addCustomer.RestaurantName))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Restaurant name is required",
                        Value = new { },
                        Error = new { }
                    });
                }

                if (string.IsNullOrEmpty(addCustomer.ZipCode))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "ZipCode is required",
                        Value = new { },
                        Error = new { }
                    });
                }

                if (string.IsNullOrEmpty(addCustomer.Address))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Address is required",
                        Value = new { },
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
                    Value = new { },
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
                    Value = new { },
                    Error = new { Response = ex.ToString() }
                });
            }
        }
        [MyAthurizeFilter]
        [HttpPost("Customer/BOEditCustomer")]
        public IActionResult EditCustomer([FromBody] EditCustomer editCustomer)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Unknown error",
                        Value = new { },
                        Error = new { ErrorMsg = ModelState }
                    });
                }

                if (editCustomer.Id == 0)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Customer ID is required",
                        Value = new { },
                        Error = new { }
                    });
                }

                var customer = _service.customer.GetById(editCustomer.Id);

                if (string.IsNullOrEmpty(editCustomer.FirstName))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "First name is required",
                        Value = new { },
                        Error = new { }
                    });
                }

                if (string.IsNullOrEmpty(editCustomer.LastName))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Last name is required",
                        Value = new { },
                        Error = new { }
                    });
                }

                if (string.IsNullOrEmpty(editCustomer.PhoneNumber))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Phone number is required",
                        Value = new { },
                        Error = new { }
                    });
                }

                if (editCustomer.PhoneNumber.Length != 11)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Phone number is not valid",
                        Value = new { },
                        Error = new { }
                    });
                }

                if (string.IsNullOrEmpty(editCustomer.RestaurantName))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Restaurant name is required",
                        Value = new { },
                        Error = new { }
                    });
                }

                if (string.IsNullOrEmpty(editCustomer.ZipCode))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "ZipCode is required",
                        Value = new { },
                        Error = new { }
                    });
                }

                if (string.IsNullOrEmpty(editCustomer.Address))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Address is required",
                        Value = new { },
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
                    Value = new { },
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
                    Value = new { },
                    Error = new { Response = ex.ToString() }
                });
            }
        }
        [MyAthurizeFilter]
        [HttpGet("Product/BOGetAllProducts")]
        public IActionResult BOGetAllProducts()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Unknown error",
                        Value = new { },
                        Error = new { ErrorMsg = ModelState }
                    });
                }

                var res = _service.product.BOGetAll();

                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.OK,
                    Message = "Products list send succesfully!",
                    Value = new { Response = res },
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
                    Value = new { },
                    Error = new { Response = ex.ToString() }
                });
            }



        }
        [MyAthurizeFilter]
        [HttpPost("Product/AddProduct")]
        public IActionResult AddProduct([FromBody] AddProduct addProduct)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Unknown error",
                        Value = new { },
                        Error = new { ErrorMsg = ModelState }
                    });
                }


                if (string.IsNullOrEmpty(addProduct.Title))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Title is required",
                        Value = new { },
                        Error = new { }
                    });
                }

                if (string.IsNullOrEmpty(addProduct.Description))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Description is required",
                        Value = new { },
                        Error = new { }
                    });
                }

                if (addProduct.Inventory == null)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Inventory is required",
                        Value = new { },
                        Error = new { }
                    });
                }

                if (string.IsNullOrEmpty(addProduct.Price))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Price is required",
                        Value = new { },
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
                    var photo = _service.photo.Upload(addProduct.PhotoName + "-" + DateTime.Now.ToString("MMddHHmmss"), addProduct.Photo, false, 1) ;
                    product.Photo = photo.Address;
                }

                _service.product.Add(product);

                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.OK,
                    Message = "New product had added successfully",
                    Value = new { },
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
                    Value = new { },
                    Error = new { Response = ex.ToString() }
                });
            }
        }
        [MyAthurizeFilter]
        [HttpPost("Product/EditProduct")]
        public IActionResult EditProduct([FromBody] EditProduct editProduct)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Unknown error",
                        Value = new { },
                        Error = new { ErrorMsg = ModelState }
                    });
                }

                if (editProduct.Id == 0)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Product ID is required",
                        Value = new { },
                        Error = new { }
                    });
                }

                var product = _service.product.GetById(editProduct.Id);

                if (string.IsNullOrEmpty(editProduct.Title))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Title is required",
                        Value = new { },
                        Error = new { }
                    });
                }

                if (string.IsNullOrEmpty(editProduct.Description))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Description is required",
                        Value = new { },
                        Error = new { }
                    });
                }

                if (editProduct.Inventory == null)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Inventory is required",
                        Value = new { },
                        Error = new { }
                    });
                }

                if (string.IsNullOrEmpty(editProduct.Price))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Price is required",
                        Value = new { },
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
                        var res = _service.photo.GetByAddress(product.Photo);
                        if (res != null)
                        {
                            var imageName = res.Address.Split("/")[res.Thumbnail.Split("/").Count() - 1];
                            if (System.IO.File.Exists(_appSettings.SaveImagePath + "\\Products\\" + imageName))
                                System.IO.File.Delete(_appSettings.SaveImagePath + "\\Products\\" + imageName);
                            _service.photo.DeleteById(res.Id);
                        }
                    }
                    var photo = _service.photo.Upload(editProduct.PhotoName + "-" + DateTime.Now.ToString("MMddHHmmss"), editProduct.Photo, false, 1);
                    product.Photo = photo.Address;
                }

                _service.product.Edit(product);

                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.OK,
                    Message = $"Product {product.Id} had edited successfully",
                    Value = new { },
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
                    Value = new { },
                    Error = new { Response = ex.ToString() }
                });
            }
        }
        [AllowAnonymous]
        [HttpPost("auth/Login")]
        public IActionResult Login([FromBody] Domain.DTOs.UserDTO.LoginRequest _singIn)
        {
            try
            {
                //----------------------------------------------------------------------------------Check parameters
                if (string.IsNullOrEmpty(_singIn.Username))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Username or email is required",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (string.IsNullOrEmpty(_singIn.Password))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Password is required",
                        Value = new { },
                        Error = new { }
                    });
                }
                //----------------------------------------------------------------------------------Check parameters

                //----------------------------------------------------------------------------------Find user                


                User user = _service.user.LoginUser(_singIn.Username, _singIn.Password);

                if (user == null)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.NotFound,
                        Message = "The username or password is incorrect.",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (user.IsActive == false)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "The desired user is disabled.",
                        Value = new { },
                        Error = new { }
                    });
                }

                if (user.RoleId != 0)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "The desired user is not admin.",
                        Value = new { },
                        Error = new { }
                    });
                }

                var token = _service.user.GenToken(user);

                var refreshToken = "";
                if (_singIn.RememberMe)
                {
                    Random random = new Random();
                    refreshToken = new string(Enumerable.Repeat("abcdefghijklmnopqrstuvwxyz0123456789", 50).Select(s => s[random.Next(s.Length)]).ToArray());
                }
                user.RememberMe = _singIn.RememberMe;
                user.RefreshToken = refreshToken;
                _service.user.EditUser(user);

                LoginResponse data = new LoginResponse
                {
                    Id = user.Id,
                    CreationDateTime = user.JoinDate,
                    DisplayName = user.FirstName + " " + user.LastName,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    MobileNumber = user.MobileNumber,
                    RoleId = (int)user.RoleId,
                    RoleTitle = _repositoryContext.Roles.FirstOrDefault(s => s.Id == user.RoleId).Title,
                    Token = token.AccessToken,
                    RefreshToken = refreshToken,
                    UserName = user.UserName
                };
                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.OK,
                    Message = "Login was successful.",
                    Value = new { data },
                    Error = new { }
                });
                //----------------------------------------------------------------------------------Find user
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.InternalServerError,
                    Message = "An internal server error has occurred",
                    Value = new { Response = ex.ToString() },
                    Error = new { }
                });
            }

        }
        [AllowAnonymous]
        [HttpPost("RefreshToken")]
        public IActionResult RefreshToken([FromBody] RefreshTokenRequest _refreshTokenRequest)
        {
            try
            {
                //----------------------------------------------------------------------------------Check parameters
                if (_refreshTokenRequest is null)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "The received data is not valid",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (string.IsNullOrEmpty(_refreshTokenRequest.RefreshToken))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Refresh Token amount is required",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (string.IsNullOrEmpty(_refreshTokenRequest.Username))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Username is required",
                        Value = new { },
                        Error = new { }
                    });
                }
                //----------------------------------------------------------------------------------Check parameters
                //----------------------------------------------------------------------------------Check Customer Exist
                User user = _service.user.GetUserByUsername(_refreshTokenRequest.Username);

                if (user == null)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.NotFound,
                        Message = "The username or password is incorrect.",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (user.IsActive == false)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "The desired user is disabled.",
                        Value = new { },
                        Error = new { }
                    });
                }
                if (user.RefreshToken != _refreshTokenRequest.RefreshToken || (user.RememberMe == false))
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Refresh token is invalid.",
                        Value = new { },
                        Error = new { }
                    });
                }
                var token = _service.user.GenToken(user);
                var refreshToken = "";
                Random random = new Random();
                refreshToken = new string(Enumerable.Repeat("abcdefghijklmnopqrstuvwxyz0123456789", 50).Select(s => s[random.Next(s.Length)]).ToArray());
                user.RefreshToken = refreshToken;
                _service.user.EditUser(user);

                LoginResponse data = new LoginResponse
                {
                    DisplayName = user.FirstName + " " + user.LastName,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    MobileNumber = user.MobileNumber,
                    RoleId = (int)user.RoleId,
                    Token = token.AccessToken,
                    RefreshToken = refreshToken,
                    UserName = user.UserName
                };
                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.OK,
                    Message = "The token was successfully retrieved.",
                    Value = new { Response = data },
                    Error = new { }
                });
                //----------------------------------------------------------------------------------Check Customer Exist
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.InternalServerError,
                    Message = "An internal server error has occurred",
                    Value = new { Response = ex.ToString() },
                    Error = new { }
                });
            }
        }
    }
}
