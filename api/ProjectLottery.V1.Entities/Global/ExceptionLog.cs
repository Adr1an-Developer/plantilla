using ProjectLottery.V1.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLottery.V1.Entities.Global
{
    [Table("exception_log")]
    public class ExceptionLog : AuditEntityNotUserFields
    {
        [Column("id")]
        public int Id
        {
            get; set;
        } = 0;
        [Column("log_level")]
        public string LogLevel
        {
            get; set;
        } // e.g., "Error", "Warning"
        [Column("message")]
        public string Message
        {
            get; set;
        }
        [Column("stack_trace")]
        public string StackTrace
        {
            get; set;
        }
        [Column("user_id")]
        public string UserId
        {
            get; set;
        }
        [Column("service_name")]
        public string ServiceName
        {
            get; set;
        }
    }
}
