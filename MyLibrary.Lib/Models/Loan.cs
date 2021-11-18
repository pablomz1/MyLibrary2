using Common.Lib.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyLibrary.Lib.Models
{
    public class Loan : Entity
    {

        #region Client

        public Guid ClientId { get; set; }
        public Client Client { get; set; }

        #endregion

        #region bookCopy


        public Guid BookCopyId { get; set; }
        public BookCopy BookCopy { get; set; }

        #endregion 


        public DateTime RequestDate { get; set; }
        public DateTime ReturnDate { get; set; }


        
    }
}
