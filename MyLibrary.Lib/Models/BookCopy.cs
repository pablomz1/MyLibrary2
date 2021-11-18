using Common.Lib.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyLibrary.Lib.Models
{
    public class BookCopy:  Entity

    {
        public Guid BookId { get; set; }

        public Book Book { get; set; }

        public string Barcode { get; set; }

        public virtual List<Loan> Loans { get; set;}








    }
}
