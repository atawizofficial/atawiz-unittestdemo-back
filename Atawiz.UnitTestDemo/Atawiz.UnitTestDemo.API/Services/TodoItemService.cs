using Atawiz.UnitTestDemo.Core.Dtos;
using Atawiz.UnitTestDemo.Core.Exceptions;
using Atawiz.UnitTestDemo.Core.Models;
using Atawiz.UnitTestDemo.Core.Repositories;
using Atawiz.UnitTestDemo.Core.Services;

namespace Atawiz.UnitTestDemo.API.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ITodoItemRepository _todoItemRepository;
        public TodoItemService(ITodoItemRepository todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }

        public async Task<IEnumerable<TodoItemDto>> GetAllAsync()
        {
            IEnumerable<TodoItem> todoItems = await _todoItemRepository.FindAllAsync();
            return todoItems.Select(x => new TodoItemDto
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                DueDateUtc = x.DueDateUtc,
                Assignee = x.Assignee
            });
        }

        public async Task<TodoItemDto> GetByIdAsync(int id)
        {
            TodoItem? todoItem = await _todoItemRepository.FindByIdAsync(id);

            if (todoItem is null)
                throw new NotFoundException<TodoItem>(id);

            return new TodoItemDto
            {
                Id = todoItem.Id,
                Title = todoItem.Title,
                Description = todoItem.Description,
                DueDateUtc = todoItem.DueDateUtc,
                Assignee = todoItem.Assignee
            };
        }

        public async Task<TodoItemDto> CreateAsync(TodoItemDto todoItemDto)
        {
            string title = todoItemDto.Title;
            string assignee = todoItemDto.Assignee;

            TodoItem? existingTodoItem = await _todoItemRepository.FindByTitleAndAssigneeAsync(title, assignee);

            if (existingTodoItem is not null)
                throw new AlreadyExistingException<TodoItem>(title, assignee);

            TodoItem todoItem = new TodoItem
            {
                Title = todoItemDto.Title,
                Description = todoItemDto.Description,
                DueDateUtc = todoItemDto.DueDateUtc,
                Assignee = todoItemDto.Assignee
            };

            TodoItem createdTodoItem = await _todoItemRepository.AddAsync(todoItem);

            return new TodoItemDto
            {
                Id = createdTodoItem.Id,
                Title = createdTodoItem.Title,
                Description = createdTodoItem.Description,
                DueDateUtc = createdTodoItem.DueDateUtc,
                Assignee = createdTodoItem.Assignee
            };
        }
        
        public async Task<TodoItemDto> UpdateAsync(int id, TodoItemDto todoItemDto)
        {
            TodoItem? todoItem = await _todoItemRepository.FindByIdAsync(id);

            if (todoItem is null)
                throw new NotFoundException<TodoItem>(id);

            TodoItem? otherTdoItem = await _todoItemRepository.FindByTitleAndAssigneeAsync(todoItemDto.Title, todoItemDto.Assignee);

            if (otherTdoItem is not null && otherTdoItem.Id != id)
                    throw new AlreadyExistingException<TodoItem>(todoItemDto.Title, todoItemDto.Assignee);

            todoItem.Title = todoItemDto.Title;
            todoItem.Description = todoItemDto.Description;
            todoItem.DueDateUtc = todoItemDto.DueDateUtc;
            todoItem.Assignee = todoItemDto.Assignee;
            await _todoItemRepository.UpdateAsync(todoItem);

            return new TodoItemDto
            {
                Id = todoItem.Id,
                Title = todoItem.Title,
                Description = todoItem.Description,
                DueDateUtc = todoItem.DueDateUtc,
                Assignee = todoItem.Assignee
            };
        }


        public async Task<int> DeleteAsync(int id)
        {
            TodoItem? existingTodoItem = await _todoItemRepository.FindByIdAsync(id);

            if (existingTodoItem is null)
                throw new NotFoundException<TodoItem>(id);

            await _todoItemRepository.DeleteAsync(existingTodoItem);

            return id;
        }

    }
}
