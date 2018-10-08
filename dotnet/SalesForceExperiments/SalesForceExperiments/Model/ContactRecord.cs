using System;

namespace SalesForceExperiments.Model
{
    public class ContactRecord
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string AccountId { get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public Attributes Attributes { get; set; }
    }
}
