using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TB.TestManagerApi.Domain
{
    [Table("ExamQuestionAnswerXref")]
    public class ExamQuestionAnswerXref
    {
        [Key]
        public Guid Id { get; set; }
        public Guid TestQuestionId { get; set; }
        public Guid TestAnswerId { get; set; }
        public bool isCorrect { get; set; }
    }
}
