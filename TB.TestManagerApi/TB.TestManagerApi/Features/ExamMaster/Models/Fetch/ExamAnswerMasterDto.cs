using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TB.TestManagerApi.Domain
{
    public class ExamAnswerMasterDto : DomainBaseDto
    {
        public Guid Id { get; set; }
        public string Answer { get; set; }
    }
}
