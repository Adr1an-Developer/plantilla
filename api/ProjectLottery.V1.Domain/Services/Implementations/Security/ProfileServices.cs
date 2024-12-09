using Microsoft.AspNetCore.Http;
using ProjectLottery.V1.Domain.Data.Abstractions.Security;
using ProjectLottery.V1.Domain.Data.Repository;
using ProjectLottery.V1.Domain.DTOs.Auth;
using ProjectLottery.V1.Domain.DTOs.Common;
using ProjectLottery.V1.Domain.DTOs.Profile;
using ProjectLottery.V1.Domain.Services.Abstractions.Security;
using ProjectLottery.V1.Entities.Security;
using ProjectLottery.V1.Helpers.Exceptions;
using ProjectLottery.V1.Helpers.Utils;

namespace ProjectLottery.V1.Domain.Services.Implementations.Security
{
    public class ProfileServices : ServiceBase<Profile>, IProfileServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProfileRepository _repository;
        private string _serviceName;
        private LoggedOutput _userInfologged;

        public ProfileServices(IProfileRepository profileRepository, IHttpContextAccessor httpContextAccessor) : base(profileRepository)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _repository = profileRepository ?? throw new ArgumentNullException(nameof(profileRepository));
            // Información de usuario logueado
            _userInfologged = Helpers.Mapper.GetLoggedInfo(_httpContextAccessor);
            _serviceName = nameof(UserServices);
            _repository.SetLoggedUserInfo(_userInfologged.UserID, _userInfologged.isAdmin);
        }

        public async Task<DataResults<Profile>> GetAll()
        {
            var resultData = new DataResults<Profile>();
            try
            {
                var rows = await _repository.GetAllProfiles();

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

        public async Task<DataResult<Profile>> GetbyId(string id)
        {
            var resultData = new DataResult<Profile>();
            try
            {
                var row = await _repository.GetProfileByID(id);

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

        public async Task<DataResult<Profile>> GetByName(string ProfileName)
        {
            var resultData = new DataResult<Profile>();
            try
            {
                var row = await _repository.GetProfileByName(ProfileName);

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

        public async Task<DataResult<Profile>> Create(AddProfile entity)
        {
            var resultData = new DataResult<Profile>();
            try
            {
                var item = await _repository.FindByConditionAsync(x => x.Name.ToLower().Trim() == entity.Name.ToLower().Trim() && x.IsDeleted == false);

                if (item.Any())
                {
                    resultData.messageType = nameof(Enums.MessageTypeResultEnum.Info);
                    resultData.error = true;
                    resultData.id = string.Empty;
                    resultData.result = null;
                    resultData.messages = new List<string>()
                    {
                       $"{typeof(Profile)} ya existe."
                    };
                    return resultData;
                }

                var newId = Guid.NewGuid().ToString();

                var newRow = new Profile()
                {
                    Id = newId,
                    Name = entity.Name,
                    Abbreviation = entity.Abbreviation,
                    CreateByUser = _userInfologged.UserID,
                    IsActive = true,
                    IsDeleted = false,
                    CreationDate = DateTime.UtcNow,
                };

                var result = await _repository.CreateAsync(newRow);

                await _repository.UnitOfWork.SaveAsync();

                resultData.messageType = nameof(Enums.MessageTypeResultEnum.Info);
                resultData.error = false;
                resultData.id = newId;
                resultData.result = newRow;
                resultData.messages = new List<string>()
                {
                    "Datos guardados con éxito"
                };
                return resultData;
            }
            catch (Exception ex)
            {
                var message = $"{_userInfologged.UserID},{_serviceName}";
                throw new ExceptionCustom(message, ex);
            }
        }

        public async Task<DataResult<Profile>> Update(Profile entity)
        {
            var resultData = new DataResult<Profile>();
            try
            {
                entity.UpdateAuditInfo(_userInfologged.UserID);
                await _repository.Update(entity);
                await _repository.UnitOfWork.SaveAsync();

                resultData.messageType = nameof(Enums.MessageTypeResultEnum.Info);
                resultData.error = false;
                resultData.id = entity.Id;
                resultData.result = entity;
                resultData.messages = new List<string>()
                {
                    "Datos actualizados con éxito"
                };
                return resultData;
            }
            catch (Exception ex)
            {
                var message = $"{_userInfologged.UserID},{_serviceName}";
                throw new ExceptionCustom(message, ex);
            }
        }

        public async Task<DataResult<Profile>> Delete(string id)
        {
            var resultData = new DataResult<Profile>();
            try
            {
                var row = await _repository.GetProfileByID(id);

                row.Delete(_userInfologged.UserID);

                await _repository.Update(row);
                await _repository.UnitOfWork.SaveAsync();

                resultData.messageType = nameof(Enums.MessageTypeResultEnum.Info);
                resultData.error = false;
                resultData.id = row.Id.ToString();
                resultData.result = row;
                resultData.messages = new List<string>()
                {
                    $"Registro '{row.Name}' eliminado con éxito"
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