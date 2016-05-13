using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DataImporter;
using Domain.DAL;
using Domain.Models;

namespace Service.Administration
{
    public class FighterImportService : IFighterImportService
    {
        private readonly IRepository _repository;

        public FighterImportService(IRepository repository)
        {
            _repository = repository;
        }

        public List<Fighter> GetFightersFromImport()
        {
            var result = FighterImporter.GetGoogleSheetData((row, worksheet) =>
            {
                var fighter = new Fighter();
                int success;
                if (Int32.TryParse(row.Elements[0].Value, out success))
                {
                    fighter.Ranking = success;
                }
                Regex rgx = new Regex("[^a-zA-Z ]");
                var dirtyName = row.Elements[2].Value;
                var fullName = rgx.Replace(dirtyName, "").Split(' ');
                var lastNameList = fullName.Skip(1).ToList();
                var lastNameTemp = "";
                lastNameList.ForEach(l =>
                {
                    lastNameTemp = lastNameTemp + " " + l;
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
                if (Int32.TryParse(row.Elements[10].Value, out success))
                {
                    fighter.Draws = success;
                }
                if (Int32.TryParse(row.Elements[11].Value, out success))
                {
                    fighter.NoContest = success;
                }
                Division div;
                div = Enum.TryParse(worksheet.Title.Text, true, out div) ? div : Division.Unknown;
                if (div != Division.Unknown)
                {
                    fighter.Division = div;
                }
                return fighter;
            });

            return result;
        }

        public void HydrateWithIds(HashSet<Fighter> fighters)
        {
            var dbFightersSet = new Dictionary<Fighter, Fighter>(
                    _repository.GetAll<Fighter>().ToDictionary(o => o, u => u));
            foreach (var fighter in fighters)
            {
                if (dbFightersSet.Keys.Contains(fighter))
                {
                    fighter.FighterId = dbFightersSet[fighter].FighterId;
                }
            }
        }
    }
}
