using System.Diagnostics.CodeAnalysis;

namespace WhereInParametersApp.Models;

public record Person
{
    public Person() { }

    [SetsRequiredMembers]
    public Person(string fName, string lName, string userName)
        => (First, Last, UserName)
            = (fName, lName, userName);

    public required string First { get; init; }
    public required string Last { get; init; }
    public required string UserName { get; init; }
    public override string ToString() => $"{First} {Last}";

}