using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TB.TestManagerApi.Domain
{
    public class ExamQuestionMasterDto : DomainBaseDto
    {
        public Guid Id { get; set; }
        public string Question { get; set; }
        public ExamTypeSectionDto ExamTypeSection { get; set; }

        public override int GetHashCode()
        {
            if (Id == Guid.Empty) return 0;
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            ExamQuestionMasterDto other = obj as ExamQuestionMasterDto;
            return other != null && other.Id == this.Id;
        }
    }
}
