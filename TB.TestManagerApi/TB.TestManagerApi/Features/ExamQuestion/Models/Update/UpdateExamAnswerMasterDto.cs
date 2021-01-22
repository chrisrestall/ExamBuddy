using System;

namespace TB.TestManagerApi.Domain
{
    public class UpdateExamAnswerMasterDto
    {
        public Guid Id { get; set; }
        public string Answer { get; set; }
        public UpdateExamAnswerQuestionXrefCorrectDto ExamAnswerQuestionXrefCorrect { get; set; }
        public DateTime EditDate { get; set; } = DateTime.Now;
    }
}
