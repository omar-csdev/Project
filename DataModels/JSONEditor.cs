using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

public class JSONEditor
{
    public static void AddItem(Item item)
    {
        string filePath = Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataSources\menu.json");
        string JSONString = File.ReadAllText(filePath);

        List<Item> menu = JsonConvert.DeserializeObject<List<Item>>(JSONString) ?? new List<Item>();

        Item newItem;
        if (item is Food)
        {
            Food food = (Food)item;
            newItem = new Food(food.Name, food.Price, food.Type);
        }
        else if (item is Drink)
        {
            Drink drink = (Drink)item;
            newItem = new Drink(drink.Name, drink.Price, drink.Type);
        }
        else
        {
            throw new ArgumentException("Invalid item type.");
        }

        newItem.Id = menu.Count > 0 ? menu.Max(i => i.Id) + 1 : 1;

        menu.Add(newItem);

        string updatedJSONString = JsonConvert.SerializeObject(menu, Formatting.Indented);

        File.WriteAllText(filePath, updatedJSONString);
    }
}
