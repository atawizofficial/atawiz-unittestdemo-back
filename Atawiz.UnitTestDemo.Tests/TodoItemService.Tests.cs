using Atawiz.UnitTestDemo.API.Services;
using Atawiz.UnitTestDemo.Core.Dtos;
using Atawiz.UnitTestDemo.Core.Exceptions;
using Atawiz.UnitTestDemo.Core.Models;
using Atawiz.UnitTestDemo.Core.Repositories;
using Atawiz.UnitTestDemo.Core.Services;
using Atawiz.UnitTestDemo.EF.Context;
using Atawiz.UnitTestDemo.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Atawiz.UnitTestDemo.Tests
{
    public class TodoItemServiceTests
    {

        private readonly ITodoItemRepository _repository;
        private readonly ITodoItemService _service;
        private readonly DbContextOptions<MainDbContext> _dbOptions;
        private readonly MainDbContext dbContext;

        private List<TodoItem> todoItems = new List<TodoItem>
        {
            new TodoItem {Id = 1, Title = "Title 1", Description = "Description 1", DueDateUtc = new DateTime(2023, 10, 9, 1, 0, 0), Assignee = "Assignee 1"},
            new TodoItem {Id = 2, Title = "Title 2", Description = "Description 2", DueDateUtc = new DateTime(2023, 10, 9, 2, 0, 0), Assignee = "Assignee 2"},
            new TodoItem {Id = 3, Title = "Title 3", Description = "Description 3", DueDateUtc = new DateTime(2023, 10, 9, 3, 0, 0), Assignee = "Assignee 3"},
        };

        public TodoItemServiceTests()
        {
            _dbOptions = new DbContextOptionsBuilder<MainDbContext>()
                .UseInMemoryDatabase(databaseName: $"unittestdemo{Guid.NewGuid()}")
                .Options;

            dbContext = new MainDbContext(_dbOptions);

            _repository = new TodoItemRepository(dbContext);

            _service = new TodoItemService(_repository);

            dbContext.AddRange(todoItems);
            dbContext.SaveChanges();
        }

        [Fact]
        public async Task AddTodoItem_ShouldReturnCreatedItem_WhenSuccess()
        {
            // Arrange
            TodoItemDto todoItemDto = new TodoItemDto
            {
                Title = "Title 4",
                Description = "Description 4",
                DueDateUtc = new DateTime(2023, 10, 9, 4, 0, 0),
                Assignee = "Assignee 4"
            };

            // Act
            TodoItemDto result = await _service.CreateAsync(todoItemDto);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task AddTodoItem_ReturnedIdShouldBeGreaterThanMaxId_WhenSuccess()
        {
            // Arrange
            TodoItemDto todoItemDto = new TodoItemDto
            {
                Title = "Title 4",
                Description = "Description 4",
                DueDateUtc = new DateTime(2023, 10, 9, 4, 0, 0),
                Assignee = "Assignee 4"
            };

            int maxId = todoItems.Max(x => x.Id);

            // Act
            TodoItemDto result = await _service.CreateAsync(todoItemDto);

            // Assert
            Assert.True(result.Id > maxId);
        }

        [Fact]
        public async Task AddTodoItem_ReturnedItemShouldHaveSameProperties_WhenSuccess()
        {
            // Arrange
            TodoItemDto todoItemDto = new TodoItemDto
            {
                Title = "Title 4",
                Description = "Description 4",
                DueDateUtc = new DateTime(2023, 10, 9, 4, 0, 0),
                Assignee = "Assignee 4"
            };

            // Act
            TodoItemDto result = await _service.CreateAsync(todoItemDto);

            // Assert
            Assert.Equal(todoItemDto.Title, result.Title);
            Assert.Equal(todoItemDto.Description, result.Description);
            Assert.Equal(todoItemDto.DueDateUtc, result.DueDateUtc);
            Assert.Equal(todoItemDto.Assignee, result.Assignee);
        }

        [Fact]
        public async Task AddTodoItem_ShouldThrowAlreadyExistingException_WhenItemExists()
        {
            // Arrange
            TodoItemDto todoItemDto = new TodoItemDto
            {
                Title = "Title 3",
                Description = "Description 3",
                DueDateUtc = new DateTime(2023, 10, 9, 3, 0, 0),
                Assignee = "Assignee 3"
            };

            // Act & assert
            await Assert.ThrowsAsync<AlreadyExistingException<TodoItem>>(() => _service.CreateAsync(todoItemDto));
        }
    }
}