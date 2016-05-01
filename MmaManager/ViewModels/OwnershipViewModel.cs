using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Models;

namespace MmaManager.ViewModels
{
    public class OwnershipViewModel
    {
        public Ownership Ownership { get; set; }
        public decimal NetIncome { get; set; }
        public string OwnershipRecord { get; set; }
    }
}