﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.Enums;

namespace Domain.Models
{    
    public class Transaction
    {
        public Transaction()
        {
            TimeStamp = DateTime.Now;
        }
        public int TransactionID { get; set; }
        public string FromUser { get; set; }
        public string ToUser { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName="money")]
        public int Amount { get; set; }
        [Display(Name="Timestamp")]
        public DateTime TimeStamp { get; set; }
        [Display(Name = "Transaction Type")]
        public TransactionType TransactionType {get; set; }
        public int? FightListingID { get; set; }
        public int? FighterID { get; set; }

        public int AmountForUser(string username)
        {
            if(username == ToUser) return Amount;
            if(username == FromUser) return Amount*-1;
            return 0;
        }
        public virtual Fighter Fighter { get; set; }
        public virtual FightListing FightListing { get; set; }
    }
}