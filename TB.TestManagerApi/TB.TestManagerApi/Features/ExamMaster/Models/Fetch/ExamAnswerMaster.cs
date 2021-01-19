
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TB.TestManagerApi.Domain
{
    [Table("ExamAnswerMaster")]
    public class ExamAnswerMaster : DomainBase
    {
        [Key]
        public Guid Id { get; set; }
        public string Answer { get; set; }        
    }
}



