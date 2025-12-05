class IronSword : Item
{
    public IronSword() : base("Iron Sword") { }
    public override void Apply(Character c)
    {
        c.ModifyBaseAttackPower(1);
        c.ModifyCritChance(5);
        Console.WriteLine($"{c.Name} Equipped {Name}: +1 Attack, +5% Crit");
    }
}

