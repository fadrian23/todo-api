using Application.DTOs.TodoTask;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Application.MappingProfiles
{
    public class TodoTaskProfile : Profile
    {
        public TodoTaskProfile()
        {
            CreateMap<CreateTodoTaskDTO, TodoTask>().ReverseMap();
            CreateMap<TodoTaskDTO, TodoTask>().ReverseMap();
            CreateMap<UpdateTodoTaskDTO, TodoTask>().ReverseMap();
        }
    }
}
