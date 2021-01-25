using System;

namespace TB.TestManagerApi.Domain
{
    public class DeactivateQuestionMasterDto
    {
        public Guid Id { get; set; }
        public DateTime EditDate { get; set; } = DateTime.Now;

    }
}
