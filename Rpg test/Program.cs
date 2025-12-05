using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;



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
interface IItem
{
    void Apply(Character c);
    void ApplyPassive(Character c);
    string Name { get; }
}
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
            c1.ResetStats();
            c2.ResetStats();
            foreach (var item in c1.Items) item.ApplyPassive(c1);
            foreach (var item in c2.Items) item.ApplyPassive(c2);
            c1.UseAbility();
            c2.UseAbility();
            c1.TakeDamage(Math.Max(0,c2.CalculateDamage() - c1.Armor));
            c2.TakeDamage(Math.Max(0,c1.CalculateDamage() - c2.Armor));
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

