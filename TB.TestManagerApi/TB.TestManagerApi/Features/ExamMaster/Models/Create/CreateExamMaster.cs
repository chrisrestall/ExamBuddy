using System;

namespace TB.TestManagerApi.Domain
{
    public class CreateExamMaster
    {
        public string Name { get; set; }
        public string UserId { get; set; }
        public Guid TestTypeId { get; set; }
    }
}
