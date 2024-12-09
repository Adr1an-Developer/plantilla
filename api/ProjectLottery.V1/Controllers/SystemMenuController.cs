using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectLottery.V1.Helpers.Utils;
using ProjectLottery.V1.Domain.DTOs.Common;
using ProjectLottery.V1.Domain.DTOs.SystemMenu;
using System;
using System.Threading.Tasks;
using ProjectLottery.V1.Entities.Security;
using ProjectLottery.V1.Domain.Enums;
using ProjectLottery.V1.Domain.DTOs.Auth;
using ProjectLottery.V1.Entities.System;
using ProjectLottery.V1.Domain.Services.Abstractions.Config;

namespace ProjectLottery.V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemMenuController : ControllerBase
    {
        private IHttpContextAccessor _httpContextAccessor;
        private ISystemMenuServices _SystemMenuServices;

        public SystemMenuController(ISystemMenuServices SystemMenuServices, IHttpContextAccessor httpContextAccessor)
        {
            _SystemMenuServices = SystemMenuServices;
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

            var results = await _SystemMenuServices.GetAll();

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

            var results = await _SystemMenuServices.GetbyId(id);

            return Ok(results);
        }

        //[Authorize]
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> post([FromBody] AddSystemMenu SystemMenusdata)
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

            if (SystemMenusdata == null) return BadRequest();

            var result = await _SystemMenuServices.Create(SystemMenusdata);

            return Ok(result);
        }

        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateSystemMenu(string id, SystemMenu SystemMenusdata)
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

            if (SystemMenusdata == null) return BadRequest();

            var result = await _SystemMenuServices.Update(SystemMenusdata);

            return Ok(result);
        }

        //[Authorize]
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteSystemMenu(string id)
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

            var result = await _SystemMenuServices.Delete(id);

            return Ok(result);
        }
    }
}