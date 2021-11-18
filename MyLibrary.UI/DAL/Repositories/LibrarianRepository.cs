using System;
using System.Collections.Generic;
using System.Linq;
using MyLibrary.Lib.Models;

namespace MyLibrary.UI.DAL
{
    public static class LibrarianRepository
    {
        private static Dictionary<Guid, Client> Clients
        {
            get
            {
                // me miro el campo interno con el diccionario y si está vació
                if (_Clients == null)
                {
                    //entonces lo inicializo
                    _Clients = new Dictionary<Guid, Client>();

                    //creo Clients de prueba
                    var std1 = new Client()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Pepe",
                        Email = "p@p.com",
                        Dni = "12345678a"
                    };
                    var std2 = new Client()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Marta",
                        Email = "m@m.com",
                        Dni = "12345678b"
                    };

                    // y lo meto en el campo interno
                    _Clients.Add(std1.Id, std1);
                    _Clients.Add(std2.Id, std2);
                }

                //devuelvo el campo interno
                return _Clients;
            }
        }
        static Dictionary<Guid, Client> _Clients;

        public static List<Client> ClientsList
        {
            get
            {
                return GetAll();
            }
        }

        public static List<Client> GetAll()
        {
            // opción 1: a pelo

            // creo un objeto de tipo List de Client
            // que vamos a devolver
            //var output = new List<Client>();

            ////recorremos cada par de key/value en el diccionario
            //foreach (var item in Clients)
            //{
            //    // y sacamos su valor (objeto de tipo Client)
            //    // y lo metemos en la lista de salida
            //    var Client = item.Value;
            //    output.Add(Client);
            //}

            //// devolvemos la lista
            //return output;

            // opción 2: short cut para lo de arriba
            return Clients.Values.ToList();
        }

        public static Client Get(Guid id)
        {
            // si existe un Client con es id en la DB me lo devuelve
            if (Clients.ContainsKey(id))
                return Clients[id];
            else
                // si no, me devuelve el Client por defecto,
                // prob un null
                return default(Client);
        }

        public static Client GetByDni(string dni)
        {
            foreach (var item in Clients)
            {
                var Client = item.Value;
                if (Client.Dni == dni)
                    return Client;
            }

            return default(Client);
        }

        public static List<Client> GetByName(string name)
        {
            var output = new List<Client>();

            foreach (var item in Clients)
            {
                var Client = item.Value;

                if (Client.Name == name)
                    output.Add(Client);
            }

            // devolvemos la lista
            return output;
        }

        public static ClientValidationsTypes Add(Client Client)
        {
            if (Client.Id != default(Guid))
            {
                // todo bien porque no hay ningún Id
                Client.Id = Guid.NewGuid();

                return ClientValidationsTypes.IdNotEmpty;
            }
            else if (!Client.ValidateDniFormat(Client.Dni))
            {
                //el dni está mal construido
                return ClientValidationsTypes.WrongDniFormat;
            }
            else
            {
                var stdWithSameDni = GetByDni(Client.Dni);
                if (stdWithSameDni != null && Client.Id != stdWithSameDni.Id)
                {
                    // hay dos estudiantes distintos con mismo dni
                    return ClientValidationsTypes.DniDuplicated;
                }
            }

            if (!Client.ValidateName(Client.Name))
            {
                return ClientValidationsTypes.WrongNameFormat;
            }
            else if (!Clients.ContainsKey(Client.Id))
            {
                // si Client tiene un Id y no hay ninguno
                // en la base de datos con ese Id
                // lo permitimos, con lo cual sólo hay
                // que añadir el estudiante a la base de datos
                Clients.Add(Client.Id, Client);

                return ClientValidationsTypes.Ok;
            }

            // si el Id no estaba vacío y ya existía en la DB devolvmenos false
            return ClientValidationsTypes.IdDuplicated;
        }

        public static ClientValidationsTypes Update(Client Client)
        {
            if (Client.Id == default(Guid))
            {
                // no se puede actualizar un registro sin id
                return ClientValidationsTypes.IdEmpty;
            }
            if (!Clients.ContainsKey(Client.Id))
            {
                // no se puede actualizar un registro
                // que no exista en la DB
                return ClientValidationsTypes.ClientNotFound;
            }

            // comprobamos que el dni sea correcto
            if (!Client.ValidateDniFormat(Client.Dni))
            {
                //el dni está mal construido
                return ClientValidationsTypes.WrongDniFormat;
            }


            // comprobamos que no haya otro alumno diferente
            // con el mismo dni

            var stdWithSameDni = GetByDni(Client.Dni);
            if (stdWithSameDni != null && Client.Id != stdWithSameDni.Id)
            {
                // hay dos estudiantes distintos con mismo dni
                return ClientValidationsTypes.DniDuplicated;
            }

            if (!Client.ValidateName(Client.Name))
            {
                return ClientValidationsTypes.WrongNameFormat;
            }

            Clients[Client.Id] = Client;

            return ClientValidationsTypes.Ok;
        }

        public static ClientValidationsTypes Delete(Guid id)
        {
            if (Clients.ContainsKey(id))
            {
                Clients.Remove(id);
                return ClientValidationsTypes.ClientNotFound;
            }
            else
                return ClientValidationsTypes.Ok;
        }

    }
}
