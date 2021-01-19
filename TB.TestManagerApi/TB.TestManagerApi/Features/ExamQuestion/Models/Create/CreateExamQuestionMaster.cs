using System;

namespace TB.TestManagerApi.Domain
{
    public class CreateExamQuestionMaster
    {
        public string Question { get; set; }
        public Guid ExamTypeSectionId { get; set; }
    }
}
