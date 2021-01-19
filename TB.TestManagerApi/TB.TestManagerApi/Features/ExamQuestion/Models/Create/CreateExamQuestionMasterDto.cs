using System;

namespace TB.TestManagerApi.Domain
{
    public class CreateExamQuestionMasterDto
    {
        public string Question { get; set; }
        public Guid ExamTypeSectionId { get; set; }
    }
}
