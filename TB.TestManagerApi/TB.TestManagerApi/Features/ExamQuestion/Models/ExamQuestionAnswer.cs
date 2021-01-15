using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TB.TestManagerApi.Domain
{
    public class ExamQuestionAnswer
    {
        public Guid ExamQuestionAnswerXrefId { get; set; }
        public ExamQuestionMaster ExamQuestion { get; set; }
        public ExamAnswerMaster ExamAnswer { get; set; }
        public bool isCorrect { get; set; }
    }
}
