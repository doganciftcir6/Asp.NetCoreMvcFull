using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.TodoAppNTier.Dtos.WorkDtos;

namespace Udemy.TodoAppNTier.Business.ValidationRules
{
    public class WorkCreateDtoValidator : AbstractValidator<WorkCreateDto>
    {
        public WorkCreateDtoValidator()
        {
            //RuleFor(x => x.Definition).NotEmpty().WithMessage("Defination is required.").When(x=>x.IsCompleted == true).Must(NotBeYavuz).WithMessage("Defination yavuz olamaz.");
            RuleFor(x => x.Definition).NotEmpty();
            
        }

        //private bool NotBeYavuz(string arg)
        //{
        //    return arg != "Yavuz" && arg != "yavuz";
        //}
    }
}
