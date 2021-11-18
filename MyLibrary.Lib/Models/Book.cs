using Common.Lib.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyLibrary.Lib.Models
{
    public class Book : Entity
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Brieving { get; set; }
        public int FirstPublicationYear { get; set; }
        public virtual list <BookCopy> BookCopies { get; set; }
    }
}
