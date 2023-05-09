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
        else if (item is Halal)
        {
            Halal halal = (Halal)item;
            newItem = new Halal(halal.Name, halal.Price, halal.Type);
        }
        else if (item is Vegan)
        {
            Vegan vegan = (Vegan)item;
            newItem = new Vegan(vegan.Name, vegan.Price, vegan.Type);
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
        string filePath = Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataSources\menu.json");
        string JSONString = File.ReadAllText(filePath);

        List<Item> menu = JsonConvert.DeserializeObject<List<Item>>(JSONString) ?? new List<Item>();

        Item? itemToRemove = menu.FirstOrDefault(i => i.Id == itemId);
        if (itemToRemove != null)
        {
            menu.Remove(itemToRemove);

            string updatedJSONString = JsonConvert.SerializeObject(menu, Formatting.Indented);

            File.WriteAllText(filePath, updatedJSONString);
            return true;
        }
        else
        {
            return false;
        }
    }

    //Gets an item as parameter with updated values and updates the item in the JSON file with the same ID
    public static bool UpdateItem(int itemId, Item updatedItem)
    {
        string filePath = Path.Combine(Environment.CurrentDirectory, @"..\..\..\DataSources\menu.json");
        string jsonString = File.ReadAllText(filePath);

        List<Item> menu = JsonConvert.DeserializeObject<List<Item>>(jsonString) ?? new List<Item>();

        Item itemToUpdate = menu.FirstOrDefault(i => i.Id == itemId);
        if (itemToUpdate != null)
        {
            if (updatedItem is Food)
            {
                Food food = (Food)updatedItem;
                ((Food)itemToUpdate).Name = food.Name;
                ((Food)itemToUpdate).Price = food.Price;
                ((Food)itemToUpdate).Type = food.Type;
            }
            else if (updatedItem is Drink)
            {
                Drink drink = (Drink)updatedItem;
                ((Drink)itemToUpdate).Name = drink.Name;
                ((Drink)itemToUpdate).Price = drink.Price;
                ((Drink)itemToUpdate).Type = drink.Type;
            }
            else if (updatedItem is Halal)
            {
                Halal halal = (Halal)updatedItem;
                ((Halal)itemToUpdate).Name = halal.Name;
                ((Halal)itemToUpdate).Price = halal.Price;
                ((Halal)itemToUpdate).Type = halal.Type;
            }
            else if (updatedItem is Vegan)
            {
                Vegan vegan = (Vegan)updatedItem;
                ((Vegan)itemToUpdate).Name = vegan.Name;
                ((Vegan)itemToUpdate).Price = vegan.Price;
                ((Vegan)itemToUpdate).Type = vegan.Type;
            }
            else
            {
                throw new ArgumentException("Invalid item type.");
            }

            string updatedJsonString = JsonConvert.SerializeObject(menu, Formatting.Indented);
            File.WriteAllText(filePath, updatedJsonString);
            return true;
        }
        else
        {
            return false;
        }
    }
}
