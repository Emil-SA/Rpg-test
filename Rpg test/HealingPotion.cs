class HealingPotion : Item
{
    public HealingPotion() : base("Healing Potion") { }

    private int Charges;

    public override void Apply(Character c)
    {
        Console.WriteLine($"{c.Name} Equipped {Name}");
        Charges = 1;
    }
    public override void ApplyPassive(Character c)
    {
        if (c.Health <= 12 && Charges > 0)
        {
        Console.WriteLine($"{c.Name} used {Name}: +5 Health");
        c.Heal(5);
        Charges = Math.Max(0, Charges - 1);
        }
        
    }
}

