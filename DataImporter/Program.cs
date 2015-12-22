using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Google.GData.Spreadsheets;
using Google.GData.Client;
using MmaManager.Models;

namespace DataImporter
{
    class Program
    {
        static void Main(string[] args)
        {
            SpreadsheetsService service = new SpreadsheetsService("MmaManager");            
            var query = new WorksheetQuery("1Y-W6ae2tKP0sV57cv0PVbozH3dUkr-5AYQ2N1hjwTb0","public","full");

            var feed = service.Query(query);
            foreach (var entry in feed.Entries)
            {
                // Print the title of this spreadsheet to the screen
                Console.WriteLine(entry.Title.Text);
            }
           // var spreadsheet = (SpreadsheetEntry)feed.Entries[0];
            WorksheetEntry worksheet = (WorksheetEntry)feed.Entries[0];
            AtomLink listFeedLink = worksheet.Links.FindService(GDataSpreadsheetsNameTable.ListRel, null);

            // Fetch the list feed of the worksheet.
            ListQuery listQuery = new ListQuery(listFeedLink.HRef.ToString());
            listQuery.StartIndex = 3;
            ListFeed listFeed = service.Query(listQuery);
            // Iterate through each row, printing its cell values.
            
            foreach (ListEntry row in listFeed.Entries)
            {
                // Print the first column's cell value
                Console.WriteLine(row.Title.Text);
                // Iterate over the remaining columns, and print each cell value
                foreach (ListEntry.Custom element in row.Elements)
                {
                    Console.WriteLine(element.Value);
                }
                var result = RowToFighter(row);
                Console.ReadLine();

            }
            
            Console.ReadLine();
            
        }

        private static Fighter RowToFighter(ListEntry row)
        {
            var fighter = new Fighter();
            int success;
            if(Int32.TryParse(row.Elements[0].Value, out success))
            {
                fighter.Ranking = success;
            }
            Regex rgx = new Regex("[^a-zA-Z ]");
            var dirtyName = row.Elements[2].Value; 
            var fullName = rgx.Replace(dirtyName,"").Split(' ');
            fighter.LastName = fullName[1]; //Parse this better later
            fighter.FirstMidName = fullName[0];
            return fighter;
        }
    }
}
