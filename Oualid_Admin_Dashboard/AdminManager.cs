public static class AdminManager
{
    public static void Start()
    {
        Console.Clear();
        ShowAccounts();
        Console.WriteLine("Press enter to go back.");
        var input = Console.ReadLine();
        AdminDashboard.DisplayDashboard();
    }

    public static void ShowAccounts()
    {
        List<Admin> AllAccounts = LoginAccess.LoadAll("admindata.json");
        int i = 0;
        foreach (Admin admin in AllAccounts) 
        {
            string x = "Offline";
            if (admin.IsLoggedIn) 
            {
                x = "Online";
            }
            if (i != 0)
            {
                Console.WriteLine($"{i} | {admin.UserName} | {x}");
            }
            i += 1;
        }
    }
}