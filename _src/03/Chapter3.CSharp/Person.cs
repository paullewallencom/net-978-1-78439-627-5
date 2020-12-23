

using System;
using System.Collections.Generic;

namespace Chapter3.CSharp
{
    public class Person
    {
public Person()
{
    Phones = new HashSet<Phone>();
}
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime? BirthDate { get; set; }
        public decimal HeightInFeet { get; set; }
        public byte[] Photo { get; set; }
        public bool IsActive { get; set; }
        public int NumberOfCars { get; set; }
        public virtual ICollection<Phone> Phones { get; set; }
    }

    //public class Person
    //{
    //    public int PersonId { get; set; }

    //    [MaxLength(30, ErrorMessage = "First name cannot be longer than 30")]
    //    public string FirstName { get; set; }

    //    [MaxLength(30)]
    //    public string LastName { get; set; }

    //    [StringLength(1, MinimumLength = 1)]
    //    [Column(TypeName = "char")]
    //    public string MiddleName { get; set; }
    //}
}
