using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.TodoAppNTier.Dtos.WorkDtos;

namespace Udemy.TodoAppNTier.Business.ValidationRules
{
    public class WorkUpdateValidator : AbstractValidator<WorkUpdateDto>
    {
        public WorkUpdateValidator()
        {
            RuleFor(x => x.Definition).NotEmpty();
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
