using System;
using System.Collections.Generic;
using System.Linq;
using MyLibrary.Lib.Models;


namespace MyLibrary.UI.DAL
{
    public class ClientsRepository : IDisposable
    {
        public LibraryDbContext DbContext { get; set; }

        public List<Client> ClientsList
        {
            get
            {
                return GetAll();
            }
        }

        public ClientsRepository()
        {
            DbContext = new LibraryDbContext();
            LoadInitData();
        }

        public void LoadInitData()
        {
            if (DbContext.Clients.Count() == 0)
            {
                //creo Clients de prueba
                var std1 = new Client()
                {
                    Id = Guid.NewGuid(),
                    Name = "Pepe",
                    Email = "p@p.com",
                    Password = "1234",
                    Surname1 = "García",
                    Surname2 = string.Empty,
                    Address = "13 Rue del Percebe",
                    //Phone = string.Empty
                };
                var std2 = new Client()
                {
                    Id = Guid.NewGuid(),
                    Name = "Marta",
                    Email = "m@m.com",
                    Password = "qwer",
                    Surname1 = "Márquez",
                    Surname2 = string.Empty,
                    Address = "El Clot mola",
                    //Phone = "123456789"

                };

                // y lo meto en el campo interno
                DbContext.Clients.Add(std1);
                DbContext.Clients.Add(std2);

                DbContext.SaveChanges();
            }

        }

        public List<Client> GetAll()
        {
            return DbContext.Clients.ToList();
        }

        public Client Get(Guid id)
        {
            var output = DbContext.Clients.Find(id);
            return output;
        }

        public List<Client> GetByName(string name)
        {

            var output = DbContext.Clients.Where(s => s.Name == name).ToList();
            return output;
        }

        public ClientValidationsTypes Add(Client Client)
        {
            if (Client.Id != default(Guid))
            {
                // todo bien porque no hay ningún Id
                return ClientValidationsTypes.IdNotEmpty;
            }

            if (!Client.ValidateName(Client.Name))
            {
                return ClientValidationsTypes.WrongNameFormat;
            }
            //else if (!DbContext.Clients.Any(s => s.Id == Client.Id)) ineficiente
            else if (DbContext.Clients.All(s => s.Id != Client.Id)) // más eficiente
            {
                Client.Id = Guid.NewGuid();
                DbContext.Clients.Add(Client);
                DbContext.SaveChanges();

                return ClientValidationsTypes.Ok;
            }

            // si el Id no estaba vacío y ya existía en la DB devolvmenos false
            return ClientValidationsTypes.IdDuplicated;
        }

        public ClientValidationsTypes Update(Client Client)
        {
            if (Client.Id == default(Guid))
            {
                // no se puede actualizar un registro sin id
                return ClientValidationsTypes.IdEmpty;
            }
            if (DbContext.Clients.All(s => s.Id != Client.Id))
            {
                // no se puede actualizar un registro
                // que no exista en la DB
                return ClientValidationsTypes.NotFound;
            }

            if (!Client.ValidateName(Client.Name))
            {
                return ClientValidationsTypes.WrongNameFormat;
            }

            DbContext.Clients.Update(Client);
            DbContext.SaveChanges();

            return ClientValidationsTypes.Ok;
        }

        public ClientValidationsTypes Delete(Guid id)
        {
            var Client = DbContext.Clients.Find(id);
            if (Client == null)
            {
                return ClientValidationsTypes.NotFound;
            }
            else
            {
                DbContext.Clients.Remove(Client);
                return ClientValidationsTypes.Ok;
            }
        }

        public void Dispose()
        {
        }
    }
}
