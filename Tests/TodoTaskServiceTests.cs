using Application.DTOs.TodoTask;
using Application.Exceptions;
using Application.Interfaces;
using Application.MappingProfiles;
using Application.Services;
using AutoMapper;
using Data.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class TodoTaskServiceTests
    {
        private readonly ITodoTaskService _sut;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TodoTaskServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDB")
                .EnableSensitiveDataLogging()
                .Options;

            _context = new ApplicationDbContext(options);

            var mapperProfile = new TodoTaskProfile();
            var configuration = new MapperConfiguration(c => c.AddProfile(mapperProfile));
            _mapper = new Mapper(configuration);

            _sut = new TodoTaskService(_mapper, _context);
        }

        [Fact]
        public async Task CreateTodoTask_ShouldAddTaskToDatabase()
        {
            // Arrange
            var task = new CreateTodoTaskDTO
            {
                Title = "Mow the lawn",
                Description = "Mow the lawn in front of the house",
                ExpirationDate = DateTime.UtcNow.AddDays(2),
            };

            // Act
            var createdTask = await _sut.CreateTodoTask(task);

            // Assert
            Assert.True(_context.TodoTasks.Any(x => x.Id == createdTask.Id));
        }

        [Fact]
        public async Task DeleteTodoTask_ShouldRemoveTaskFromDatabase()
        {
            //Arrange
            var task = new TodoTask
            {
                Title = "Mow the lawn",
                Description = "Mow the lawn in front of the house",
                ExpirationDate = DateTime.UtcNow.AddDays(2),
            };

            _context.TodoTasks.Add(task);
            _context.SaveChanges();
            Assert.True(_context.TodoTasks.Any(x => x.Id == task.Id));

            // Act

            await _sut.DeleteTodoTask(task.Id);

            // Assert
            Assert.False(_context.TodoTasks.Any(x => x.Id == task.Id));
        }

        [Fact]
        public async Task GetTodoTask_ShouldReturnTask_IfTaskExists()
        {
            // Arrange
            var task = new TodoTask
            {
                Title = "Mow the lawn",
                Description = "Mow the lawn in front of the house",
                ExpirationDate = DateTime.UtcNow.AddDays(2),
            };

            _context.TodoTasks.Add(task);
            _context.SaveChanges();
            Assert.True(_context.TodoTasks.Any(x => x.Id == task.Id));

            // Act

            var expectedTask = await _sut.GetTodoTask(task.Id);

            // Assert

            Assert.True(expectedTask.Id == task.Id);
        }

        [Fact]
        public async Task GetTodoTask_ShouldThrowNotFoundException_IfTaskDoesNotExist()
        {
            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await _sut.DeleteTodoTask(-1));
        }

        [Fact]
        public async Task GetTodoTasks_ShouldReturnAllTasks()
        {
            // Arrange
            var task = new TodoTask
            {
                Title = "Mow the lawn",
                Description = "Mow the lawn in front of the house",
                ExpirationDate = DateTime.UtcNow.AddDays(2),
            };

            var task2 = new TodoTask
            {
                Title = "Do the dishes",
                Description = "Do the dishes in the kitchen",
                ExpirationDate = DateTime.UtcNow.AddDays(1),
            };

            _context.AddRange(new[] { task, task2 });
            _context.SaveChanges();

            var tasksInDbCount = _context.TodoTasks.Count();

            // Act
            var expectedResult = await _sut.GetTodoTasks();

            // Assert
            Assert.Equal(expectedResult.Count(), tasksInDbCount);
        }

        [Fact]
        public async Task MarkTaskAsDone_ShouldSetCompletePercentTo100_OfTaskOfGivenId()
        {
            // Arrange
            var task = new TodoTask
            {
                Title = "Mow the lawn",
                Description = "Mow the lawn in front of the house",
                ExpirationDate = DateTime.UtcNow.AddDays(2),
                CompletePercent = 50,
            };

            _context.TodoTasks.Add(task);
            _context.SaveChanges();

            // Act
            await _sut.MarkTaskAsDone(task.Id);

            // Assert
            Assert.Equal(100, task.CompletePercent);
        }

        [Fact]
        public async Task MarkTaskAsDone_ShouldThrowNotFoundException_IfTaskOfGivenIdDoesNotExist()
        {
            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await _sut.MarkTaskAsDone(-1));
        }

        [Fact]
        public async Task SetTodoTaskPercentComplete_ShouldSetPercentComplete()
        {
            // Arrange
            var task = new TodoTask
            {
                Title = "Mow the lawn",
                Description = "Mow the lawn in front of the house",
                ExpirationDate = DateTime.UtcNow.AddDays(2),
                CompletePercent = 20,
            };

            _context.TodoTasks.Add(task);
            _context.SaveChanges();

            var dto = new SetPercentTodoTaskDTO { CompletePercent = 80, };

            // Act
            await _sut.SetTodoTaskPercentComplete(task.Id, dto);

            // Assert
            Assert.Equal(task.CompletePercent, dto.CompletePercent);
        }
    }
}
