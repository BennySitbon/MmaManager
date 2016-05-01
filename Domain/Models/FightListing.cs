using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public enum FightResult
    {
        RedWin,BlueWin,Draw,NC
    }
    public enum WinType
    {
        KO,TKO,Submission,Decision
    }
    public enum BonusType
    {
        FOTN,POTN
    }
    public class FightListing
    {
        [Key]
        public int FightListingID { get; set; }
        public int RedFighterFighterID { get; set; }
        public int BlueFighterFighterID { get; set; }

        public int EventID { get; set; }
        [Display(Name = "Fight Result")]
        public FightResult? FightResult { get; set; }
        [Display(Name = "Win Round")]
        public int? WinRound { get; set; }
        [Display(Name = "Win Time")]
        public int? WinTime { get; set; }
        [Display(Name = "Win Type")]
        public WinType? WinType { get; set; }
        [Display(Name = "Fight Bonus")]
        public BonusType? FightBonus { get; set; }
        public virtual Fighter RedFighter { get; set; }
        public virtual Fighter BlueFighter { get; set; }
        public virtual Event Event { get; set; }

        public string GetResult()
        {
            string name = "";
            switch (FightResult)
            {
                case Models.FightResult.RedWin:
                    name = RedFighter.LastName;
                    break;
                case Models.FightResult.BlueWin:
                    name = BlueFighter.LastName;
                    break;
                case Models.FightResult.Draw:
                    return "Draw";
                case Models.FightResult.NC:
                    return "NC";
                default:
                    return "TBD";
            }
            return name + " By " + WinType;
        }
    }
}