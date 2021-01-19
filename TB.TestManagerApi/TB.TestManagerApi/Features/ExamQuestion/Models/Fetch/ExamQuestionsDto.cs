using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TB.TestManagerApi.Domain
{
    public class ExamQuestionsDto
    {
        public ExamQuestionsDto()
        {
            ExamQuestionCollection = new List<ExamQuestionAnswersXrefDto>();
        }

        public ExamMasterDto ExamMaster { get; set; }

        public List<ExamQuestionAnswersXrefDto> ExamQuestionCollection { get; set; }
       // public List<ExamQuestion> ExamQuestionCollection { get; set; }
    }
}
