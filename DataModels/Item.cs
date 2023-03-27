public class Item
{
    public string Name { get; set; }
    public string Type { get; set; }
    public double Price { get; set; }
    public int Id { get; set; }

    public Item(string name, double price, string type)
    {
        Name = name;
        Price = price;
        Type = type;
    }
}