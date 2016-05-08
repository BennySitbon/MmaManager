using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Models;

namespace MmaManager.ViewModels
{
    public class OnSaleOwnershipViewModel
    {
        public Ownership Ownership { get; set; }
        public bool CanBuy { get; set; }
    }
}