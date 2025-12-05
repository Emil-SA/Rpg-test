class StaffOfFire : Item
{
    public StaffOfFire() : base("Staff of Fire") { }
    public override void Apply(Character c)
    {
        
        Console.WriteLine($"{c.Name} Equipped {Name}");
    }
    public override void ApplyPassive(Character c)
    {
        Console.WriteLine($"{c.Name} gain 1 Resource from {Name}");
        c.ModifyResource(1);
    }
}

