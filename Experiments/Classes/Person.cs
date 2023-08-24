using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Experiments.Classes;
internal class Person
{
    
    public int Id { get; set; }
    // create firstname and lastname properties
    public string FirstName { get; set; }
    public string LastName { get; set; }
    // create a fullname property that returns the firstname and lastname properties
    public string FullName => $"{FirstName} {LastName}";
    // create a constructor that accepts the firstname and lastname properties
    public Person(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
    // create a constructor that accepts the id, firstname and lastname properties
    public Person(int id, string firstName, string lastName)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
    }
    
}

// create a crud class for Person class
internal class PersonCrud
{
    // create a list of Person objects
    private static readonly List<Person> People = new();
    // create a method that adds a Person object to the list
    public static void Add(Person person)
    {
        People.Add(person);
    }
    // create a method that returns a Person object from the list
    public static Person Get(int id)
    {
        return People.FirstOrDefault(p => p.Id == id);
    }
    // create a method that returns a list of Person objects
    public static List<Person> GetAll()
    {
        return People;
    }
    // create a method that updates a Person object in the list
    public static void Update(Person person)
    {
        var index = People.FindIndex(p => p.Id == person.Id);
        People[index] = person;
    }
    // create a method that deletes a Person object from the list
    public static void Delete(int id)
    {
        var index = People.FindIndex(p => p.Id == id);
        People.RemoveAt(index);
    }
}
