using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TB.TestManagerApi.Domain
{
    public class ExamQuestionDto : DomainBaseDto
    {
        public Guid Id { get; set; }
        public Guid TestMasterId { get; set; }
        public ExamQuestionAnswerDto TestQuestionAnswer { get; set; }

    }
}
