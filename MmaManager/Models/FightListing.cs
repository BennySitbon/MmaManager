using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MmaManager.Models
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
        //[ForeignKey()]
        public int RedFighterFighterID { get; set; }
        public int BlueFighterFighterID { get; set; }
        public int EventID { get; set; }
        public FightResult? FightResult { get; set; }
        public int? WinRound { get; set; }
        public int? WinTime { get; set; }
        public WinType? WinType { get; set; }
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