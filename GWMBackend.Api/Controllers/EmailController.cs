using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Vml.Office;
using GWMBackend.Core.Model.Base;
using GWMBackend.Domain.Models;
using GWMBackend.Service.Base;
using Microsoft.AspNetCore.Authorization;
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

                        DisplayName = res.Name,
                        Mobile = "",
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
        [HttpGet("GetData")]
        public IActionResult GetData()
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

                var userId = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;

                var res = _service.email.GetById(Convert.ToInt32(userId));

                if (res != null)
                {
                    return Ok(new
                    {
                        TimeStamp = DateTime.Now,
                        ResponseCode = HttpStatusCode.OK,
                        Message = "Your info",
                        Data = new { res },
                        Error = new { }
                    });
                }

                return Ok(new
                {
                    TimeStamp = DateTime.Now,
                    ResponseCode = HttpStatusCode.BadRequest,
                    Message = "Failed",
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
