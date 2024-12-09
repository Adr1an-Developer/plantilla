using ProjectLottery.V1.Helpers.DTOs;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace ProjectLottery.V1.Helpers.Utils
{
    public class UtilsSegurity
    {
        public static string EncodePassword(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

        public static string DecodePassword(string encodedData)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new string(decoded_char);
            return result;
        }

        private static LoggedOutput getLoggedUser(string token)
        {
            LoggedOutput result = new LoggedOutput();
            try
            {
                if (string.IsNullOrEmpty(token)) return result;

                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(token);

                var _name = jwt.Claims.First(claim => claim.Type == "Name").Value;
                var _profileName = jwt.Claims.First(claim => claim.Type == "Profile").Value;
                var _fullname = jwt.Claims.First(claim => claim.Type == "FullName").Value;
                var _email = jwt.Claims.First(claim => claim.Type == "Email").Value;
                var _userID = jwt.Claims.First(claim => claim.Type == "UserId").Value;
                int timeExp = 0;
                var isValidExp = int.TryParse(jwt.Claims.First(claim => claim.Type == "exp").Value.ToString(), out timeExp);

                var dateExp = DateTimeOffset.FromUnixTimeSeconds(timeExp).LocalDateTime;

                result.isValidExp = dateExp > DateTime.Now;

                result.Name = _name;
                result.UserID = _userID;
                result.Profile = _profileName;
                result.Email = _email;
                result.Fullname = _fullname;

                return result;
            }
            catch (Exception ex)
            {
                throw new(ex.Message);
            }
        }

        public static LoggedOutput getLoggedUser(IHttpContextAccessor httpContextAccessor)
        {
            LoggedOutput result = new LoggedOutput();
            try
            {
                var httpContext = httpContextAccessor.HttpContext;

                var jwt = httpContext.User;

                if (!jwt.Claims.Any())
                {
                    return result;
                }

                var _name = jwt.Claims.First(claim => claim.Type == "Name").Value;
                var _profileName = jwt.Claims.First(claim => claim.Type == "Profile").Value;
                var _fullname = jwt.Claims.First(claim => claim.Type == "FullName").Value;
                var _email = jwt.Claims.First(claim => claim.Type == "Email").Value;
                var _userID = jwt.Claims.First(claim => claim.Type == "UserId").Value;
                int timeExp = 0;
                var isValidExp = int.TryParse(jwt.Claims.First(claim => claim.Type == "exp").Value.ToString(), out timeExp);

                var dateExp = DateTimeOffset.FromUnixTimeSeconds(timeExp).LocalDateTime;

                result.isValidExp = dateExp > DateTime.Now;

                result.Name = _name;
                result.UserID = _userID;
                result.Profile = _profileName;
                result.Email = _email;
                result.Fullname = _fullname;

                return result;
            }
            catch (Exception ex)
            {
                throw new(ex.Message);
            }
        }

        public static (LoggedOutput, bool) ValidateToken(string token, bool hasBearer = true)
        {
            string accessToken = token;
            if (string.IsNullOrEmpty(accessToken))
            {
                return (null, false);
            }

            if (hasBearer)
            {
                accessToken = token.Replace("Bearer ", "");
            }

            var result = getLoggedUser(accessToken);

            return (result, result != null);
        }

        public static (LoggedOutput, bool) ValidateToken(IHttpContextAccessor httpContextAccessor)
        {
            var result = getLoggedUser(httpContextAccessor);

            return (result, result.isValidExp);
        }
    }
}