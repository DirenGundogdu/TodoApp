using Dtos.WorkDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules
{
    public class WorkCreateDtoValidator : AbstractValidator<WorkCreateDto>
    {
        public WorkCreateDtoValidator()
        {
            RuleFor(x => x.Definition).NotEmpty();
            //RuleFor(x => x.Definition).NotEmpty().WithMessage("Definition is required").When(x =>x.IsCompleted).Must(TestMethod).WithMessage("Not Test");
        }

        //private bool TestMethod(string arg)
        //{
        //    return arg != "Test" && arg != "test";
        //}
    }
}
