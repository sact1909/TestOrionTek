using Newtonsoft.Json;
using PropertyChanged;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestOrionTek.DatabaseSettings.Entities
{
    public class Employees
    {
        [PrimaryKey]
        public Guid ID { get; set; } = Guid.NewGuid();
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get => $"{FirstName} {LastName}";}
        public string Phone { get; set; }
        public string CompanyID { get; set; }
        public string Position { get; set; }

        [Ignore]
        public List<string> Address { get; set; }

        public string AddressJson
        {
            get { return JsonConvert.SerializeObject(Address); }
            set { Address = JsonConvert.DeserializeObject<List<string>>(value); }
        }
    }
}
