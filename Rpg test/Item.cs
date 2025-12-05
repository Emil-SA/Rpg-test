abstract class Item : IItem
{
    public string Name { get; protected set; }

    public Item(string name)
    {
        Name = name; 
    }
    public abstract void Apply(Character c);
    public virtual void ApplyPassive(Character c) { }
}

interface IItem
{
    void Apply(Character c);
    void ApplyPassive(Character c);
    string Name { get; }
}