using Common.Lib.Authentication;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyLibrary.Lib.Models
{
    public class Client : User
    {
        public string Name { get; set; }
        public string Surname1 { get; set; }
        public string Surname2 { get; set; }
        public string Dni { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }

        public virtual List<Loans> Loans { get; set; }  


    }

    public Client()
    {
    }

    public enum ClientValidationsTypes
    {
        Ok,
        WrongDniFormat,
        DniDuplicated,
        WrongNameFormat,
        IdNotEmpty,
        IdDuplicated,
        IdEmpty,
        NotFound
    }


}

