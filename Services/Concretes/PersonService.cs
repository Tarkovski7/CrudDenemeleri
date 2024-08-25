using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using CrudDenemeleri.Models;
using CrudDenemeleri.Services.Interfaces;

namespace CrudDenemeleri.Services.Concretes
{
    public class PersonService : IPersonService
    {
        private static List<Person> people = new List<Person>{
                new Person{Id=1 , Name= "Tarık Buğra" , SurName="Kaya" , Mail= new MailAddress("tarik.kaya@gmail.com") , BirthDate= new DateOnly(2000,07,17)},
                new Person{Id=2 , Name= "Talha Tuğra" , SurName="Kaya" , Mail= new MailAddress("talha.kaya@gmail.com") , BirthDate= new DateOnly(2005,03,26)}
            };
        public PersonService()
        {

        }
        public void Add(Person person)
        {
            people.Add(person);
        }

        public void Delete(int id)
        {
            Person person = people.FirstOrDefault(p => p.Id == id);
            people.Remove(person);
        }

        public List<Person> GetAll()
        {
            return people;
        }

        public Person GetById(int id)
        {
            return people.FirstOrDefault(p => p.Id == id);
        }

        public void Update(Person person)
        {
            Person updatedPerson = people.FirstOrDefault(p => p.Id == person.Id);
            updatedPerson.Name = person.Name;
            updatedPerson.SurName = person.SurName;
            updatedPerson.BirthDate = person.BirthDate;
            updatedPerson.Mail = person.Mail;

        }
    }
}