using System;
using System.Collections.Generic;

namespace TB.TestManagerApi.Domain
{
    public class CreateExamAnswer
    {
        public CreateExamAnswer()
        {
            ExamAnswers = new List<CreateExamAnswerMaster>();
        }
        public Guid ExamMasterId { get; set; }
        public Guid ExamQuestionMasterId { get; set; }
        public List<CreateExamAnswerMaster> ExamAnswers { get; set; }
    }
}
