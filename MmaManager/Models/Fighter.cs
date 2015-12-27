using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MmaManager.Models
{
    public enum Division
    {
        Flyweight,Bantamweight,Featherweight,Lightweight,Welterweight,Middleweight,
        LightHeavyweight,Heavyweight,FemaleBantamweight, Unknown
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
        public List<Division> Divisions { get; set; }
        public int? Ranking { get; set; }
        public int Wins { get; set; }
        public int Loses { get; set; }
        public virtual ICollection<FightListing> FightListings { get; set; }

        public Fighter()
        {
            Divisions = new List<Division>();
        }
    }
}