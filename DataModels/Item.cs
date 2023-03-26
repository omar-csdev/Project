public class Item
{
    public string Name { get; set; }
    public string Category { get; set; }
    public double Price { get; set; }
    public int Id { get; set; }

    public Item(string name, double price, string category)
    {
        Name = name;
        Price = price;
        Category = category;
    }
}