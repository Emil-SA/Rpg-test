using System;
using System.Security.Cryptography.X509Certificates;



class Character
{
    public String Name;
    public int Health;
    public int AttackPower;
    public int BaseAttackPower;
    public int Armor;
    public int Resource;
    public int CritChance;
    public bool DidItCrit;
    public String ClassName;
    public List<string> Items = new();

    public static Random RandomNumberGenerator = new Random();
    public Character(String name) => Name = name;

    public bool IsAlive => Health > 0;


    public void ApplyItems()
    {
        if (Items.Contains("Iron Sword"))
        {
            Items.Remove("Iron Sword");
            BaseAttackPower += 1;
            CritChance += 5;
        }
    }
    public void PassiveItems()
    {
        if (Items.Contains("Staff Of Fire"))
        {
            Resource++;
        }
        if (Health <= 10)
        {
            DrinkHealingPotion();
        }
    }

    public void Crit()
    {
        var RandomNumber = RandomNumberGenerator.Next(0, 100);
        if (RandomNumber <= CritChance)
        {
            DidItCrit = true;
            AttackPower = BaseAttackPower * 2;
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
        Health = 20;
        BaseAttackPower = 3;
        AttackPower = BaseAttackPower;
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
            if (DidItCrit == true)
            {
                AttackPower += 1;
            }
        }
    }
}

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
        Items.Add("Healing Potion");
        Items.Add("Staff Of Fire");

    }
    public override void FireBall()
    {
        if (Resource >= 4)
        {
            AttackPower += 10;
            Resource -= 4;
            if (DidItCrit == true)
            {
                AttackPower += 10;
            }
        } 
    }
}

class Battle
{
    public int Rounds = 0;
    Character c1;
    Character c2;
    public Battle(Character c1, Character c2)
    {
        this.c1 = c1;
        this.c2 = c2;
    }

    public void Start()
    {
        Console.Clear();
        c1.ApplyItems();
        c2.ApplyItems();

        while (c1.IsAlive && c2.IsAlive)
        {
            Console.WriteLine("--------------------");
            Console.WriteLine("");
            Rounds ++;
            Console.WriteLine($"-----Round{Rounds}-----");
            Thread.Sleep(4000);
            c1.AttackPower = c1.BaseAttackPower;
            c2.AttackPower = c2.BaseAttackPower;
            c1.PassiveItems();
            c2.PassiveItems();
            c1.Crit();
            c2.Crit();
            c1.PowerStrike();
            c2.FireBall();
            c1.TakeDamage(Math.Max(0,c2.AttackPower - c1.Armor));
            c2.TakeDamage(Math.Max(0,c1.AttackPower - c2.Armor));
            c1.ResourceGain();
            c2.ResourceGain();
            Console.WriteLine($"{c1.Name}'s Health:{c1.Health} Resource {c1.Resource}, Ap {c1.AttackPower}");
            Console.WriteLine("vs");
            Console.WriteLine($"{c2.Name}'s Health:{c2.Health} Resource {c2.Resource}, Ap {c2.AttackPower}");
            if ( !c1.IsAlive && !c2.IsAlive )
            {
                Console.WriteLine("Both are dead");
            }
            else if (!c1.IsAlive)
            {
             Console.WriteLine($"{c2.Name} Won");
            }
             else if (!c2.IsAlive)
             {
             Console.WriteLine($"{c1.Name} Won");
             }
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
        Thread.Sleep(4000);
        fight.Start();

        
    }
}

