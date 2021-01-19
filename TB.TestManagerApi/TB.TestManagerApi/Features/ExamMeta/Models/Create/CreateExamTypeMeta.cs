using System;
using System.Collections.Generic;

namespace TB.TestManagerApi.Domain
{
    public class CreateExamTypeMeta
    {
        public CreateExamTypeMeta()
        {
            TestTypeSections = new List<CreateExamTypeSection>();
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<CreateExamTypeSection> TestTypeSections { get; set; }
    }
}