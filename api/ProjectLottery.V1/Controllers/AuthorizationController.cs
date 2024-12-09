using ProjectLottery.V1.Domain.DTOs.Auth;
using ProjectLottery.V1.Domain.DTOs.Common;
using ProjectLottery.V1.Domain.Enums;
using ProjectLottery.V1.Helpers.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Threading.Tasks;
using ProjectLottery.V1.Domain.Services.Abstractions.Security;

namespace ProjectLottery.V1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthorizeServices _authorizeServices;
        private readonly IUserServices _userServices;

        public AuthorizationController(
            IAuthorizeServices AuthorizeServices, IUserServices userServices)
        {
            _authorizeServices = AuthorizeServices ?? throw new ArgumentNullException(nameof(AuthorizeServices));
            _userServices = userServices ?? throw new ArgumentNullException(nameof(userServices)); ;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> Authenticate(Login usersdata)
        {
            usersdata.Password = UtilsSegurity.EncodePassword(usersdata.Password);

            var token = (await _authorizeServices.Authenticate(usersdata));

            if (token.Token == null)
            {
                var notFount = new ResultToken()
                {
                    messageType = nameof(MessageTypeResultEnum.Info),
                    error = true,
                    messages = new string[] { "Usuario no encontrado." },
                    result = null
                };

                return Unauthorized(notFount);
            }

            var result = new ResultToken()
            {
                messageType = nameof(MessageTypeResultEnum.Success),
                error = false,
                messages = new string[] { "Inicio de sesión realizado con éxito." },
                result = token.Token
            };

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("decodificate")]
        public async Task<IActionResult> Authenticate(string userName)
        {
            var userdata = await _userServices.GetByName(userName);

            var password = UtilsSegurity.DecodePassword(userdata.result.Password);

            return Ok(password);
        }
    }
}