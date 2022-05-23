using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.TodoTask
{
    public class SetPercentTodoTaskDTO
    {
        public int CompletePercent { get; set; }
    }

    public class SetPercentTodoTaskDTOValidator : AbstractValidator<SetPercentTodoTaskDTO>
    {
        public SetPercentTodoTaskDTOValidator()
        {
            RuleFor(x => x.CompletePercent).InclusiveBetween(1, 100);
        }
    }
}
