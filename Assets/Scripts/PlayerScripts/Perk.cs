
using System;

public class Perk
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public Action ApplyEffect { get; private set; }

    public Perk(string name, string description, Action effect)
    {
        Name = name;
        Description = description;
        ApplyEffect = effect;
    }
}
