﻿using System;
using System.Collections.Generic;

namespace TB.TestManagerApi.Domain
{
    public class CreateExamQuestionDto
    {
        public CreateExamQuestionDto()
        {
            ExamAnswers = new List<CreateExamAnswerMasterDto>();
        }
        public Guid ExamMasterId { get; set; }

        public CreateExamQuestionMasterDto ExamQuestion { get; set; }

        public List<CreateExamAnswerMasterDto> ExamAnswers {get;set;}
    }
}
