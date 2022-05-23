using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.TodoTask
{
    public class TodoTaskDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTimeOffset ExpirationDate { get; set; }
        public int CompletePercent { get; set; }
        public bool Done
        {
            get { return CompletePercent == 100; }
        }
    }
}
