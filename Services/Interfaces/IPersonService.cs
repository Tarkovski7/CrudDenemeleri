using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudDenemeleri.Models;

namespace CrudDenemeleri.Services.Interfaces
{
    public interface IPersonService
    {
        List<Person> GetAll();
        Person GetById(int id);
        public void Add(Person person);
        public void Update(Person person);
        public void Delete(int id);

    }
}