using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

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

        File.WriteAllText(filePath, updatedJSONString);
    }

    public static void RemoveItem(int itemId)
    {
        string filePath = Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataSources\menu.json");
        string JSONString = File.ReadAllText(filePath);

        List<Item> menu = JsonConvert.DeserializeObject<List<Item>>(JSONString) ?? new List<Item>();

        Item ? itemToRemove = menu.FirstOrDefault(i => i.Id == itemId);
        if (itemToRemove != null)
        {
            menu.Remove(itemToRemove);

            string updatedJSONString = JsonConvert.SerializeObject(menu, Formatting.Indented);

            File.WriteAllText(filePath, updatedJSONString);
            Console.WriteLine($"Item {itemToRemove.Name} has been removed from the menu");
            //Kan het niet beter string return maken en in de view de console.writeline doen?
            Thread.Sleep(3000);
        }
        else
        {
            Console.WriteLine($"Item {itemToRemove.Name} doesn't exist");
            //Kan het niet beter string return maken en in de view de console.writeline doen?
            Thread.Sleep(3000);
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