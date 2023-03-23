using FluentValidation;
using System;
using Udemy.AdvertisementApp.UI.Models;

namespace Udemy.AdvertisementApp.UI.ValidationRules
{
    //Burası Modeller için Validator.
    public class UserCreateModelValidator : AbstractValidator<UserCreateModel>
    {
        //[Obsolete]
        public UserCreateModelValidator()
        {
            //CascadeMode = CascadeMode.StopOnFirstFailure;
            RuleFor(x => x.Password).NotEmpty().WithMessage("Parola boş olamaz.");
            RuleFor(x => x.Password).MinimumLength(3).WithMessage("Parola min 3 karakter olmalıdır.");
            RuleFor(x => x.Password).Equal(x => x.ConfirmPassword).WithMessage("Parolalar eşleşmiyor.");
            RuleFor(x => x.Firstname).NotEmpty().WithMessage("Ad boş olamaz.");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Soyad boş olamaz.");
            RuleFor(x => x.Username).NotEmpty().WithMessage("Kullanıcı adı boş olamaz");
            RuleFor(x => x.Username).MinimumLength(3).WithMessage("Kullanıcı adı min 3 karakter olmalıdır.");
            RuleFor(x => new
            {
                x.Username,
                x.Firstname
            }).Must(x => CanNotFirstName(x.Username, x.Firstname)).WithMessage("Kullanıcı adı, adınızı içeremez!").When(x=> x.Username!=null && x.Firstname!=null);
            RuleFor(x => x.GenderId).NotEmpty().WithMessage("Cinsiyet seçimi zorunludur.");
       
        }

        private bool CanNotFirstName(string userName, string firstName)
        {
            return !userName.Contains(firstName);
        }
    }
}
