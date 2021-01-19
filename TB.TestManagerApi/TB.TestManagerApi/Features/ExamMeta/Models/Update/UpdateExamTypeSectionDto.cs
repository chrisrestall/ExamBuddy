using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TB.TestManagerApi.Domain
{
    public class UpdateExamTypeSectionDto
    {
        public Guid Id { get; set; }
        public Guid TestTypeMetaId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public DateTime EditDate { get; set; } = DateTime.Now;
    }
}
