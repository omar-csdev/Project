public class Drink : Item
{
    public string Name;
    public string Category;
    public double Price;
    public Drink(string name, double price) : base("Drink", price)
    {
        this.Name = name;
        this.Price = price;
        Category = "Drink";
    }
}