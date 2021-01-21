using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TB.TestManagerApi.Domain
{
    public class UpdateExamQuestionMaster
    {
        public Guid Id { get; set; }
        public string Question { get; set; }
        public Guid ExamTypeSectionId { get; set; }

        public DateTime EditDate { get; set; } = DateTime.Now;
    }
}
