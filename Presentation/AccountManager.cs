public static class AccountManager
{
    public static void CreateAccount()
    {
        List<CustomerAccount> accounts = CustomerAccess.LoadAll();
        Console.Clear();
        Console.WriteLine();
        List<string> usernames = new List<string>();
        foreach (CustomerAccount account in accounts)
        {
            usernames.Add(account.UserNameSetter);
        }

        bool askingName = true;
        while (askingName)
        {
            Console.Clear();
            Console.WriteLine("Creating Account");
            Console.WriteLine("Enter a username:");
            string username = Console.ReadLine();
            if (username != null)
            {
                askingName = false;
            }
        }

        Console.WriteLine("Enter a password:");
        List<char> symbols = new List<char>() { '!', '@', '?', '#', '&' };

        bool askingPassword = true;
        while (askingPassword)
        {
            Console.Clear();
            Console.WriteLine("Creating Account");
            Console.WriteLine();
            int checking = 0;
            Helper.Say("!", "The password has got to contain 1 number and 1 symbol (!, @, ?, #, &)");
            string password = Console.ReadLine();
            foreach (char character in password)
            {
                if (symbols.Contains(character))
                {
                    checking += 1;
                }
            }
            bool containsInt = password.Any(char.IsDigit);

            if (containsInt && checking > 0)
            {
                askingPassword = false;
            }
        }
    }
}