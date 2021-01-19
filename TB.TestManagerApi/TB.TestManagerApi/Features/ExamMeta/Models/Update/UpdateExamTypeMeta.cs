using System;
using System.Collections.Generic;

namespace TB.TestManagerApi.Domain
{
    public class UpdateExamTypeMeta
    {
        public UpdateExamTypeMeta()
        {
            TestTypeSections = new List<UpdateExamTypeSection>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public DateTime EditDate { get; set; } = DateTime.Now;
        public List<UpdateExamTypeSection> TestTypeSections { get; set; }
    }
}
