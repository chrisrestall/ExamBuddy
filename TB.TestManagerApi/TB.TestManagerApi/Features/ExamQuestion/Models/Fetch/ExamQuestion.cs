using Dapper.Contrib.Extensions;
using System;

namespace TB.TestManagerApi.Domain
{
    [Table("ExamQuestions")]
    public class ExamQuestion : DomainBase
    {
        [Key]
        public Guid Id { get; set; }      
        public Guid TestMasterId { get; set; }
        public ExamQuestionAnswerXref ExamQuestionAnswerXref { get;set;}
        public ExamAnswerMaster ExamAnswerMaster { get; set; }
        public ExamQuestionMaster ExamQuestionMaster { get; set; }
    }
}
