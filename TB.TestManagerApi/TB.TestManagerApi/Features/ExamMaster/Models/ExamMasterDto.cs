using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TB.TestManagerApi.Domain
{
    public class ExamMasterDto : DomainBaseDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public ExamTypeMetaDto ExamType { get; set; }
    }
}
