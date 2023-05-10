public class Item
{
    public string Name { get; set; }
    public string Type { get; set; }
    public double Price { get; set; }

    public string Category { get; set; }
    public int Id { get; set; }

    public Item(string name, double price, string type, string category)
    {
        Name = name;
        Price = price;
        Type = type;
        Category = category;
    }
}