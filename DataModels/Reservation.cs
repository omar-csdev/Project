public class Reservation
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public int GroupSize { get; set; }

    public string Code { get; set; }
    public int Id { get; set; }

    public Reservation(string name, string lastName, int groupSize, string code)
    {
        Name = name;
        LastName = lastName;
        GroupSize = groupSize;
        Code = code;
    }
}