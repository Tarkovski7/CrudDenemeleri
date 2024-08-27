using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudDenemeleri.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudDenemeleri.Context
{
    public class PersonDbContext : DbContext
    {
        public PersonDbContext(DbContextOptions<PersonDbContext> options) : base(options) { }

        public DbSet<Person> Persons { get; set; }
        
    }
}