using System.Collections.Generic;

namespace Chapter5.CSharp.Models
{
    public class PersonType
    {
        public int PersonTypeId { get; set; }
        public string TypeName { get; set; }
        public ICollection<Person> Persons { get; set; }
    }
}
