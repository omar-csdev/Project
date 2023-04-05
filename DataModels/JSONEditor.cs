using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using Console = Colorful.Console;

public class JSONEditor
{
    public static void AddItem(Item item)
    {
        string filePath = Path.Combine(Environment.CurrentDirectory, @".\DataSources\menu.json");
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

    public static bool RemoveItem(int itemId)
    {
        string filePath = Path.Combine(Environment.CurrentDirectory, @".\DataSources\menu.json");
        string JSONString = File.ReadAllText(filePath);

        List<Item> menu = JsonConvert.DeserializeObject<List<Item>>(JSONString) ?? new List<Item>();

        Item ? itemToRemove = menu.FirstOrDefault(i => i.Id == itemId);
        if (itemToRemove != null)
        {
            menu.Remove(itemToRemove);

            string updatedJSONString = JsonConvert.SerializeObject(menu, Formatting.Indented);

            File.WriteAllText(filePath, updatedJSONString);
            return true;
            //Kan het niet beter string return maken en in de view de console.writeline doen?
        }
        else
        {
            //Kan het niet beter string return maken en in de view de console.writeline doen?
            return false;
        }
    }

    //Gets an item as parameter with updated values and updates the item in the JSON file with the same ID
    public static bool UpdateItem(int ItemID, Item item)
    {
        string filePath = Path.Combine(Environment.CurrentDirectory, @".\DataSources\menu.json");
        string JSONString = File.ReadAllText(filePath);

        List<Item> menu = JsonConvert.DeserializeObject<List<Item>>(JSONString) ?? new List<Item>();

        Item ? itemToUpdate = menu.FirstOrDefault(i => i.Id == ItemID);
        if (itemToUpdate != null)
        {
            itemToUpdate.Name = item.Name;
            itemToUpdate.Price = item.Price;
            itemToUpdate.Category = item.Category;
            string updatedJSONString = JsonConvert.SerializeObject(menu, Formatting.Indented);
            File.WriteAllText(filePath, updatedJSONString);
            return true;
        }
        else
        {
            return false;
        }
    }
}