using System.Diagnostics.CodeAnalysis;

namespace WhereInParametersApp.Models;

public record Person
{
    public Person() { }

    [SetsRequiredMembers]
    public Person(string firstName, string lastName, string userName)
        => (FirstName, LastName, UserName)
            = (firstName, lastName, userName);

    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string UserName { get; init; }
    public override string ToString() => $"{FirstName} {LastName}";

}