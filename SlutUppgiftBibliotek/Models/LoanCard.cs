using EntityFrameworkCore.EncryptColumn.Attribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlutUppgiftBibliotek.Models
{
    internal class LoanCard
    {
        public int Id { get; set; }
        [StringLength(4, ErrorMessage = "The property must be exactly 4 characters long.")]
        [EncryptColumn]
        public string Pin { get; set; }
        public Borrower Borrower { get; set; }
    }
}
