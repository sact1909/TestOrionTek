using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestOrionTek.Models
{
    public class Employees
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [JsonIgnore]
        public string FullName { get => $"{FirstName} {LastName}"; }
        public string Phone { get; set; }
        public string CompanyID { get; set; }
        public string Position { get; set; }
        public virtual List<Address> Address { get; set; } = new List<Address>();

    }
}
