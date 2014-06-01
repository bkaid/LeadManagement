using System;

namespace LeadManagement.Model.Domain
{
    public class Lead
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Occupation { get; set; }

        /// <summary>
        /// Current user who has the lead locked.
        /// </summary>
        public string LockedBy { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsProcessed { get; set; }
    }
}
