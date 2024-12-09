using Microsoft.AspNetCore.Http;
using ProjectLottery.V1.Domain.Data.Abstractions.Config;
using ProjectLottery.V1.Domain.DTOs.Auth;
using ProjectLottery.V1.Domain.DTOs.Common;
using ProjectLottery.V1.Domain.DTOs.SystemMenu;
using ProjectLottery.V1.Domain.Services.Abstractions.Config;
using ProjectLottery.V1.Domain.Services.Implementations.Security;
using ProjectLottery.V1.Entities.System;
using ProjectLottery.V1.Helpers.Exceptions;

namespace ProjectLottery.V1.Domain.Services.Implementations.Config
{
    public class SystemMenuServices : ServiceBase<SystemMenu>, ISystemMenuServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISystemMenuRepository _repository;
        private string _serviceName;
        private LoggedOutput _userInfologged;

        public SystemMenuServices(ISystemMenuRepository repository, IHttpContextAccessor httpContextAccessor) : base(repository)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            // Información de usuario logueado
            _userInfologged = Helpers.Mapper.GetLoggedInfo(_httpContextAccessor);
            _serviceName = nameof(UserServices);
            _repository.SetLoggedUserInfo(_userInfologged.UserID, _userInfologged.isAdmin);
        }

        public async Task<DataResults<SystemMenu>> GetAll()
        {
            var resultData = new DataResults<SystemMenu>();
            try
            {
                var rows = await _repository.GetAllSystemMenus();

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

        public async Task<DataResult<SystemMenu>> GetbyId(string id)
        {
            var resultData = new DataResult<SystemMenu>();
            try
            {
                var row = await _repository.GetSystemMenuByID(id);

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

        public async Task<DataResult<SystemMenu>> GetByName(string SystemMenuName)
        {
            var resultData = new DataResult<SystemMenu>();
            try
            {
                var row = await _repository.GetSystemMenuByName(SystemMenuName);

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

        public async Task<DataResult<SystemMenu>> Create(AddSystemMenu entity)
        {
            var resultData = new DataResult<SystemMenu>();
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
                       $"{typeof(SystemMenu)} ya existe."
                    };
                    return resultData;
                }

                var newId = Guid.NewGuid().ToString();

                var newRow = new SystemMenu()
                {
                    Id = newId,
                    Name = entity.Name,
                    Title = entity.Title,
                    Icon = entity.Icon,
                    Order = entity.Order,
                    Group = entity.Group,
                    Url = entity.Url,
                    ParentMenuId = entity.ParentMenuId,
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

        public async Task<DataResult<SystemMenu>> Update(SystemMenu entity)
        {
            var resultData = new DataResult<SystemMenu>();
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

        public async Task<DataResult<SystemMenu>> Delete(string id)
        {
            var resultData = new DataResult<SystemMenu>();
            try
            {
                var row = await _repository.GetSystemMenuByID(id);

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