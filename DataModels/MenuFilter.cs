using ConsoleTables;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class MenuFilter
{
    private readonly List<Item> _items;

    public MenuFilter(List<Item> items)
    {
        _items = items;
    }

    public List<Item> Filter(string type, string category)
    {
        var filteredItems = _items.Where(item => item.Type == type && item.Category == category).ToList();
        return filteredItems;
    }

    /*var allMenuItems = getMenuItems();

    var filter = new MenuFilter(allMenuItems);

    // Filter food items that are vegan
    var veganFoodItems = filter.Filter("Food", "Vegan");
    createAndDisplayTable(veganFoodItems, "Vegan Food Menu");

    // Filter drink items that are non-alcoholic
    var nonAlcoholicDrinkItems = filter.Filter("Drink", "Non-Alcoholic");
    createAndDisplayTable(nonAlcoholicDrinkItems, "Non-Alcoholic Drink Menu");*/
}

