using Atawiz.UnitTestDemo.Core.Models;
using Atawiz.UnitTestDemo.Core.Repositories.Generic;

namespace Atawiz.UnitTestDemo.Core.Repositories
{
    public interface ITodoItemRepository : IBaseRepository<TodoItem>
    {
        Task<TodoItem?> FindByTitleAndAssigneeAsync(string title, string assignee);
    }
}
