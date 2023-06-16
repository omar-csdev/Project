using ConsoleTables;
using Newtonsoft.Json;
using System.Data;
using System.Drawing;
using System.Text;

public static class AdminManager
{
    public static void Start()
    {
        Console.Clear();
        WriteLogo();
        Helper.Say("1", "View all Customer accounts");
        Helper.Say("2", "View all Admin accounts");
        Helper.Say("3", "Remove Admin");
        Helper.Say("4", "Add Admin");
        Helper.Say("5", "Back");
        string input = Console.ReadLine();
        if (input == "1")
        {
            ShowAccountsCustomer();
            Helper.ContinueDisplay();
            Start();
        }

        else if (input == "2")
        {
            ShowAccounts();
            Helper.ContinueDisplay();
            Start();
        }

        else if (input == "3")
        {
            RemoveAdmin();
            Helper.ContinueDisplay();
            Start();
        }

        else if (input == "4")
        {
            AddAdmin();
            Helper.ContinueDisplay();
            Start();
        }

        else if (input == "5")
        {
            AdminDashboard.DisplayDashboard();
        }

        else
        {
            Helper.Say("!", "Invalid input, please choose from option 1-5");
            Helper.ContinueDisplay();
            Start();
        }

    }

    public static void RemoveAdmin()
    {
        Console.Clear();
        Console.WriteLine("REMOVING AN ADMIN:", Color.RebeccaPurple);


        //bij de functies verwijderen en toevoegen van een admin altijd nieuwe list van de json
        //als we de oude "test" list gebruiken en er zijn hiervoor admins toegevoegd worden deze
        //niet aangetoond bij het verwijderen
        List<Admin> updatedList = LoginAccess.LoadAll();


        if (updatedList.Count == 0)
        {
            Console.WriteLine("No Admins to remove!", Color.Red);
            Helper.ContinueDisplay();
        }

        int x = 1;
        foreach (Admin admin in updatedList)
        {
            if (admin.UserName != null)
            {
                Helper.Say($"{x}", $"{admin.UserName}");
                x += 1;
            }
        }
        Helper.Say("!", "Type '/back' to go back to the admin menu");
        Console.WriteLine("Enter the ID of the username you'd like to remove:");
        string id = Console.ReadLine();
        if (id == "/back")
        {
            Start();
        }

        if (Convert.ToInt32(id) <= 0 || id == null)
        {
            Console.WriteLine("Invalid input!", Color.Red);
            Helper.ContinueDisplay();
            Start();
        }
        int ID = Convert.ToInt32(id);
        if (ID <= updatedList.Count && ID > 0)
        {
            Console.WriteLine($"Succesfully removed admin {updatedList[ID - 1].UserName}!");
            updatedList.Remove(updatedList[ID - 1]);
            LoginAccess.WriteAll(updatedList);
        }
        else
        {
            Console.Clear();
            Console.WriteLine("ID not found.", Color.Red);
            Helper.ContinueDisplay();
            Start();
        }
    }

    //Adding admin which can only be done by the original admin account
    //this account is integrated into the system.
    public static void AddAdmin()
    {
        Console.Clear();
        Console.WriteLine("ADDING AN ADMIN:", Color.RebeccaPurple);
        List<Admin> updatedList = LoginAccess.LoadAll();
        Helper.Say("!", "Type '/back' to go back to the admin menu");
        Console.WriteLine("Username:");
        string username = Console.ReadLine();
        if (username == "/back")
        {
            Start();
        }
        foreach (Admin admin in updatedList)
        {
            if (admin.UserName == username)
            {
                Console.WriteLine("Username already taken!", Color.Red);
                Helper.ContinueDisplay();
                Start();
            }
        }

        //checks op het wachtwoord dat aangemaakt wordt
        Console.WriteLine("Password:");
        List<char> symbols = new List<char>() { '!', '@', '?', '#', '&' };

        bool creatingAccount = true;
        while (creatingAccount)
        {
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
                Admin newAdmin = new(username, password);
                updatedList.Add(newAdmin);
                LoginAccess.WriteAll(updatedList);
                creatingAccount = false;
            }

            else
            {
                Helper.Say("!", "Password does not meet criteria");
                Helper.ContinueDisplay();
            }
        }
        Console.WriteLine("Added admin succesfully!");
    }

    //this is the code where we create the table for admin accounts
    //this option is only available for the admin
    public static void ShowAccounts()
    {
        Console.OutputEncoding = Encoding.UTF8;
        var data = TableCreator();

        string[] columnNames = data.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

        DataRow[] rows = data.Select();


        var table = new ConsoleTable(columnNames);
        table.Configure(o => o.EnableCount = false);
        foreach (DataRow row in rows)
        {
            table.AddRow(row.ItemArray);
        }
        table.Write(Format.Default);
    }

    public static DataTable TableCreator()
    {
        List<Admin> AllAccounts = LoginAccess.LoadAll();
        var table = new DataTable();
        table.Columns.Add("Username", typeof(string));
        table.Columns.Add("Password", typeof(string));
        table.Columns.Add("Status", typeof(string));
        Console.WriteLine("Admin Accounts:");
        AccountsDisplay(AllAccounts, table);

        static void AccountsDisplay(List<Admin> accounts, DataTable table)
        {
            int num = 0;
            foreach (Admin account in accounts)
            {
                num++;
                string status = "Offline";
                if (account.IsLoggedIn) { status = "Online"; }
                table.Rows.Add(account.UserName, account.Password, status);
            }
        }
        return table;
    }


    //this is the code where we create the table for customer accounts
    //this option is only available for the admin
    public static void ShowAccountsCustomer()
    {
        Console.OutputEncoding = Encoding.UTF8;
        var data = TableCreatorCustomer();

        string[] columnNames = data.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();

        DataRow[] rows = data.Select();


        var table = new ConsoleTable(columnNames);
        table.Configure(o => o.EnableCount = false);
        foreach (DataRow row in rows)
        {
            table.AddRow(row.ItemArray);
        }
        table.Write(Format.Default);
    }

    public static DataTable TableCreatorCustomer()
    {
        List<CustomerAccount> AllAccounts = CustomerAccess.LoadAll();
        var table = new DataTable();
        table.Columns.Add("Username", typeof(string));
        table.Columns.Add("Password", typeof(string));
        table.Columns.Add("Status", typeof(string));
        Console.WriteLine("Customer Accounts:");
        AccountsDisplay(AllAccounts, table);

        static void AccountsDisplay(List<CustomerAccount> accounts, DataTable table)
        {
            int num = 0;
            foreach (CustomerAccount account in accounts)
            {
                num++;
                string status = "Offline";
                if (account.IsLoggedIn) { status = "Online"; }
                table.Rows.Add(account.UserNameSetter, account.PasswordSetter, status);
            }
        }
        return table;
    }

    public static void WriteLogo()
    {
        string logo = @"     _                             _       
    / \   ___ ___ ___  _   _ _ __ | |_ ___ 
   / _ \ / __/ __/ _ \| | | | '_ \| __/ __|
  / ___ \ (_| (_| (_) | |_| | | | | |_\__ \
 /_/   \_\___\___\___/ \__,_|_| |_|\__|___/
                                 ";

        Console.WriteLine(logo, Color.Wheat);
    }
}