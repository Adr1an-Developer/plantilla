namespace ProjectLottery.V1.Helpers.DTOs
{
    public class LoggedOutput
    {
        public string UserName
        {
            get; set;
        } = string.Empty;

        public string Name
        {
            get; set;
        }

        public string UserID
        {
            get; set;
        }

        public string Profile
        {
            get; set;
        }

        public string Email
        {
            get; set;
        }

        public string Fullname
        {
            get; set;
        }

        public bool isValidExp { get; set; } = false;
    }
}