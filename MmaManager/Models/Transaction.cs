using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MmaManager.Models
{
    public enum TransactionType
    {
        Winnings,
        Sell,
        NewPlayer
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
        [Display(Name = "Transaction Type")]
        public TransactionType TransactionType {get; set; }
        public int? FightListingID { get; set; }
        public int? FighterID { get; set; }

        public decimal AmountForUser(string username)
        {
            if(username == ToUser) return Amount;
            if(username == FromUser) return Amount*-1;
            return 0;
        }
        public virtual Fighter Fighter { get; set; }
        public virtual FightListing FightListing { get; set; }
    }
}