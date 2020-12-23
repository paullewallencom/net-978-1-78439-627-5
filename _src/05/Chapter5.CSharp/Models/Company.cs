
using System.Collections.Generic;

namespace Chapter5.CSharp.Models
{
public class Company
{
    public Company()
    {
        Persons = new HashSet<Person>();
        Address = new Address();
    }
    public int CompanyId { get; set; }
    public string CompanyName { get; set; }
    public Address Address { get; set; }
    public ICollection<Person> Persons { get; set; }
}
}
