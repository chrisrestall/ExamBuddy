using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TB.TestManagerApi.Domain
{
    public class DomainBaseDto
    {
        public bool Active { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EditDate { get; set; }

    }
}
