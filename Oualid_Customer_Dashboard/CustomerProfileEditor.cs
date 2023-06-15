using System.Drawing;
using Console = Colorful.Console;
using Newtonsoft.Json;

public static class CustomerProfileEditor
{
    private static CustomerAccount customer { get; set; }
    public static void DisplayDashboard()
    {
        GetLoggedInCustomer();
        while (customer != null)
        {
            Console.Clear();
            WriteLogo();
            DisplayCustomerData();
            WriteToConsole(1, "Edit Name");
            WriteToConsole(2, "Edit Password");
            WriteToConsole(3, "Go Back");
            try
            {
                string ? input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        EditName();
                        break;
                    case "2":
                        EditPassword();
                        break;
                    case "3":
                        CustomerDashboard.DisplayDashboard();
                        break;
                    default:
                        throw new Exception("Invalid input");
                }
            }catch(Exception ex)
                {
                    Console.WriteLine(ex.Message, Color.Red);
                    Helper.ContinueDisplay();
                }
    }
    }

    public static void WriteToConsole(int prefix, string message)
    {
        Console.Write("[");
        Console.Write(prefix, Color.Red);
        Console.WriteLine("] " + message);
    }

    public static void WriteLogo()
    {
        string logo = @"
____             __ _ _                 _ _ _             
|  _ \ _ __ ___  / _(_) | ___    ___  __| (_) |_ ___  _ __ 
| |_) | '__/ _ \| |_| | |/ _ \  / _ \/ _` | | __/ _ \| '__|
|  __/| | | (_) |  _| | |  __/ |  __/ (_| | | || (_) | |   
|_|   |_|  \___/|_| |_|_|\___|  \___|\__,_|_|\__\___/|_|                        
        ";

        Console.WriteLine(logo, Color.Wheat);
    }

    public static void ClearScreen()
    {
        Console.Clear();
        WriteLogo();
    }

    public static void GetLoggedInCustomer()
    {
        List<CustomerAccount> accounts = CustomerAccess.LoadAll();
        CustomerAccount loggedInCustomer = accounts.FirstOrDefault(x => x.IsLoggedIn == true);
        if (loggedInCustomer != null)
        {
            customer = loggedInCustomer;
        }
        else
        {
            Console.WriteLine("Something went wrong! You will be redirected back to the log in page.");
            Console.ReadKey();
            AccountManager.LogIn();
        }
    }

    public static void DisplayCustomerData()
    {
        if (customer == null) return;

        Console.WriteLine("Name: " + customer.UserNameSetter);
        Console.Write("Password: ");
        for(int i = 0; i < customer.PasswordSetter.Length; i++)
        {
            Console.Write("*");
        }
        Console.WriteLine();
        Console.WriteLine();
    }

    public static void EditName(){
        ClearScreen();
        Console.Write("Enter your new username: ");
        try
        {
            string? newName = Console.ReadLine();

            if (newName.Length < 3)
            {
                throw new Exception("Name must be at least 3 characters long!");
            }

            if (newName == customer.UserNameSetter)
            {
                throw new Exception("Name cannot be the same username as your old one!");
            }

            List<CustomerAccount> accounts = CustomerAccess.LoadAll();
            CustomerAccount? account = accounts.FirstOrDefault(x => x.ID == customer.ID);
            if (account != null)
            {
                account.UserNameSetter = newName;
            }
            else
            {
                throw new Exception("Error! Something went wrong searching for your account!");
            }
            CustomerAccess.WriteAll(accounts);
            GetLoggedInCustomer();
            Console.WriteLine();
            Console.WriteLine($"Succesfully updated username to {customer.UserNameSetter}", Color.Green);
            Helper.ContinueDisplay();
            CustomerProfileEditor.DisplayDashboard();

        }
        catch (Exception e)
        {
            Console.WriteLine();
            Console.WriteLine(e.Message, Color.Red);
            Helper.ContinueDisplay();
            CustomerProfileEditor.DisplayDashboard();
        }
    }

    public static void EditPassword()
    {
        ClearScreen();
        Console.Write("Enter your old password: ");
        try
        {
            string ? oldPassword = Console.ReadLine();

            if (oldPassword != customer.PasswordSetter)
            {
                throw new Exception("Entered wrong password!");
            }

            ClearScreen();
            Helper.Say("!", "The password has got to contain 1 number and 1 symbol (!, @, ?, #, &)");
            Console.Write("Enter your new password: ");

            string? newPassword = Console.ReadLine();
            bool validPassword = ValidatePassword(newPassword);

            if (!validPassword)
            {
                throw new Exception("The password has got to contain 1 number and 1 symbol (!, @, ?, #, &)");
            }

            if (newPassword == customer.PasswordSetter)
            {
                throw new Exception("Password cannot be the same as your old password!");
            }
            Console.Write("Re-enter your new password: ");
            string ? repeatedNewPassword = Console.ReadLine();

            if(repeatedNewPassword != newPassword)
            {
                throw new Exception("Password did not match.");
            }

            List<CustomerAccount> accounts = CustomerAccess.LoadAll();
            CustomerAccount? account = accounts.FirstOrDefault(x => x.ID == customer.ID);
            if (account != null)
            {
                account.PasswordSetter = newPassword;
            }
            else
            {
                throw new Exception("Error! Something went wrong searching for your account!");
            }
            CustomerAccess.WriteAll(accounts);
            GetLoggedInCustomer();
            Console.WriteLine();
            Console.WriteLine($"Succesfully updated password to {customer.PasswordSetter}", Color.Green);
            Helper.ContinueDisplay();
            CustomerProfileEditor.DisplayDashboard();

        }
        catch (Exception e)
        {
            Console.WriteLine();
            Console.WriteLine(e.Message, Color.Red);
            Helper.ContinueDisplay();
            CustomerProfileEditor.DisplayDashboard();
        }
    }

    public static bool ValidatePassword(string password)
    {
        List<char> symbols = new List<char>() { '!', '@', '?', '#', '&' };

        // Check if password contains at least one symbol
        bool hasSymbol = symbols.Any(symbol => password.Contains(symbol));

        // Check if password contains at least one digit
        bool hasDigit = password.Any(char.IsDigit);

        return hasSymbol && hasDigit;
    }
}
