using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MmaManager.Models
{
    public enum TransactionType
    {
        Winnings,Sell,NewPlayer
    }
    public class Transaction
    {
        public int TransactionID { get; set; }
        public string FromUser { get; set; }
        public string ToUser { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName="money")]
        public decimal Amount { get; set; }
        [Display(Name="Timestamp")]
        public DateTime TimeStamp { get; set; }
        public TransactionType TransactionType {get; set; }
        public int? FightListingID { get; set; }
        public int? FighterID { get; set; }
        public virtual Fighter Fighter { get; set; }
        public virtual FightListing FightListing { get; set; }
    }
}