using ConsoleTables;
using Newtonsoft.Json;
using System.Data;
using System.Text;

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
        List<Admin> AllAccounts = LoginAccess.LoadAll("admindata.json");
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
}