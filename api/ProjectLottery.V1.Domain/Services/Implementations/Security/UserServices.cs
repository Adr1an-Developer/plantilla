using Microsoft.AspNetCore.Http;
using ProjectLottery.V1.Domain.Data.Abstractions.Security;
using ProjectLottery.V1.Domain.DTOs.Auth;
using ProjectLottery.V1.Domain.DTOs.Common;
using ProjectLottery.V1.Domain.DTOs.User;
using ProjectLottery.V1.Domain.Services.Abstractions.Security;
using ProjectLottery.V1.Entities.Security;
using ProjectLottery.V1.Helpers.Exceptions;
using ProjectLottery.V1.Helpers.Utils;

namespace ProjectLottery.V1.Domain.Services.Implementations.Security
{
    public class UserServices : ServiceBase<User>, IUserServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUsersRepository _usersRepository;
        private string _serviceName;
        private LoggedOutput _userInfologged;

        public UserServices(
          IUsersRepository usersRepository,
            IHttpContextAccessor httpContextAccessor
          ) : base(usersRepository)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));

            // Información de usuario logueado
            _userInfologged = Helpers.Mapper.GetLoggedInfo(_httpContextAccessor);
            _serviceName = nameof(UserServices);
            _usersRepository.SetLoggedUserInfo(_userInfologged.UserID, _userInfologged.isAdmin);
        }

        public async Task<DataResults<User>> GetAll()
        {
            var resultData = new DataResults<User>();
            try
            {
                var users = await _usersRepository.GetAllUsers();

                var validar = users.Count();

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

                resultData.totalRecords = users.Count();
                resultData.messageType = nameof(Enums.MessageTypeResultEnum.Info);
                resultData.error = false;
                resultData.results = users;
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

        public async Task<DataResult<User>> Create(AddUser entity)
        {
            var resultData = new DataResult<User>();
            try
            {
                var user = await _usersRepository.FindByConditionAsync(x => (x.UserName == entity.UserName || x.Email == entity.Email) && x.IsDeleted == false);

                if (user.Count() > 0)
                {
                    resultData.messageType = nameof(Enums.MessageTypeResultEnum.Info);
                    resultData.error = true;
                    resultData.id = string.Empty;
                    resultData.result = null;
                    resultData.messages = new List<string>()
                    {
                        "Nombre de usuario o email ya existe."
                    };
                    return resultData;
                }

                var newId = Guid.NewGuid().ToString();

                var newUser = new User()
                {
                    Id = newId,
                    CreateByUser = _userInfologged.UserID,
                    Email = entity.Email,
                    ProfileId = entity.ProfileId,
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    Password = UtilsSegurity.EncodePassword("123456"),
                    IsActive = true,
                    IsDeleted = false,
                    UserName = entity.UserName,
                    CreationDate = DateTime.UtcNow,
                    isFirstLogin = true,
                    ExternalCode = entity.ExternalCode
                };

                var resultUser = await _usersRepository.CreateAsync(newUser);

                await _usersRepository.UnitOfWork.SaveAsync();

                resultData.messageType = nameof(Enums.MessageTypeResultEnum.Info);
                resultData.error = false;
                resultData.id = newId.ToString();
                resultData.result = newUser;
                resultData.messages = new List<string>()
                {
                    "Datos guardados con �xito"
                };
                return resultData;
            }
            catch (Exception ex)
            {
                var message = $"{_userInfologged.UserID},{_serviceName}";
                throw new ExceptionCustom(message, ex);
            }
        }

        public async Task<DataResult<User>> GetByName(string userName)
        {
            var resultData = new DataResult<User>();
            try
            {
                var user = await _usersRepository.GetUserByName(userName);

                resultData.messageType = nameof(Enums.MessageTypeResultEnum.Info);
                resultData.error = false;
                resultData.id = user.Id.ToString();
                resultData.result = user;
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

        public async Task<DataResult<User>> GetbyId(string id)
        {
            var resultData = new DataResult<User>();
            try
            {
                var user = await _usersRepository.GetUserByID(id);

                resultData.messageType = nameof(Enums.MessageTypeResultEnum.Info);
                resultData.error = false;
                resultData.id = user.Id.ToString();
                resultData.result = user;
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

        public async Task<DataResult<User>> Update(User user)
        {
            var resultData = new DataResult<User>();
            try
            {
                user.UpdateByUser = _userInfologged.UserID;
                user.ModificationDate = DateTime.Now;
                await _usersRepository.Update(user);
                await _usersRepository.UnitOfWork.SaveAsync();

                resultData.messageType = nameof(Enums.MessageTypeResultEnum.Info);
                resultData.error = false;
                resultData.id = user.Id.ToString();
                resultData.result = user;
                resultData.messages = new List<string>()
                {
                    "Datos actualizados con �xito"
                };
                return resultData;
            }
            catch (Exception ex)
            {
                var message = $"{_userInfologged.UserID},{_serviceName}";
                throw new ExceptionCustom(message, ex);
            }
        }

        public async Task<DataResult<User>> Delete(string id)
        {
            var resultData = new DataResult<User>();
            try
            {
                var user = await _usersRepository.GetUserByID(id);

                user.Delete(_userInfologged.UserID);

                await _usersRepository.Update(user);
                await _usersRepository.UnitOfWork.SaveAsync();

                resultData.messageType = nameof(Enums.MessageTypeResultEnum.Info);
                resultData.error = false;
                resultData.id = user.Id;
                resultData.result = user;
                resultData.messages = new List<string>()
                {
                    $"Registro '{user.UserName}' eliminado con �xito"
                };
                return resultData;
            }
            catch (Exception ex)
            {
                var message = $"{_userInfologged.UserID},{_serviceName}";
                throw new ExceptionCustom(message, ex);
            }
        }

        public async Task<bool> ChangePassword(string userID, string password)
        {
            var resultData = new DataResult<User>();
            try
            {
                var user = await _usersRepository.GetUserByID(userID);

                if (user == null) return false;
                if (password == null) return false;

                var newPassword = UtilsSegurity.EncodePassword(password);

                user.Password = newPassword;
                user.isFirstLogin = false;
                user.UpdateAuditInfo(_userInfologged.UserID);

                await _usersRepository.Update(user);
                var Result = _usersRepository.UnitOfWork.SaveAsync();
                return true;
            }
            catch (Exception ex)
            {
                var message = $"{_userInfologged.UserID},{_serviceName}";
                throw new ExceptionCustom(message, ex);
            }
        }
    }
}