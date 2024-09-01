using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudDenemeleri.Models;
using FluentValidation;

namespace CrudDenemeleri.Validations
{
    public class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            RuleFor(n => n.Name).NotEmpty().WithMessage("İsim alanı boş bırakılamaz.");
            RuleFor(n => n.Name).MinimumLength(2).WithMessage("İsim alanı 2 harften az olamaz.");
            RuleFor(n => n.Name).MaximumLength(25).WithMessage("İsim alanı 25 harften fazla olamaz.");


            RuleFor(n => n.SurName).NotEmpty().WithMessage("Soyisim alanı boş bırakılamaz.");
            RuleFor(n => n.SurName).MinimumLength(2).WithMessage("Soyisim alanı 2 harften az olamaz.");
            RuleFor(n => n.SurName).MaximumLength(25).WithMessage("Soyisim alanı 25 harften fazla olamaz.");

            RuleFor(m => m.Mail).NotEmpty().EmailAddress().WithMessage("Geçerli bir e-posta adresi girin.");

        }
    }
}