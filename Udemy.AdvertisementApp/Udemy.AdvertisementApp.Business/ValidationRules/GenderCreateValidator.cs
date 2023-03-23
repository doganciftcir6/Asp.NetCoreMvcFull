using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.AdvertisementApp.Dtos;

namespace Udemy.AdvertisementApp.Business.ValidationRules
{
    public class GenderCreateValidator : AbstractValidator<GenderCreateDto>
    {
        public GenderCreateValidator()
        {
            RuleFor(x => x.Definition).NotEmpty();
        }
    }
}
