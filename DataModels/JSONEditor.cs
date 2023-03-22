using Newtonsoft.Json;

public class JSONEditor
{
    public static void AddItem(string fileName, Item item)
    {
        string JSONString = File.ReadAllText(fileName);
        List<Item> menu = JsonConvert.DeserializeObject<List<Item>>(JSONString) ?? new List<Item>();

    }
}