using System;
using System.Collections.Generic;
using System.Linq;

public class Role {
    public static Role Author { get; } = new Role(0, "Author");
    public static Role Editor { get; } = new Role(1, "Editor");
    public static Role Administrator { get; } = new Role(2, "Administrator");
    public static Role SalesRep { get; } = new Role(3, "Sales Representative");

    private Role(int val, string name)
    {
        Value = val;
        Name = name;
    }

    private Role()
    {
        // required for EF
    }

    public int Value { get; private set; }
    public string Name { get; private set; }

    public IEnumerable<Role> List()
    {
        return new[] { Author, Editor, Administrator, SalesRep };
    }

    public Role FromString(string roleString)
    {
        return List().FirstOrDefault(r => String.Equals(r.Name, roleString, StringComparison.OrdinalIgnoreCase));
    }

    public Role FromValue(int value)
    {
        return List().FirstOrDefault(r => r.Value == value);
    }
}