using Microsoft.AspNetCore.Http;
using ProjectLottery.V1.Domain.Data.Abstractions.System;
using ProjectLottery.V1.Domain.DTOs.Auth;
using ProjectLottery.V1.Domain.DTOs.Common;
using ProjectLottery.V1.Domain.DTOs.SystemClient;
using ProjectLottery.V1.Domain.Services.Abstractions.System;
using ProjectLottery.V1.Domain.Services.Implementations.Security;
using ProjectLottery.V1.Entities.Global;
using ProjectLottery.V1.Entities.System;
using ProjectLottery.V1.Helpers.Exceptions;

namespace ProjectLottery.V1.Domain.Services.Implementations.System
{
    public class ClientServices : ServiceBase<SystemClient>, IClientServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IClientRepository _repository;
        private string _serviceName;
        private LoggedOutput _userInfologged;

        public ClientServices(IClientRepository repository, IHttpContextAccessor httpContextAccessor) : base(repository)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            // Información de usuario logueado
            _userInfologged = Helpers.Mapper.GetLoggedInfo(_httpContextAccessor);
            _serviceName = nameof(UserServices);
            _repository.SetLoggedUserInfo(_userInfologged.UserID, _userInfologged.isAdmin);
        }

        public async Task<DataResults<SystemClient>> GetAll()
        {
            var resultData = new DataResults<SystemClient>();
            try
            {
                var rows = await _repository.GetAllSystemClient();

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

        public async Task<DataResult<SystemClient>> GetbyId(string id)
        {
            var resultData = new DataResult<SystemClient>();
            try
            {
                var row = await _repository.GetSystemClientById(id);

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

        public async Task<DataResult<SystemClient>> GetByName(string clientName)
        {
            var resultData = new DataResult<SystemClient>();
            try
            {
                var row = await _repository.GetSystemClientByName(clientName);

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

        public async Task<DataResult<SystemClient>> Create(AddSystemClient entity)
        {
            var resultData = new DataResult<SystemClient>();
            try
            {
                var item = await _repository.FindByConditionAsync(x => x.CompanyName == entity.CompanyName
                                                                 && x.IsDeleted == false);

                if (item.Any())
                {
                    resultData.messageType = nameof(Enums.MessageTypeResultEnum.Info);
                    resultData.error = true;
                    resultData.id = string.Empty;
                    resultData.result = null;
                    resultData.messages = new List<string>()
                    {
                       $"{typeof(SystemClient)} ya existe."
                    };
                    return resultData;
                }

                var newId = Guid.NewGuid().ToString();

                var newRow = new SystemClient()
                {
                    Id = newId,
                    CompanyName = entity.CompanyName,
                    Address = entity.Address,
                    CityId = newId,
                    PostalCode = entity.PostalCode,
                    ClientTypeId = entity.ClientTypeId,
                    Email = entity.Email,
                    Notes = entity.Notes,
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    Phone = entity.Phone,
                    Register_Id = entity.Register_Id,
                    Status = entity.Status,
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

        public async Task<DataResult<SystemClient>> Update(SystemClient entity)
        {
            var resultData = new DataResult<SystemClient>();
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

        public async Task<DataResult<SystemClient>> Delete(string id)
        {
            var resultData = new DataResult<SystemClient>();
            try
            {
                var row = await _repository.GetSystemClientById(id);

                row.Delete(_userInfologged.UserID);

                await _repository.Update(row);
                await _repository.UnitOfWork.SaveAsync();

                resultData.messageType = nameof(Enums.MessageTypeResultEnum.Info);
                resultData.error = false;
                resultData.id = row.Id.ToString();
                resultData.result = row;
                resultData.messages = new List<string>()
                {
                    $"Registro '{row.CompanyName}' eliminado con éxito"
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