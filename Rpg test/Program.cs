using System;


class Character
{
    public String Name;
    public int Health;
    public int AttackPower;
    public int Resource;
    public String ClassName;

    public Character(String name) => Name = name;

    public bool IsAlive = true;
    public void TakeDamage(int amount)
    {
        Health -= amount;
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
        AttackPower = 2;
        Resource = 0;
        
    }

    public override void PowerStrike()
    {
        if (Resource == 2)
        {
            AttackPower += 1;
            Resource = 0;
        }
        else
        {
            AttackPower = 2;
        }
    }
}

class Mage : Character
{
    public Mage(string name) : base(name)
    {
        ClassName = "Mage";
        Health = 15;
        AttackPower = 1;
    }
    public override void FireBall()
    {
        if (Resource == 4)
        {
            AttackPower += 10;
            Resource = 0;
        } else
        {
            AttackPower = 1;
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
        c1.PowerStrike();
        c2.FireBall();
        c1.TakeDamage(c2.AttackPower);
        c2.TakeDamage(c1.AttackPower);
        c1.ResourceGain();
        c2.ResourceGain();
        Console.WriteLine($"{c1.Name}'s Health:{c1.Health}");
        Console.WriteLine("vs");
        Console.WriteLine($"{c2.Name}'s Health:{c2.Health}");
        if (c1.IsAlive == false)
        {
            Console.WriteLine($"{c2.Name} Won");
        }
        else if (c2.IsAlive == false)
        {
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

