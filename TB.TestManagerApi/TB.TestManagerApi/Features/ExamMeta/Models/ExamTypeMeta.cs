using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper.Contrib;
using Dapper;
using Dapper.Contrib.Extensions;

namespace TB.TestManagerApi.Domain
{
    [Table("ExamTypeMeta")]
    public class ExamTypeMeta : DomainBase
    {
        public ExamTypeMeta()
        {
            TestTypeSections = new List<ExamTypeSection>();           
        }
        [Key]        
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }      
        public List<ExamTypeSection> TestTypeSections { get; set; }

    }
}
