namespace ProjectLottery.V1.Domain.DTOs.Auth
{
    public class Tokens
    {
        public string Token
        {
            get; set;
        }

        public string RefreshToken
        {
            get; set;
        }

        public string? AccessToken
        {
            get; set;
        }
    }
}