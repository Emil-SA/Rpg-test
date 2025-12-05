class Warrior : Character
{
    
    public Warrior(string name) : base(name)
    {
        ClassName = "Warrior";
        Health = 20;
        BaseAttackPower = 3;
        AttackPower = BaseAttackPower;
        Armor = 1;
        Resource = 0;
        CritChance = 10;
        Items.Add(new HealingPotion());
        Items.Add(new IronSword());

        
        
    }

    public override void UseAbility() => PowerStrike();
    public void PowerStrike()
    {
        if (Resource >= 2)
        {
            AttackPower += 1;
            Resource -= 2;            
        }
    }
}

