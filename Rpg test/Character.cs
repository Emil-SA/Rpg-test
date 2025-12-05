class Character
{
    public String Name { get; }
    public int Health { get; protected set; }
    public int AttackPower { get; protected set; }
    public int BaseAttackPower { get; protected set; }
    public int Armor {  get; protected set; }
    public int Resource { get; protected set; }
    public int CritChance { get; protected set; }
    public String ClassName;
    public List<IItem> Items = new();

    public static Random RandomNumberGenerator = new Random();
    public Character(String name) => Name = name;

    public bool IsAlive => Health > 0;
    public void ModifyBaseAttackPower(int amount) => BaseAttackPower += amount;
    public void ModifyCritChance(int amount) => CritChance += amount;
    public void ModifyResource(int amount) => Resource += amount;

    public void ApplyItems()
    {
        foreach (var item in Items)
        {
            item.Apply(this);
            item.ApplyPassive(this);
        }
    }
    public void ResetStats()
    {
        AttackPower = BaseAttackPower;
    }
    public bool RollCrit()
    {
        return RandomNumberGenerator.Next(1, 101) <= CritChance;
    }
    public int CalculateDamage()
    {
        int ap = AttackPower;
        if (RollCrit()) 
        { 
            ap *= 2;
            Console.WriteLine($"{Name} crit ");
        }
        return ap;
    }


    public void TakeDamage(int amount)
    {
        Health = Math.Max(0,Health - amount);
    }
    public void ResourceGain()
    {
        Resource ++;
    }
    public void Heal(int amount)
    {
        Health += amount;
    }
    public void PrintStats()
    {
        Console.WriteLine($"--- Name:    {Name} ");
        Console.WriteLine($"--- Class:   {ClassName} ");
        Console.WriteLine($"--- Health:  {Health} ");
    }

    public virtual void UseAbility() { }
    //public virtual void PowerStrike(){ }
    //public virtual void FireBall() { }
}

