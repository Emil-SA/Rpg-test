using System;


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

