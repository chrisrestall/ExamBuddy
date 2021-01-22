using System;


namespace TB.TestManagerApi.Domain
{
    public class UpdateExamAnswerMaster
    {
        public Guid Id { get; set; }
        public string Answer { get; set; }
        public UpdateExamAnswerQuestionXrefCorrect ExamAnswerQuestionXrefCorrect { get; set; }
        public DateTime EditDate { get; set; } = DateTime.Now;
    }
}
