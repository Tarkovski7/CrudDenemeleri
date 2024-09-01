using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CrudDenemeleri.Models
{
    public class Person
    {
        
        public int Id { get; set; }
        // [Required]
        public string Name { get; set; }
        // [Required]
        public string SurName { get; set; }
        // [Required]
        [EmailAddress]
        public string Mail { get; set; }
        // [Required]
        public DateOnly BirthDate { get; set; }
    }
}