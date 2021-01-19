using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TB.TestManagerApi.Domain
{
    public class ExamQuestionAnswerDto : DomainBaseDto
    {         
            public Guid Id { get; set; }
            public ExamQuestionMasterDto ExamQuestion { get; set; }
            public ExamAnswerMasterDto ExamAnswer { get; set; }
            public bool IsCorrect { get; set; }

         

    }
}
