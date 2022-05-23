using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.TodoTask
{
    public class UpdateTodoTaskDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTimeOffset ExpirationDate { get; set; }
        public int CompletePercent { get; set; }
    }

    public class UpdateTodoTaskDTOValidator : AbstractValidator<UpdateTodoTaskDTO>
    {
        public UpdateTodoTaskDTOValidator()
        {
            RuleFor(x => x.Title).NotNull();
            RuleFor(x => x.ExpirationDate).NotNull();
            RuleFor(x => x.CompletePercent).InclusiveBetween(0, 100);
        }
    }
}
