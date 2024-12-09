using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectLottery.V1.Helpers.Utils;
using ProjectLottery.V1.Domain.DTOs.Common;
using ProjectLottery.V1.Domain.DTOs.Profile;
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
    public class ProfileController : ControllerBase
    {
        private IHttpContextAccessor _httpContextAccessor;
        private IProfileServices _ProfileServices;

        public ProfileController(IProfileServices ProfileServices, IHttpContextAccessor httpContextAccessor)
        {
            _ProfileServices = ProfileServices;
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

            var results = await _ProfileServices.GetAll();

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

            var results = await _ProfileServices.GetbyId(id);

            return Ok(results);
        }

        //[Authorize]
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> post([FromBody] AddProfile Profilesdata)
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

            if (Profilesdata == null) return BadRequest();

            var result = await _ProfileServices.Create(Profilesdata);

            return Ok(result);
        }

        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateProfile(Guid id, Profile Profilesdata)
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

            if (Profilesdata == null) return BadRequest();

            var result = await _ProfileServices.Update(Profilesdata);

            return Ok(result);
        }

        //[Authorize]
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteProfile(string id)
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

            var result = await _ProfileServices.Delete(id);

            return Ok(result);
        }
    }
}