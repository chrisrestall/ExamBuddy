using System;
using System.Collections.Generic;

namespace TB.TestManagerApi.Domain
{
    public class CreateExamQuestion
    {
        public Guid ExamMasterId { get; set; }

        public CreateExamQuestionMaster ExamQuestion { get; set; }

        public List<CreateExamAnswerMaster> ExamAnswers { get; set; }
    }
}
