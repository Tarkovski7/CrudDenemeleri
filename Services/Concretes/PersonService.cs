using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using CrudDenemeleri.Context;
using CrudDenemeleri.Models;
using CrudDenemeleri.Services.Interfaces;

namespace CrudDenemeleri.Services.Concretes
{
    public class PersonService : IPersonService
    {
        private readonly PersonDbContext context;
    
        public PersonService(PersonDbContext context)
        {
            this.context = context;
        }
        public void Add(Person person)
        {
            context.Persons.Add(person);
            SaveChanges();
        }

        private void SaveChanges()
        {
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var person = context.Persons.FirstOrDefault(p=> p.Id == id);
            context.Persons.Remove(person);
            SaveChanges();
        }

        public List<Person> GetAll()
        {
            return context.Persons.ToList();
        }

        public Person GetById(int id)
        {
            return context.Persons.FirstOrDefault(p => p.Id == id);
        }

        public void Update(Person person)
        {
            context.Persons.Update(person);
            SaveChanges();
        }
    }
}