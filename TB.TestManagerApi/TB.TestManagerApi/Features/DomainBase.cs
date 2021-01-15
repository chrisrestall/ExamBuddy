using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TB.TestManagerApi.Domain
{
    public class DomainBase
    {
        public DomainBase()
        {
            EditDate = CreateDate;
        }
        public bool Active { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime EditDate { get; set; }
    }
}
