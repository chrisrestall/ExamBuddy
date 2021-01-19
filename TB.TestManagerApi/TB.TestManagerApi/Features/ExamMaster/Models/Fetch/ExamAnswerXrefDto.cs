using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TB.TestManagerApi.Domain
{
    public class ExamAnswerXrefDto
    {
        public Guid Id { get; set; }
        public Guid XrefId { get; set; }
        public string Answer { get; set; }
        public bool isCorrect {get;set;}
    }
}
