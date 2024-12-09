using ProjectLottery.V1.Domain.Enums;

namespace ProjectLottery.V1.Domain.DTOs.Common
{
    public class DataResults<TEntity> where TEntity : new()
    {
        public int totalRecords
        {
            get; set;
        }
        public IEnumerable<TEntity>? results
        {
            get; set;
        }
        public bool error
        {
            get; set;
        }
        public string messageType
        {
            get; set;
        }
        public IEnumerable<string> messages
        {
            get; set;
        }
        public DataResults()
        {
            this.messageType = nameof(MessageTypeResultEnum.Success);
            this.error = false;
            this.messages = new List<string>();
            this.results = new List<TEntity>();
            this.totalRecords = 0;
        }
    }

    public class DataResult<TEntity> where TEntity : new()
    {
        public string id
        {
            get; set;
        }
        public TEntity? result
        {
            get; set;
        }
        public bool error
        {
            get; set;
        }
        public string messageType
        {
            get; set;
        }
        public IEnumerable<string> messages
        {
            get; set;
        }

        public DataResult()
        {
            this.messageType = nameof(MessageTypeResultEnum.Success);
            this.error = false;
            this.messages = new List<string>();
            this.id = string.Empty;
            this.result = new TEntity();
        }
    }

    public class ResultExecption
    {
        public bool error
        {
            get; set;
        } = true;
        public string messageType
        {
            get; set;
        } = nameof(MessageTypeResultEnum.Error);
        public IEnumerable<string> messages
        {
            get; set;
        }

    }

    public class ResultNotFound
    {
        public bool error
        {
            get; set;
        }
        public string messageType
        {
            get; set;
        }
        public IEnumerable<string> messages
        {
            get; set;
        }
    }

    public class ResultToken
    {
        public string messageType
        {
            get; set;
        }
        public bool error
        {
            get; set;
        }
        public IEnumerable<string> messages
        {
            get; set;
        }
        public string result
        {
            get; set;
        }
    }
}
