using Microsoft.AspNetCore.Http;
using ProjectLottery.V1.Domain.Data.Abstractions;
using ProjectLottery.V1.Domain.Data.Abstractions.Security;
using ProjectLottery.V1.Domain.DTOs.Auth;
using ProjectLottery.V1.Domain.Services.Abstractions.Security;

namespace ProjectLottery.V1.Domain.Services.Implementations.Security
{
    public class AuthorizeServices : IAuthorizeServices
    {
        private readonly IJWTManagerRepository _jWTManagerRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUsersRepository _usersRepository;

        public AuthorizeServices(
            IJWTManagerRepository JWTManagerRepository,
            IHttpContextAccessor httpContextAccessor,
            IUsersRepository usersRepository
         )
        {
            _httpContextAccessor = httpContextAccessor;
            _jWTManagerRepository = JWTManagerRepository ?? throw new ArgumentNullException(nameof(JWTManagerRepository));
            _usersRepository = usersRepository ?? throw new ArgumentNullException(nameof(usersRepository));
        }

        public async Task<Tokens> Authenticate(Login login)
        {
            var user = (await _usersRepository.Login(login)).FirstOrDefault();

            if (user == null)
            {
                return new Tokens();
            }

            var result = await _jWTManagerRepository.GetToken(user);

            return result;
        }
    }
}