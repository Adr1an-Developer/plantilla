using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectLottery.V1.Helpers.Utils;
using ProjectLottery.V1.Domain.DTOs.Common;
using ProjectLottery.V1.Domain.DTOs.MenuPermission;
using System;
using System.Threading.Tasks;
using ProjectLottery.V1.Domain.Enums;
using ProjectLottery.V1.Entities.System;
using ProjectLottery.V1.Domain.Services.Abstractions.Config;

namespace ProjectLottery.V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuPermissionController : ControllerBase
    {
        private IHttpContextAccessor _httpContextAccessor;
        private IMenuPermissionServices _MenuPermissionServices;

        public MenuPermissionController(IMenuPermissionServices MenuPermissionServices, IHttpContextAccessor httpContextAccessor)
        {
            _MenuPermissionServices = MenuPermissionServices;
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

            var results = await _MenuPermissionServices.GetAll();

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

            var results = await _MenuPermissionServices.GetbyId(id);

            return Ok(results);
        }

        [HttpGet]
        [Route("getByProfile/{id}")]
        public async Task<IActionResult> GetByProfile(string id)
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

            var results = await _MenuPermissionServices.GetMenuPermissionByProfileId(id);

            return Ok(results);
        }

        //[Authorize]
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> post([FromBody] AddMenuPermission MenuPermissionsdata)
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

            if (MenuPermissionsdata == null) return BadRequest();

            var result = await _MenuPermissionServices.Create(MenuPermissionsdata);

            return Ok(result);
        }

        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> UpdateMenuPermission(string id, MenuPermission MenuPermissionsdata)
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

            if (MenuPermissionsdata == null) return BadRequest();

            var result = await _MenuPermissionServices.Update(MenuPermissionsdata);

            return Ok(result);
        }

        //[Authorize]
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteMenuPermission(string id)
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

            var result = await _MenuPermissionServices.Delete(id);

            return Ok(result);
        }
    }
}