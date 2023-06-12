using System.Drawing;

public static class AccountManager
{

    //creating account
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

        //username checks
        string username = null;
        bool askingName = true;
        while (askingName)
        {
            Console.Clear();
            Console.WriteLine("Creating Account");
            Helper.Say("!", "Type '/back' to go back to the main menu");
            Console.WriteLine("Enter a username:");
            username = Console.ReadLine();
            if (username == "/back")
            {
                MainMenu.NewStart();
            }

            if (username != null && usernames.Contains(username) == false)
            {
                askingName = false;
            }

            else
            {
                Helper.Say("!", "Username unavailable");
                Helper.ContinueDisplay();
            }
        }

        List<char> symbols = new List<char>() { '!', '@', '?', '#', '&' };

        //password checks
        string password = null;
        bool askingPassword = true;
        while (askingPassword)
        {
            Console.Clear();
            Console.WriteLine("Creating Account");
            Console.WriteLine();
            int checking = 0;
            Console.WriteLine("Enter a password:");
            Helper.Say("!", "The password has got to contain 1 number and 1 symbol (!, @, ?, #, &)");
            Helper.Say("!", "Type '/back' to go back to the main menu");
            password = Console.ReadLine();

            if (password == "/back")
            {
                MainMenu.NewStart();
            }

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

            else
            {
                Helper.Say("!", "Password does not meet criteria");
                Helper.ContinueDisplay();
            }
        }

        accounts.Add(new CustomerAccount(username, password, accounts));
        CustomerAccess.WriteAll(accounts);
        Console.WriteLine($"Registered user {username} succesfully!");
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
        MainMenu.NewStart();
    }

    public static void LogIn()
    {
        Console.Clear();
        Console.WriteLine("Logging in");
        Helper.Say("!", "Type '/back' to go back to the main menu");
        List<CustomerAccount> accounts = CustomerAccess.LoadAll();
        string username = null;
        bool askingUsername = true;
        while (askingUsername)
        {
            Console.WriteLine("Username:");
            username = Console.ReadLine();
            if (username == "/back")
            {
                MainMenu.NewStart();
            }
            int check = 0;
            foreach (CustomerAccount i in accounts)
            {
                if (i.UserNameSetter == username)
                {
                    check += 1;
                }
            }

            //check of gebruikersnaam bestaat
            if (check == 0)
            {
                Console.WriteLine("Username doesn't exist!", Color.Red);
                Helper.ContinueDisplay();

            }

            else
            {
                askingUsername = false;
            }
        }

        Console.WriteLine("Password:");
        string password = Console.ReadLine();


        //kijken in de json of de gegeven combinatie van wachtwoord en gebruikersnaam bestaat.
        foreach (CustomerAccount customer in accounts)
        {
            if (customer.UserNameSetter == username && customer.PasswordSetter == password)
            {
                customer.IsLoggedIn = true;
                CustomerAccess.WriteAll(accounts);
                Console.WriteLine($"User {username} logged in succesfully!");
                Helper.ContinueDisplay();
                CustomerDashboard.DisplayDashboard();
            }
        }
        Helper.Say("!", "No users found with the matching credentials");
        Helper.ContinueDisplay();
        MainMenu.NewStart();
    }
}