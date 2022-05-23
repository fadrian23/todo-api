using Application.DTOs.TodoTask;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TodoTaskController : ControllerBase
    {
        private readonly ITodoTaskService _todoTaskService;

        public TodoTaskController(ITodoTaskService todoTaskService)
        {
            _todoTaskService = todoTaskService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoTaskDTO>>> Get()
        {
            var todoTaskDTOs = await _todoTaskService.GetTodoTasks();

            return Ok(todoTaskDTOs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoTaskDTO>> Get(int id)
        {
            var todoTaskDTO = await _todoTaskService.GetTodoTask(id);

            return Ok(todoTaskDTO);
        }

        [HttpPost]
        public async Task<ActionResult<TodoTaskDTO>> Create(CreateTodoTaskDTO createTodoTaskDTO)
        {
            var todoTaskDTO = await _todoTaskService.CreateTodoTask(createTodoTaskDTO);

            return CreatedAtAction(nameof(Get), new { Id = todoTaskDTO.Id }, todoTaskDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, UpdateTodoTaskDTO updateTodoTaskDTO)
        {
            await _todoTaskService.UpdateTodoTask(updateTodoTaskDTO, id);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _todoTaskService.DeleteTodoItem(id);

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<TodoTaskDTO>> MarkAsDone(int id)
        {
            await _todoTaskService.MarkTaskAsDone(id);

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<TodoTaskDTO>> SetPercentComplete(
            int id,
            SetPercentTodoTaskDTO setPercentTodoTaskDTO
        )
        {
            await _todoTaskService.SetTodoTaskPercentComplete(id, setPercentTodoTaskDTO);

            return NoContent();
        }
    }
}
