using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Google.GData.Client;
using Google.GData.Spreadsheets;

namespace DataImporter
{
    public class FighterImporter
    {
        static void Main(string[] args)
        {
            /*var result = GetGoogleSheetData();
            XmlSerializer serializer = new XmlSerializer(typeof (Fighter));
            result.ForEach(r => serializer.Serialize(Console.Out,r));
            Console.ReadLine();*/

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
                // Iterate through each row, printing its cell values.
            
                foreach (ListEntry row in listFeed.Entries)
                {
                    //// Print the first column's cell value
                    //Console.WriteLine(row.Title.Text);
                    //// Iterate over the remaining columns, and print each cell value
                    //foreach (ListEntry.Custom element in row.Elements)
                    //{
                    //    Console.WriteLine(element.Value);
                    //}
                   // Division div;
                    //toReturn.Add(RowToFighter(row, Enum.TryParse(worksheet.Title.Text, true, out div) ? div : Division.Unknown));
                    toReturn.Add(rowToEntity(row,worksheet));
                }
            }
            return toReturn;
        }
        /*
        private static Fighter RowToFighter(ListEntry row, Division div)
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
            var lastNameList = fullName.Skip(1).ToList();
            var lastNameTemp = "";
            lastNameList.ForEach(l =>
            {
                lastNameTemp = lastNameTemp +" "+ l;
            });
            fighter.LastName = lastNameTemp.TrimStart();
            fighter.FirstMidName = fullName[0];
            if (row.Elements[0].Value == "C.")
            {
                fighter.Ranking = 0;
            }
            else if (Int32.TryParse(row.Elements[0].Value, out success))
            {
                fighter.Ranking = success;
            }
            if (Int32.TryParse(row.Elements[5].Value, out success))
            {
                fighter.Wins = success;
            }
            if (Int32.TryParse(row.Elements[6].Value, out success))
            {
                fighter.Loses = success;
            }
            if (div != Division.Unknown)
            {
                fighter.Division = div;
            }
            return fighter;
        }*/
    }
}
