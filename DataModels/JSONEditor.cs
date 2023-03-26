using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

public class JSONEditor
{
    public static void AddItem(string fileName, Item item)
    {
        string JSONString = File.ReadAllText(fileName);
        List<Item> menu = JsonConvert.DeserializeObject<List<Item>>(JSONString) ?? new List<Item>();

        Item newItem;
        if (item is Food)
        {
            Food food = (Food)item;
            newItem = new Food(food.Name, food.Price, food.Category);
        }
        else if (item is Drink)
        {
            Drink drink = (Drink)item;
            newItem = new Drink(drink.Name, drink.Price, drink.Category);
        }
        else
        {
            throw new ArgumentException("Invalid item type.");
        }

        newItem.Id = menu.Count > 0 ? menu.Max(i => i.Id) + 1 : 1; 

        menu.Add(newItem);

        string updatedJSONString = JsonConvert.SerializeObject(menu, Formatting.Indented);

        File.WriteAllText(fileName, updatedJSONString);
    }
}
