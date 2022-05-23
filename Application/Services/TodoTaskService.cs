using Application.DTOs.TodoTask;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Data.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TodoTaskService : ITodoTaskService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public TodoTaskService(IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<TodoTaskDTO> CreateTodoTask(CreateTodoTaskDTO createTodoTaskDTO)
        {
            var todoTask = _mapper.Map<TodoTask>(createTodoTaskDTO);

            _context.TodoTasks.Add(todoTask);
            await _context.SaveChangesAsync();

            var todoTaskDTO = _mapper.Map<TodoTaskDTO>(todoTask);

            return todoTaskDTO;
        }

        public async Task DeleteTodoItem(int id)
        {
            var todoTask = _context.TodoTasks.FirstOrDefault(x => x.Id == id);

            if (todoTask == null)
            {
                throw new NotFoundException($"TodoTask of id: {id} was not found");
            }

            _context.TodoTasks.Remove(todoTask);
            await _context.SaveChangesAsync();
        }

        public async Task<TodoTaskDTO> GetTodoTask(int id)
        {
            var todoTask = await _context.TodoTasks.FirstOrDefaultAsync(x => x.Id == id);

            if (todoTask == null)
            {
                throw new NotFoundException($"TodoTask of id: {id} was not found");
            }

            var taskDTO = _mapper.Map<TodoTaskDTO>(todoTask);

            return taskDTO;
        }

        public async Task<IEnumerable<TodoTaskDTO>> GetTodoTasks()
        {
            var todoTasks = await _context.TodoTasks.ToListAsync();

            var todoTaskDTOs = _mapper.Map<IEnumerable<TodoTaskDTO>>(todoTasks);

            return todoTaskDTOs;
        }

        public async Task<TodoTaskDTO> MarkTaskAsDone(int id)
        {
            var todoTask = _context.TodoTasks.FirstOrDefault(x => x.Id == id);

            if (todoTask == null)
            {
                throw new NotFoundException($"TodoTask of id: {id} was not found");
            }

            todoTask.CompletePercent = 100;

            await _context.SaveChangesAsync();

            var todoTaskDTO = _mapper.Map<TodoTaskDTO>(todoTask);

            return todoTaskDTO;
        }

        public async Task<TodoTaskDTO> UpdateTodoTask(UpdateTodoTaskDTO updateTodoTaskDTO, int id)
        {
            var todoTask = await _context.TodoTasks.FirstOrDefaultAsync(x => x.Id == id);

            if (todoTask == null)
            {
                throw new NotFoundException($"TodoTask of id: {id} was not found");
            }

            todoTask = _mapper.Map<TodoTask>(updateTodoTaskDTO);

            await _context.SaveChangesAsync();

            var taskDTO = _mapper.Map<TodoTaskDTO>(todoTask);

            return taskDTO;
        }
    }
}
