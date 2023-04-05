using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

static class AddItem
{
    public static void Start()
    {
        Console.WriteLine("Adding item.....");
        Console.WriteLine("Would you like to proceed (y/n)");
        string input = Console.ReadLine();
        if (input.ToLower() == "y")
        {
            Console.WriteLine("Add name of the dish/beverage");
            string name = Console.ReadLine();
            Console.WriteLine("Add price of the dish/beverage in euros");
            double prijs = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Add Description (if no discription wanted, click enter");
            string beschrijving = Console.ReadLine();
            if (string.IsNullOrEmpty(beschrijving))
            {
                beschrijving = "-";
            }
            Console.WriteLine($"Items zijn toegevoegd. Nummer(count van aantal gerechten+ 1 geven), Naam: {name} , Prijs: {prijs}, Beschrijving: {beschrijving} ");
            // maak een overschrijving van de info naar een json file. Anders kan de informatie niet overgezet worden naar de tabel.
        }
    }
