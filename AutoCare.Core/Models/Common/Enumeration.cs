using System.Reflection;
using System.Text.Json.Serialization;

namespace AutoCare.Core.Models.Common;

public abstract class Enumeration : IComparable
{
    public string Name { get; private set; }
    public int Id { get; private set; }
    [JsonIgnore]
    public char Description { get; private set; }

    protected Enumeration(int id, string name, char description)
    {
        Id = id;
        Name = name;
        Description = description;
    }

    public Enumeration()
    {
    }

    public override string ToString() => Name;

    public static IEnumerable<T> GetAll<T>() where T : Enumeration
    {
        var fields = typeof(T).GetFields(BindingFlags.Public |
                                         BindingFlags.Static |
                                         BindingFlags.DeclaredOnly);

        return fields.Select(f => f.GetValue(new object())).Cast<T>();
    }

    public override bool Equals(object obj)
    {
        var otherValue = obj as Enumeration;

        if (otherValue == null)
            return false;

        var typeMatches = GetType().Equals(obj.GetType());
        var valueMatches = Id.Equals(otherValue.Id);

        return typeMatches && valueMatches;
    }

    public int CompareTo(object other) => Id.CompareTo(((Enumeration)other).Id);

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Id, Description);
    }

}
