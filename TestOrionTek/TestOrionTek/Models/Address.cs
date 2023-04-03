using System;
using System.Collections.Generic;
using System.Text;

namespace TestOrionTek.Models
{
    public class Address
    {
        public Guid Id { get; set; }
        public string AddressName { get; set; }

        public Guid EmployeesId { get; set; }
        public virtual Employees Employees { get; set; }
    }
}
