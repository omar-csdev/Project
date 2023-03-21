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
        public static void start()
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
            Console.WriteLine("\nClick enter to go back");
            Console.ReadLine();
            FoodMenu.Start();
        }
        public static DataTable MenuCreator()
        {
            var table = new DataTable();
            table.Columns.Add("nummer", typeof(int));
            table.Columns.Add("naam", typeof(string));
            table.Columns.Add("prijs", typeof(double));
            table.Columns.Add("ingrediënten", typeof(string));
            table.Rows.Add(1, "Burger", 3.99, "Brood, sla, ui, tomaat, hamburger, burgersaus");
            //maakt colommen aan
            return table;
        }
    }
}

