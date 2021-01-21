using System;
using System.Collections.Generic;

namespace TB.TestManagerApi.Domain
{
    public class CreateExamAnswerDto
    {
        public CreateExamAnswerDto()
        {
            ExamAnswers = new List<CreateExamAnswerMasterDto>();
        }
        public Guid ExamMasterId { get; set; }
        public Guid ExamQuestionMasterId { get; set; }
        public List<CreateExamAnswerMasterDto> ExamAnswers { get; set; }
    }
}
