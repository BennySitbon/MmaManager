using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Ownership
    {
        public int OwnershipID { get; set; }
        public string Username { get; set; }
        public int FighterID { get; set; }
        public int TransactionID { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName="money")]
        [Display(Name="Price")]
        public decimal PriceRequested { get; set; }
        public virtual Fighter Fighter { get; set; }
        public virtual Transaction Transaction { get; set; }
    }
}