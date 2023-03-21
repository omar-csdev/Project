using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Diagnostics;
using Menu_item_creëren;

public static class FoodMenu
{
    public static void Start() 
    {
        Console.WriteLine("Wat wilt u doen?");
        Console.WriteLine("1. Voeg gerecht toe");
        Console.WriteLine("2. Verwijder gerecht");
        Console.WriteLine("3. Toon alle gerechten");
        Console.WriteLine("4.Ga terug");

        string input = Console.ReadLine();
        if (input == "1") 
        {
        AddItem.Start();
        }
        else if (input == "2") { Console.WriteLine("Optie nog niet beschikbaar"); }
        else if (input == "3") { MenuItem.start(); }
        else if (input == "4") { MainMenu.Start(); }
        else { Console.WriteLine("Geef een valide optie"); FoodMenu.Start(); }
    }

}
