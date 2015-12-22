using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

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
        public int FightListingID { get; set; }
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
    }
}