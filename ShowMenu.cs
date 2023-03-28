using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Menu_item_creëren
{
    static class MenuItem
    {
        public static void Start()
        {

            Console.OutputEncoding = Encoding.UTF8;
            var data = MenuCreator();
            //voert de tabelcommands uit.
            string[] columnNames = data.Columns.Cast<DataColumn>()
                                 .Select(x => x.ColumnName)
                                 .ToArray();
            //maakt het mogelijk om per colom data toe te voegen.

            DataRow[] rows = data.Select();
            //alle rijen van de tabel worden geselecteerd

            var table = new ConsoleTable(columnNames);
            //maakt een nieuwe tabel aan met de configuraties van DataTable
            table.Configure(o => o.EnableCount = false);
            //Verwijdert de count die er standaard wordt geprint (op deze manier: Count: 0-10000)
            foreach (DataRow row in rows)
            {
                table.AddRow(row.ItemArray);
            }
            table.Write(Format.Default);
            //met deze lijn code kunnen we het uiterlijk van de tabel aanpassen 
        }
        public static DataTable MenuCreator()
        {
            var table = new DataTable();
            table.Columns.Add("nummer", typeof(int));
            table.Columns.Add("Categorie", typeof(string));
            table.Columns.Add("naam", typeof(string));
            table.Columns.Add("prijs", typeof(string));
            Console.WriteLine("Menu Items:");


            getMenuItems(table);
            //de bedoeling is om een for loop te maken die langs elke item in de json file en in deze for-loop steeds een rij toe te voegen.
            //Hierdoor kunnen we de tabel updaten (item verwijderen/toevoegen in de json zal de tabel groter/kleiner maken)
            static void getMenuItems(DataTable table)
            {
                
                string filePath = Path.Combine(Environment.CurrentDirectory, @"C:\Users\elfar\source\repos\Project\DataSources\menu.json");
                string JSONString = File.ReadAllText(filePath);
                List<Item> menu = JsonConvert.DeserializeObject<List<Item>>(JSONString) ?? new List<Item>();
                displayMenuItems(menu, table);
            }

            static void displayMenuItems(List<Item> menuToDisplay, DataTable table)
            {
                int num = 0;
                foreach (Item item in menuToDisplay)
                {
                    num++;
                    table.Rows.Add(num, item.Category, item.Name, "€"+item.Price);
                }
            }
            return table;
        }
    }
}

