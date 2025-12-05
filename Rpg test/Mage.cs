class Mage : Character
{
    public Mage(string name) : base(name)
    {
        ClassName = "Mage";
        Health = 15;
        BaseAttackPower = 2;
        AttackPower = BaseAttackPower;
        Armor = 1;   
        Resource = 0;
        CritChance = 20;
        Items.Add(new HealingPotion());
        Items.Add(new StaffOfFire());

    }
    public override void UseAbility() => FireBall();
    public  void FireBall()
    {
        if (Resource >= 4)
        {
            AttackPower += 10;
            Resource -= 4;
        } 
    }
}

