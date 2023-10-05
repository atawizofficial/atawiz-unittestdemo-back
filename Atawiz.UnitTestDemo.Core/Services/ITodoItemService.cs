using Atawiz.UnitTestDemo.Core.Dtos;

namespace Atawiz.UnitTestDemo.Core.Services
{
    public interface ITodoItemService
    {
        Task<IEnumerable<TodoItemDto>> GetAllAsync();
        Task<TodoItemDto> GetByIdAsync(int id);
        Task<TodoItemDto> CreateAsync(TodoItemDto todoItem);
        Task<TodoItemDto> UpdateAsync(int id, TodoItemDto todoItem);
        Task<int> DeleteAsync(int id);
    }
}
