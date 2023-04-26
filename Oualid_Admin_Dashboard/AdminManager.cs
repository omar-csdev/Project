public static class AdminManager
{
    public static void Start()
    {
        
    }

    public static void ShowAccounts()
    {
        List<Admin> AllAccounts = LoginAccess.LoadAll("admindata.json");
    }
}