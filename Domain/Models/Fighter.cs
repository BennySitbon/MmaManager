using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public enum Division
    {
        
        Flyweight,Bantamweight,Featherweight,Lightweight,Welterweight,Middleweight,
        [Display(Name = "Light Heavyweight")]
        LightHeavyweight,
        Heavyweight,
        [Display(Name = "Female Bantamweight")]
        FemaleBantamweight, Unknown
    }
    public class Fighter
    {
        public int FighterId { get; set; }
        [StringLength(25)]
        [Required]
        [Display(Name="First Name")]
        public string FirstMidName { get; set; }
        [StringLength(50)]
        [Display(Name="Last Name")]
        [Required]
        public string LastName { get; set; }
        [StringLength(30)]
        public string Nickname { get; set; }
        public int? Height { get; set; }
        public int? Reach { get; set; }
        public Division? Division { get; set; }
        public int? Ranking { get; set; }
        public int Wins { get; set; }
        public int Loses { get; set; }
        public int Draws { get; set; }
        [Display(Name = "No Contest")]
        public int NoContest { get; set; }
        public string FullName
        {
            get { return FirstMidName + (Nickname != null? " \"" + Nickname + "\" ": " ") + LastName; }
        }

        public decimal Worth
        {
            get { return GetWorth(); }
        }
        public virtual ICollection<FightListing> FightListings { get; set; }

        public Fighter()
        {
        }

        private decimal GetWorth()
        {
            //TODO: dont hardcode these numbers
            if (Ranking == null) return 500;
            if (Ranking == 0) return 20000;
            return 16000 - 1000*Ranking.Value;
        }
        public string GetRecord()
        {
            var record = Wins + "-" + Loses;
            if (Draws > 0) record += "-" + Draws;
            if (NoContest > 0) record += " " + NoContest + "NC";
            return record;

        }
    }
}