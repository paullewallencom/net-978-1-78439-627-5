using System;
using System.Collections.Generic;

namespace Chapter5.CSharp.Models
{
    public class Person
    {
        public Person()
        {
            Phones = new HashSet<Phone>();
            Companies = new HashSet<Company>();
            Address = new Address();
        }

        public int PersonId { get; set; }
        public int? PersonTypeId { get; set; }
        public virtual PersonType PersonType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName);

            }
            set
            {
                var names = value.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                FirstName = names[0];
                LastName = names[1];
            }
        }
        public DateTime? BirthDate { get; set; }
        public decimal HeightInFeet { get; set; }
        public bool IsActive { get; set; }
        public PersonState PersonState { get; set; }
        public Address Address { get; set; }
        public byte[] Photo { get; set; }
        public byte[] FamilyPicture { get; set; }
        public virtual ICollection<Phone> Phones { get; set; }
        public virtual ICollection<Company> Companies { get; set; }

    }

}
