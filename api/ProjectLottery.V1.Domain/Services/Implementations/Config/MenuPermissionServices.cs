using Microsoft.AspNetCore.Http;
using ProjectLottery.V1.Domain.Data.Abstractions.Config;
using ProjectLottery.V1.Domain.DTOs.Auth;
using ProjectLottery.V1.Domain.DTOs.Common;
using ProjectLottery.V1.Domain.DTOs.MenuPermission;
using ProjectLottery.V1.Domain.Services.Abstractions.Config;
using ProjectLottery.V1.Domain.Services.Implementations.Security;
using ProjectLottery.V1.Entities.System;
using ProjectLottery.V1.Helpers.Exceptions;

namespace ProjectLottery.V1.Domain.Services.Implementations.Config
{
    public class MenuPermissionServices : ServiceBase<MenuPermission>, IMenuPermissionServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMenuPermissionRepository _repository;
        private string _serviceName;
        private LoggedOutput _userInfologged;

        public MenuPermissionServices(IMenuPermissionRepository repository, IHttpContextAccessor httpContextAccessor) : base(repository)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            // Información de usuario logueado
            _userInfologged = Helpers.Mapper.GetLoggedInfo(_httpContextAccessor);
            _serviceName = nameof(UserServices);
            _repository.SetLoggedUserInfo(_userInfologged.UserID, _userInfologged.isAdmin);
        }

        public async Task<DataResults<MenuPermission>> GetAll()
        {
            var resultData = new DataResults<MenuPermission>();
            try
            {
                var rows = await _repository.GetAllMenuPermissions();

                if (!rows.Any())
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

        public async Task<DataResult<MenuPermission>> GetbyId(string id)
        {
            var resultData = new DataResult<MenuPermission>();
            try
            {
                var row = await _repository.GetMenuPermissionByID(id);

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

        public async Task<DataResult<MenuPermission>> Create(AddMenuPermission entity)
        {
            var resultData = new DataResult<MenuPermission>();
            try
            {
                var item = await _repository.FindByConditionAsync(x => x.MenuId == entity.MenuId
                                                                 && x.ProfileId == entity.ProfileId
                                                                 && x.IsDeleted == false);

                if (item.Any())
                {
                    resultData.messageType = nameof(Enums.MessageTypeResultEnum.Info);
                    resultData.error = true;
                    resultData.id = string.Empty;
                    resultData.result = null;
                    resultData.messages = new List<string>()
                    {
                       $"Para este perfil este menu ya existe."
                    };
                    return resultData;
                }

                var newId = Guid.NewGuid().ToString();

                var newRow = new MenuPermission()
                {
                    Id = newId,
                    ProfileId = entity.ProfileId,
                    MenuId = entity.MenuId,
                    CanAdd = entity.CanAdd,
                    CanAuthorize = entity.CanAuthorize,
                    CanDelete = entity.CanDelete,
                    CanEdit = entity.CanEdit,
                    CanExport = entity.CanExport,
                    CanView = entity.CanView,
                    CreateByUser = _userInfologged.UserID,
                    IsActive = true,
                    IsDeleted = false,
                    CreationDate = DateTime.UtcNow,
                };

                var result = await _repository.CreateAsync(newRow);

                await _repository.UnitOfWork.SaveAsync();

                resultData.messageType = nameof(Enums.MessageTypeResultEnum.Success);
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

        public async Task<DataResult<MenuPermission>> Update(MenuPermission entity)
        {
            var resultData = new DataResult<MenuPermission>();
            try
            {
                var item = (await _repository.FindByConditionAsync(x => x.MenuId == entity.MenuId
                                                                 && x.ProfileId == entity.ProfileId
                                                                && x.IsDeleted == false));

                if (item.Any())
                {
                    resultData.messageType = nameof(Enums.MessageTypeResultEnum.Info);
                    resultData.error = true;
                    resultData.id = string.Empty;
                    resultData.result = null;
                    resultData.messages = new List<string>()
                    {
                       $"Para el perfil {item.First().ProfileName} este menu {item.First().MenuName} ya existe."
                    };
                    return resultData;
                }

                entity.UpdateAuditInfo(_userInfologged.UserID);
                await _repository.Update(entity);
                await _repository.UnitOfWork.SaveAsync();

                resultData.messageType = nameof(Enums.MessageTypeResultEnum.Success);
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

        public async Task<DataResult<MenuPermission>> Delete(string id)
        {
            var resultData = new DataResult<MenuPermission>();
            try
            {
                var row = await _repository.GetMenuPermissionByID(id);

                row.Delete(_userInfologged.UserID);

                await _repository.Update(row);
                await _repository.UnitOfWork.SaveAsync();

                resultData.messageType = nameof(Enums.MessageTypeResultEnum.Info);
                resultData.error = false;
                resultData.id = row.Id.ToString();
                resultData.result = row;
                resultData.messages = new List<string>()
                {
                    $"Registro '{row.MenuName}-{row.ProfileName}' eliminado con éxito"
                };
                return resultData;
            }
            catch (Exception ex)
            {
                var message = $"{_userInfologged.UserID},{_serviceName}";
                throw new ExceptionCustom(message, ex);
            }
        }

        public async Task<DataResults<MenuPermission>> GetMenuPermissionByProfileId(string profileId)
        {
            var resultData = new DataResults<MenuPermission>();
            try
            {
                var rows = await _repository.GetMenuPermissionByProfileId(profileId);

                if (!rows.Any())
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
    }
}