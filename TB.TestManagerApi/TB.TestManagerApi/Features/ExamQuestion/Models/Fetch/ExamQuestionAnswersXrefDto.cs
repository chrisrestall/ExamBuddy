using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TB.TestManagerApi.Domain
{
    public class ExamQuestionAnswersXrefDto
    {
        public ExamQuestionMasterDto ExamQuestion { get;set;}
        public List<ExamAnswerXrefDto> ExamQuestionAnswers { get; set; }

    }
}
