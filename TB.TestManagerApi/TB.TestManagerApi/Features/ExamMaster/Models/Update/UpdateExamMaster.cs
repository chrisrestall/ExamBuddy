using System;

namespace TB.TestManagerApi.Domain
{
    public class UpdateExamMaster
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public bool Active { get; set; }
        public DateTime EditDate { get; set; } = DateTime.Now;
        public Guid TestTypeId { get; set; }
    }
}
