using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TB.TestManagerApi.Domain
{
    [Table("ExamQuestionMaster")]
    public class ExamQuestionMaster : DomainBase
    {
        [Key]
        public Guid Id { get; set; }
        public string Question { get; set; }
        public Guid TestTypeSectionId { get; set; }
    }
}
