using Application.DTOs.TodoTask;
using Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITodoTaskService
    {
        Task<IEnumerable<TodoTaskDTO>> GetTodoTasks();
        Task<TodoTaskDTO> GetTodoTask(int id);
        Task<TodoTaskDTO> CreateTodoTask(CreateTodoTaskDTO createTodoTaskDTO);
        Task<TodoTaskDTO> UpdateTodoTask(UpdateTodoTaskDTO updateTodoTaskDTO, int id);
        Task DeleteTodoTask(int id);
        Task<TodoTaskDTO> MarkTaskAsDone(int id);
        Task<TodoTaskDTO> SetTodoTaskPercentComplete(
            int id,
            SetPercentTodoTaskDTO setPercentTodoTaskDTO
        );
        Task<IEnumerable<TodoTaskDTO>> GetIncomingTodoTasks(TimePeriod timePeriod);
    }
}
