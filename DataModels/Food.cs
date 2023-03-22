public class Food : Item
{
    public string Name;
    public string Category;
    public double Price;
    public Food(string name, double price) : base("Food", price) 
    {
        this.Name = name;
        this.Price = price;
        Category = "Food";
    }
}