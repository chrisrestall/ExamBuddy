using System;


namespace TB.TestManagerApi.Domain
{
    public class UpdateExamQuestionMasterDto
    {
        public Guid Id { get; set; }
        public string Question { get; set; }
        public Guid ExamTypeSectionId { get; set; }
        public DateTime EditDate { get; set; } = DateTime.Now;
    }
}
