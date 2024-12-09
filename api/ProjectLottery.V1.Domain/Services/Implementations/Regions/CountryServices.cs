using Microsoft.AspNetCore.Http;
using ProjectLottery.V1.Domain.Data.Abstractions.Regions;
using ProjectLottery.V1.Domain.DTOs.Auth;
using ProjectLottery.V1.Domain.DTOs.Common;
using ProjectLottery.V1.Domain.Services.Abstractions.Regions;
using ProjectLottery.V1.Domain.Services.Implementations.Security;
using ProjectLottery.V1.Entities.Global;
using ProjectLottery.V1.Helpers.Exceptions;

namespace ProjectLottery.V1.Domain.Services.Implementations.Regions
{
    public class CountryServices : ServiceReadOnlyBase<Country>, ICountryServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICountryRepository _repository;
        private string _serviceName;
        private LoggedOutput _userInfologged;

        public CountryServices(ICountryRepository repository, IHttpContextAccessor httpContextAccessor) : base(repository)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            // Información de usuario logueado
            _userInfologged = Helpers.Mapper.GetLoggedInfo(_httpContextAccessor);
            _serviceName = nameof(UserServices);
            _repository.SetLoggedUserInfo(_userInfologged.UserID, _userInfologged.isAdmin);
        }

        public async Task<DataResults<Country>> GetAll()
        {
            var resultData = new DataResults<Country>();
            try
            {
                var rows = await _repository.GetAllCountrys();

                var validar = rows.Count();

                if (validar == 0)
                {
                    resultData.messageType = nameof(Enums.MessageTypeResultEnum.Warning);
                    resultData.error = false;
                    resultData.results = null;
                    resultData.messages = new List<string>()
                    {
                        "Datos no encontrados."
                    };
                    return resultData;
                }

                resultData.totalRecords = rows.Count();
                resultData.messageType = nameof(Enums.MessageTypeResultEnum.Info);
                resultData.error = false;
                resultData.results = rows;
                resultData.messages = new List<string>()
                {
                    "Datos encontrados"
                };
                return resultData;
            }
            catch (Exception ex)
            {
                var message = $"{_userInfologged.UserID},{_serviceName}";
                throw new ExceptionCustom(message, ex);
            }
        }

        public async Task<DataResult<Country>> GetbyId(string id)
        {
            var resultData = new DataResult<Country>();
            try
            {
                var row = await _repository.GetCountryByID(id);

                resultData.messageType = nameof(Enums.MessageTypeResultEnum.Info);
                resultData.error = false;
                resultData.id = row.Id;
                resultData.result = row;
                resultData.messages = new List<string>()
                {
                    "Datos encontrados"
                };
                return resultData;
            }
            catch (Exception ex)
            {
                var message = $"{_userInfologged.UserID},{_serviceName}";
                throw new ExceptionCustom(message, ex);
            }
        }

        public async Task<DataResult<Country>> GetByName(string CountryName)
        {
            var resultData = new DataResult<Country>();
            try
            {
                var row = await _repository.GetCountryByName(CountryName);

                resultData.messageType = nameof(Enums.MessageTypeResultEnum.Info);
                resultData.error = false;
                resultData.id = row.Id;
                resultData.result = row;
                resultData.messages = new List<string>()
                {
                    "Datos encontrados"
                };
                return resultData;
            }
            catch (Exception ex)
            {
                var message = $"{_userInfologged.UserID},{_serviceName}";
                throw new ExceptionCustom(message, ex);
            }
        }
    }
}