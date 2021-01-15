using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TB.TestManagerApi.Domain
{
    public class DeactivateExamTypeSectionDto
    {
        public Guid Id { get; set; }
        public DateTime EditDate { get; set; } = DateTime.Now;
    }
}
