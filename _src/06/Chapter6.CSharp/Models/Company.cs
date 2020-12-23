
using System;
using System.Collections.Generic;

namespace Chapter6.CSharp.Models
{
    public class Company
    {
        public Company()
        {
            Persons = new HashSet<Person>();
        }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Person> Persons { get; set; }
    }
}
