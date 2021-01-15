using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TB.TestManagerApi.Domain
{
    public class ExamTypeMetaDto  : DomainBaseDto
    {
        public ExamTypeMetaDto()
        {
            TestTypeSections = new List<ExamTypeSectionDto>();
        }       
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ExamTypeSectionDto> TestTypeSections { get; set; }
    }
}
