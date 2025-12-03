using System;



class Character
{
    public String Name;
    public int Health;
    public int AttackPower;
    public int StartAttackPower;
    public int Armor;
    public int Resource;
    public int CritChance;
    public bool DidItCrit;
    public String ClassName;
    public List<string> Items = new();

    public Character(String name) => Name = name;

    public bool IsAlive = true;


    public void ItemCheck()
    {
        if (Items.Contains("Iron Sword"))
        {
            Items.Remove("Iron Sword");
            StartAttackPower += 2;
            CritChance += 5;
        }
        if (Items.Contains("Staff Of Fire"))
        {
            Resource++;
        }
    }

    public void Crit()
    {
        Random random = new Random();
        var RandomNumber = random.Next(0, 100);
        if (RandomNumber <= CritChance)
        {
            DidItCrit = true;
            _ = AttackPower * 2;
            Console.WriteLine($"{Name} crit ");
        } else
        {
            DidItCrit = false;
        }
    }
    public void DrinkHealingPotion()
    {
        if (Items.Contains("Healing Potion"))
        {
            Items.Remove("Healing Potion");
            Health += 5;
            Console.WriteLine($"{Name} used Healing Potion");
        }
    }
    public void TakeDamage(int amount)
    {
        Health -= amount;
        if (Health <= 10)
        {
            DrinkHealingPotion();
        }
        if (Health <= 0)
        {
            IsAlive = false;
        }
    }
    public void ResourceGain()
    {
        Resource ++;
    }
    public void PrintStats()
    {
        Console.WriteLine($"--- Name:    {Name} ");
        Console.WriteLine($"--- Class:   {ClassName} ");
        Console.WriteLine($"--- Health:  {Health} ");
        Console.WriteLine($"--- Items:   " + string.Join(", ", Items));
    }

    public virtual void PowerStrike(){ }
    public virtual void FireBall() { }
}
class Warrior : Character
{
    
    public Warrior(string name) : base(name)
    {
        ClassName = "Warrior";
        Health = 200;
        StartAttackPower = 3;
        AttackPower = StartAttackPower;
        Armor = 1;
        Resource = 0;
        CritChance = 10;
        Items.Add("Healing Potion");
        Items.Add("Iron Sword");

        
        
    }


    public override void PowerStrike()
    {
        if (Resource >= 2)
        {
            AttackPower += 1;
            Resource -= 2;
        }
        else if (DidItCrit == true)
        {
            AttackPower = StartAttackPower * 2;
        }
        else
        {
            AttackPower = StartAttackPower;
        }
    }
}

class Mage : Character
{
    public Mage(string name) : base(name)
    {
        ClassName = "Mage";
        Health = 150;
        StartAttackPower = 2;
        AttackPower = StartAttackPower;
        Armor = 1;   
        Resource = 0;
        CritChance = 20;
        Items.Add("Healing Potion");
        Items.Add("Staff Of Fire");

    }
    public override void FireBall()
    {
        if (Resource >= 4)
        {
            AttackPower += 10;
            Resource -= 4;
        } 
        else if (DidItCrit == true) 
        {
            AttackPower = StartAttackPower * 2;
        } 
        else
        {
            AttackPower = StartAttackPower;
        }
    }
}

class Battle
{
    Character c1;
    Character c2;
    public Battle(Character c1, Character c2)
    {
        this.c1 = c1;
        this.c2 = c2;
    }

    public void Start()
    {
        Thread.Sleep(2000);
        Console.Clear();
        c1.ItemCheck();
        c2.ItemCheck();
        c1.Crit();
        c2.Crit();
        c1.PowerStrike();
        c2.FireBall();
        c1.TakeDamage(c2.AttackPower - c1.Armor);
        c2.TakeDamage(c1.AttackPower - c2.Armor);
        c1.ResourceGain();
        c2.ResourceGain();
        Console.WriteLine($"{c1.Name}'s Health:{c1.Health} Resource {c1.Resource}");
        Console.WriteLine("vs");
        Console.WriteLine($"{c2.Name}'s Health:{c2.Health} Resource {c2.Resource}");
        if (c1.IsAlive == false)
        {
            Console.Clear();
            Console.WriteLine($"{c2.Name} Won");
        }
        else if (c2.IsAlive == false)
        {
            Console.Clear();
            Console.WriteLine($"{c1.Name} Won");
        } else
        {
            Start();
        }




    }
}

class Program
{
    static void Main()
    {
        Warrior character1 = new Warrior("Bjarne");
        Mage character2 = new Mage("Per");
        Battle fight = new Battle(character1, character2);
        

        character1.PrintStats();
        Console.WriteLine(" ");
        character2.PrintStats();
        fight.Start();

        
    }
}

