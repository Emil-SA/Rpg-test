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

