﻿using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TB.TestManagerApi.Domain
{
    [Table("ExamMaster")]
    public class ExamMaster : DomainBase
    {
        [Key]
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public Guid TestTypeId { get; set; }
      
    }
}
