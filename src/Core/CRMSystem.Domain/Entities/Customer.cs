using System;

namespace CRMSystem.Domain.Entities;

public class Customer : BaseEntity<Customer>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public Customer SetDetails(string phoneNumber, string email,string name, string surname)
    {
        Email = email;
        PhoneNumber = phoneNumber;
        Name = name;
        Surname = surname;

        return this;
    }
}
