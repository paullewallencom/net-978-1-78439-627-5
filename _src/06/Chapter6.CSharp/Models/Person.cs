﻿using System;
using System.Collections.Generic;

namespace Chapter6.CSharp.Models
{
    public class Person
    {
        public Person()
        {
            Phones = new HashSet<Phone>();
            Companies = new HashSet<Company>();
        }
        public int PersonId { get; set; }
        public int? PersonTypeId { get; set; }
        public virtual PersonType PersonType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public decimal HeightInFeet { get; set; }
        public byte[] RowVersion { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<Phone> Phones { get; set; }
        public ICollection<Company> Companies { get; set; }
    }

}
