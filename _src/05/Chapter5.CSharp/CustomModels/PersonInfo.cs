using System.Collections.Generic;

namespace Chapter5.CSharp.CustomModels
{
    public class PersonInfo
    {
        public int PersonId { get; set; }
        public string PersonType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<string> Phones { get; set; }
    }
}
