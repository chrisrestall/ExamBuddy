using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TB.TestManagerApi.Domain
{    
    public class CreateExamTypeMetaDto
    {
        public CreateExamTypeMetaDto()
        {
            TestTypeSections = new List<CreateExamTypeSectionDto>();
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public List<CreateExamTypeSectionDto> TestTypeSections { get; set; }
    }
}
