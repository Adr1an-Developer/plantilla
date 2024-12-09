using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectLottery.V1.Helpers.Utils;
using ProjectLottery.V1.Domain.DTOs.Common;
using ProjectLottery.V1.Domain.DTOs.User;
using System;
using System.Threading.Tasks;
using ProjectLottery.V1.Entities.Security;
using ProjectLottery.V1.Domain.Enums;
using ProjectLottery.V1.Domain.DTOs.Auth;
using ProjectLottery.V1.Domain.Services.Abstractions.Security;

namespace ProjectLottery.V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IHttpContextAccessor _httpContextAccessor;
        private IUserServices _userServices;

        public UsersController(IUserServices UserServices, IHttpContextAccessor httpContextAccessor)
        {
            _userServices = UserServices;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAll()
        {
            var (decodeToken, validToken) = UtilsSegurity.ValidateToken(_httpContextAccessor);

            if (!validToken)
            {
                var notFound = new ResultNotFound()
                {
                    messageType = nameof(MessageTypeResultEnum.Warning),
                    error = true,
                    messages = new string[] { "Su sesión ha expirado, vuelva a realizar el ingreso." }
                };

                return Unauthorized(notFound);
            }

            var results = await _userServices.GetAll();

            return Ok(results);
        }

        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var (decodeToken, validToken) = UtilsSegurity.ValidateToken(_httpContextAccessor);

            if (!validToken)
            {
                var notFound = new ResultNotFound()
                {
                    messageType = nameof(MessageTypeResultEnum.Warning),
                    error = false,
                    messages = new string[] { "Su sesión ha expirado, vuelva a realizar el ingreso." }
                };

                return Unauthorized(notFound);
            }

            var results = await _userServices.GetbyId(id);

            return Ok(results);
        }

        //[Authorize]
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> post([FromBody] AddUser usersdata)
        {
            var (decodeToken, validToken) = UtilsSegurity.ValidateToken(_httpContextAccessor);

            if (!validToken)
            {
                var notFound = new ResultNotFound()
                {
                    messageType = nameof(MessageTypeResultEnum.Warning),
                    error = false,
                    messages = new string[] { "Su sesión ha expirado, vuelva a realizar el ingreso." }
                };

                return Unauthorized(notFound);
            }

            if (usersdata == null) return BadRequest();

            var result = await _userServices.Create(usersdata);

            return Ok(result);
        }

        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, User usersdata)
        {
            var (decodeToken, validToken) = UtilsSegurity.ValidateToken(_httpContextAccessor);

            if (!validToken)
            {
                var notFound = new ResultNotFound()
                {
                    messageType = nameof(MessageTypeResultEnum.Warning),
                    error = false,
                    messages = new string[] { "Su sesión ha expirado, vuelva a realizar el ingreso." }
                };

                return Unauthorized(notFound);
            }

            if (usersdata == null) return BadRequest();

            var result = await _userServices.Update(usersdata);

            return Ok(result);
        }

        //[Authorize]
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var (decodeToken, validToken) = UtilsSegurity.ValidateToken(_httpContextAccessor);

            if (!validToken)
            {
                var notFound = new ResultNotFound()
                {
                    messageType = nameof(MessageTypeResultEnum.Warning),
                    error = false,
                    messages = new string[] { "Su sesión ha expirado, vuelva a realizar el ingreso." }
                };

                return Unauthorized(notFound);
            }

            if (string.IsNullOrEmpty(id.ToString())) return BadRequest();

            var result = await _userServices.Delete(id);

            return Ok(result);
        }

        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassword data)
        {
            var (decodeToken, validToken) = UtilsSegurity.ValidateToken(_httpContextAccessor);

            if (!validToken)
            {
                var notFound = new ResultNotFound()
                {
                    messageType = nameof(MessageTypeResultEnum.Warning),
                    error = false,
                    messages = new string[] { "Su sesión ha expirado, vuelva a realizar el ingreso." }
                };

                return Unauthorized(notFound);
            }

            var user = await _userServices.GetbyId(data.UserId);

            if (data.OldPassword != UtilsSegurity.DecodePassword(user.result.Password))
            {
                var notFound = new DataResult<User>()
                {
                    id = data.UserId.ToString(),
                    result = new User(),
                    messageType = nameof(MessageTypeResultEnum.Warning),
                    error = true,
                    messages = new string[] { "La contraseña actual ingresada no coninside." }
                };
                return Ok(notFound);
            }

            var result = await _userServices.ChangePassword(data.UserId, data.NewPassword);
            if (!result)
            {
                var notFound = new DataResult<User>()
                {
                    id = data.UserId.ToString(),
                    result = new User(),
                    messageType = nameof(MessageTypeResultEnum.Error),
                    error = true,
                    messages = new string[] { "Ocurrio un error al guardar la contraseña." }
                };
                return Ok(notFound);
            }

            user.messageType = nameof(MessageTypeResultEnum.Success);
            user.messages = new string[] { "Contraseña guardada con éxito, Se Cerrará la sesión" };

            return Ok(user);
        }
    }
}