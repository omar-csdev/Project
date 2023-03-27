public static class AdminLogin
{
    public static void Start()
    {

        List<Admin> test = LoginAccess.LoadAll();
        foreach (Admin admin in test) 
        {
            Console.WriteLine("hi");
        }
        Console.Clear();
        Console.WriteLine("Login als Admin.");
        Console.WriteLine("Gebruikersnaam");
        string username = Console.ReadLine();
    }
}