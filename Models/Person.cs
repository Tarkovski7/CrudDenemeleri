using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CrudDenemeleri.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public MailAddress Mail { get; set; }
        public DateOnly BirthDate { get; set; }
    }
}