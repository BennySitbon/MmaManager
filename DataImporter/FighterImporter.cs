using System;
using System.Collections.Generic;
using Google.GData.Client;
using Google.GData.Spreadsheets;

namespace DataImporter
{
    public class FighterImporter
    {
        static void Main(string[] args)
        {
        }
        public static List<T> GetGoogleSheetData<T>(Func<ListEntry, AtomEntry, T> rowToEntity) where T:class
        {
            SpreadsheetsService service = new SpreadsheetsService("MmaManager");            
            var query = new WorksheetQuery("1Y-W6ae2tKP0sV57cv0PVbozH3dUkr-5AYQ2N1hjwTb0","public","full");
            var toReturn = new List<T>();

            var feed = service.Query(query);
            foreach (var entry in feed.Entries)
            {
                // Print the title of this spreadsheet to the screen
                Console.WriteLine(entry.Title.Text);
            }
           // var spreadsheet = (SpreadsheetEntry)feed.Entries[0];
            foreach (var worksheet in feed.Entries)
            {
                AtomLink listFeedLink = worksheet.Links.FindService(GDataSpreadsheetsNameTable.ListRel, null);

                // Fetch the list feed of the worksheet.
                ListQuery listQuery = new ListQuery(listFeedLink.HRef.ToString());
                listQuery.StartIndex = 3;
                ListFeed listFeed = service.Query(listQuery);
                foreach (ListEntry row in listFeed.Entries)
                {
                    toReturn.Add(rowToEntity(row,worksheet));
                }
            }
            return toReturn;
        }
    }
}
