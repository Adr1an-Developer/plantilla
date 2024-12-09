using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectLottery.V1.Helpers.Utils;
using ProjectLottery.V1.Domain.DTOs.Common;
using System.Threading.Tasks;
using ProjectLottery.V1.Domain.Enums;
using ProjectLottery.V1.Domain.Services.Abstractions.Regions;

namespace ProjectLottery.V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private IHttpContextAccessor _httpContextAccessor;
        private ICountryServices _countryServices;
        private IStateServices _stateServices;
        private ICityServices _cityServices;

        public RegionsController(ICountryServices countryServices,
                                IStateServices stateServices,
                                ICityServices cityServices,
                                IHttpContextAccessor httpContextAccessor)
        {
            _cityServices = cityServices;
            _countryServices = countryServices;
            _stateServices = stateServices;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Route("getAll/{option}")]
        public async Task<IActionResult> GetAll(string option)
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

            switch (option.ToUpper())
            {
                case nameof(OptionRegionsEnum.COUNTRY):
                    return Ok(await _countryServices.GetAll());

                case nameof(OptionRegionsEnum.STATE):
                    return Ok(await _stateServices.GetAll());

                case nameof(OptionRegionsEnum.CITY):
                    return Ok(await _cityServices.GetAll());

                default:

                    var notFound = new ResultNotFound()
                    {
                        messageType = nameof(MessageTypeResultEnum.Warning),
                        error = false,
                        messages = new string[] { $"La opción {option} seleccionada es invalida" }
                    };
                    return Ok(notFound);
            }
        }

        [HttpGet]
        [Route("get/{option}/{id}")]
        public async Task<IActionResult> Get(string option, string id)
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

            switch (option.ToUpper())
            {
                case nameof(OptionRegionsEnum.COUNTRY):
                    return Ok(await _countryServices.GetbyId(id));

                case nameof(OptionRegionsEnum.STATE):
                    return Ok(await _stateServices.GetbyId(id));

                case nameof(OptionRegionsEnum.CITY):
                    return Ok(await _cityServices.GetbyId(id));

                default:

                    var notFound = new ResultNotFound()
                    {
                        messageType = nameof(MessageTypeResultEnum.Warning),
                        error = false,
                        messages = new string[] { $"La opción {option} seleccionada es invalida" }
                    };
                    return Ok(notFound);
            }
        }
    }
}