using System;

namespace ProjectLottery.V1.Helpers.Exceptions
{
    public class ExceptionCustom : Exception
    {
        public ExceptionCustom()
        {
        }

        public ExceptionCustom(string userInfo, Exception inner)
        : base(userInfo,  inner)
        {
        }
    }
}
