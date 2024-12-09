using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectLottery.V1.Helpers.Utils;
using ProjectLottery.V1.Domain.DTOs.Common;
using System;
using System.Threading.Tasks;
using ProjectLottery.V1.Domain.Enums;
using ProjectLottery.V1.Domain.Services.Abstractions.System;
using ProjectLottery.V1.Domain.DTOs.SystemClient;
using ProjectLottery.V1.Entities.System;

namespace ProjectLottery.V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private IHttpContextAccessor _httpContextAccessor;
        private IClientServices _ClientServices;

        public ClientController(IClientServices ClientServices, IHttpContextAccessor httpContextAccessor)
        {
            _ClientServices = ClientServices;
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

            var results = await _ClientServices.GetAll();

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

            var results = await _ClientServices.GetbyId(id);

            return Ok(results);
        }

        //[Authorize]
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> post([FromBody] AddSystemClient clientdata)
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

            if (clientdata == null) return BadRequest();

            var result = await _ClientServices.Create(clientdata);

            return Ok(result);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateClient(Guid id, SystemClient clientdata)
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

            if (clientdata == null) return BadRequest();

            var result = await _ClientServices.Update(clientdata);

            return Ok(result);
        }

        //[Authorize]
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteClient(string id)
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

            var result = await _ClientServices.Delete(id);

            return Ok(result);
        }
    }
}