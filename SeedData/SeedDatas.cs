using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudDenemeleri.Context;
using CrudDenemeleri.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudDenemeleri.SeedData
{
    public static class SeedDatas
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (
                var context = new PersonDbContext(
                    serviceProvider.GetRequiredService<DbContextOptions<PersonDbContext>>()
                )
            )
            {
                context.Database.Migrate();
                if (!context.Persons.Any())
                {
                    context.Persons.AddRange(
                    new Person {Name = "John", SurName = "Doe", Mail = "john.doe@gmail.com", BirthDate = new DateOnly(1985, 5, 5) },
                    new Person {Name = "Jane", SurName = "Smith", Mail = "jane.smith@gmail.com", BirthDate = new DateOnly(1990, 7, 10) },
                    new Person {Name = "Alice", SurName = "Johnson", Mail = "alice.johnson@gmail.com", BirthDate = new DateOnly(1980, 3, 15) },
                    new Person {Name = "Bob", SurName = "Brown", Mail = "bob.brown@gmail.com", BirthDate = new DateOnly(1975, 8, 20) },
                    new Person {Name = "Charlie", SurName = "Davis", Mail = "charlie.davis@gmail.com", BirthDate = new DateOnly(2000, 12, 25) }
                );
                context.SaveChanges();
                }
            }
        }
    }
}
