using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Vml.Office;
using GWMBackend.Core.Model.Base;
using GWMBackend.Domain.Models;
using GWMBackend.Service.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Security.Claims;
using static GWMBackend.Domain.DTOs.EmailDTO;

namespace GWMBackend.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : Controller
    {
        IServiceWrapper _service;
        private readonly AppSettings _appSettings;
        private static Random random = new Random();

        public EmailController(IServiceWrapper service, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _service = service;
        }
        [AllowAnonymous]
        [HttpGet("VerifyEmail")]
        public IActionResult VerifyEmail([FromHeader] string verifyEmail)
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

                if (string.IsNullOrEmpty(verifyEmail))
                {
                    return BadRequest(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Please enter your email address.",
                        Data = new { },
                        Error = new { ErrorMsg = ModelState }
                    });
                }

                var res = _service.email.verifyEmail(verifyEmail.ToLower());

                if (res != null)
                {
                    const string chars = "0123456789";
                    string code = new string(Enumerable.Repeat(chars, 5)
                      .Select(s => s[random.Next(s.Length)]).ToArray());

                    res.VerificationCode = code;
                    _service.email.Edit(res);

                    _service.emailSmtpService.SendEmail(res, code);
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.OK,
                        Message = "Your verification code was sent successfully!",
                        Data = new { res.Id },
                        Error = new { }
                    });
                }

                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.BadRequest,
                    Message = "The Email is not valid",
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
        [AllowAnonymous]
        [HttpPost("VerifyCode")]
        public IActionResult VerifyCode([FromBody] VerifyCode verifyCode)
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

                if (verifyCode.Id == 0 || verifyCode.Id == null)
                {
                    return BadRequest(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Please enter customer id.",
                        Data = new { },
                        Error = new { ErrorMsg = ModelState }
                    });
                }

                if (string.IsNullOrEmpty(verifyCode.Code))
                {
                    return BadRequest(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.BadRequest,
                        Message = "Please enter the code we send you.",
                        Data = new { },
                        Error = new { ErrorMsg = ModelState }
                    });
                }

                var res = _service.email.verifyCode(verifyCode.Id, verifyCode.Code);

                if (res != null)
                {
                    if (res.IsActive == false)
                    {
                        return Ok(new
                        {
                            TimeStamp = DateTime.Now,
                            ResponseCode = HttpStatusCode.MethodNotAllowed,
                            Message = "This customer is not active.",
                            Value = new { },
                            Error = new { }
                        });
                    }
                    var token = _service.email.GenToken(res);
                    var refreshToken = "";
                    //if (_singIn.RememberMe)
                    //{
                    //    Random random = new Random();
                    //    refreshToken = new string(Enumerable.Repeat("abcdefghijklmnopqrstuvwxyz0123456789", 50).Select(s => s[random.Next(s.Length)]).ToArray());
                    //}
                    res.RefreshToken = refreshToken;
                    res.VerificationCode = "";
                    _service.email.Edit(res);

                    CLoginResponse login = new CLoginResponse
                    {

                        DisplayName = res.FirstName + " " + res.LastName,
                        Mobile = res.PhoneNumber,
                        Token = token.AccessToken,
                        RefreshToken = refreshToken,
                        CreationDateTime = res.JoinDate
                    };
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.OK,
                        Message = "You have login successfully.",
                        Value = new { Response = login },
                        Error = new { }
                    });
                }

                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.BadRequest,
                    Message = ("Verification code not valid."),
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
        //[AllowAnonymous]
        //[HttpPost("Login")]
        //public IActionResult Login([FromBody] LoginRequest _singIn)
        //{
        //    try
        //    {
        //        //----------------------------------------------------------------------------------Check parameters
        //        if (string.IsNullOrEmpty(_singIn.Username))
        //        {
        //            return Ok(new
        //            {
        //                TimeStamp = DateTime.Now,
        //                ResponseCode = HttpStatusCode.BadRequest,
        //                Message = "نام کاربری یا ایمیل الزامی است",
        //                Value = new { },
        //                Error = new { }
        //            });
        //        }
        //        if (string.IsNullOrEmpty(_singIn.Password))
        //        {
        //            return Ok(new
        //            {
        //                TimeStamp = DateTime.Now,
        //                ResponseCode = HttpStatusCode.BadRequest,
        //                Message = "کلمه عبور الزامی است",
        //                Value = new { },
        //                Error = new { }
        //            });
        //        }
        //        //----------------------------------------------------------------------------------Check parameters

        //        //----------------------------------------------------------------------------------Find User                


        //        User user = _service.User.LoginUser(_singIn.Username, _singIn.Password);
        //        if (user == null)
        //        {
        //            return Ok(new
        //            {
        //                TimeStamp = DateTime.Now,
        //                ResponseCode = HttpStatusCode.NotFound,
        //                Message = "نام کاربری یا کلمه عبور نادرست است.",
        //                Value = new { },
        //                Error = new { }
        //            });
        //        }
        //        if (user.IsActive == false)
        //        {
        //            return Ok(new
        //            {
        //                TimeStamp = DateTime.Now,
        //                ResponseCode = HttpStatusCode.MethodNotAllowed,
        //                Message = "کاربر مورد نظر غیرفعال است.",
        //                Value = new { },
        //                Error = new { }
        //            });
        //        }
        //        var token = _service.User.GenToken(user);
        //        var refreshToken = "";
        //        if (_singIn.RememberMe)
        //        {
        //            Random random = new Random();
        //            refreshToken = new string(Enumerable.Repeat("abcdefghijklmnopqrstuvwxyz0123456789", 50).Select(s => s[random.Next(s.Length)]).ToArray());
        //        }
        //        user.RememberMe = _singIn.RememberMe;
        //        user.RefreshToken = refreshToken;
        //        _service.User.EditUser(user);

        //        LoginResponse login = new LoginResponse
        //        {
        //            BirthDate = user.BirthDate,
        //            DisplayName = user.DisplayName,
        //            Email = user.Email,
        //            FirstName = user.FirstName,
        //            Image = user.Image,
        //            ImageThumb = user.ImageThumb,
        //            LastName = user.LastName,
        //            Mobile = user.Mobile,
        //            NationalCode = user.NationalCode,
        //            Phone = user.Phone,
        //            RoleId = user.RoleId,
        //            Token = token.AccessToken,
        //            RefreshToken = refreshToken,
        //            UserName = user.UserName
        //        };
        //        return Ok(new
        //        {
        //            TimeStamp = DateTime.Now,
        //            ResponseCode = HttpStatusCode.OK,
        //            Message = "ورود با موفقیت انجام شد.",
        //            Value = new { Response = login },
        //            Error = new { }
        //        });
        //        //----------------------------------------------------------------------------------Find User
        //    }
        //    catch (Exception ex)
        //    {
        //        return Ok(new { TimeStamp = DateTime.Now, ResponseCode = HttpStatusCode.InternalServerError, Message = "خطای داخلی سرور رخ داده است", Value = new { }, Error = new { Response = ex.ToString() } });
        //    }

        //}

    }
}
