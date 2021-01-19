using System;

namespace TB.TestManagerApi.Domain
{
    public class CreateExamTypeSection
    {
        public Guid TestTypeMetaId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
    }
}