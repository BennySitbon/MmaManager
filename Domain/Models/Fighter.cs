﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Providers;

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
        [Key]
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
            get { return FirstMidName + " " + LastName; }
        }
        public string FullNameWithNickname
        {
            get { return FirstMidName + (Nickname != null? " \"" + Nickname + "\" ": " ") + LastName; }
        }
        public int Worth
        {
            get { return FighterWorthProvider.GetWorth(this); }
        }
        public virtual ICollection<FightListing> FightListings { get; set; }
        public bool IsActive { get; set; }
        public string Record
        {
            get
            {
                var record = Wins + "-" + Loses;
                if (Draws > 0) record += "-" + Draws;
                if (NoContest > 0) record += " " + NoContest + "NC";
                return record;
            }            
        }

        //Should do something to accomdate fighers with same name someway
        public override bool Equals(object obj)
        {
            var toCheck = obj as Fighter;
            return toCheck != null &&
                toCheck.FullName.ToLowerInvariant().Equals(FullName.ToLowerInvariant());
        }

        public override int GetHashCode()
        {
            return FullName.ToLowerInvariant().GetHashCode();
        }
    }
}