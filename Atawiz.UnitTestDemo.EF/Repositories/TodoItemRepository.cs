using Atawiz.UnitTestDemo.Core.Models;
using Atawiz.UnitTestDemo.Core.Repositories;
using Atawiz.UnitTestDemo.EF.Context;
using Atawiz.UnitTestDemo.EF.Repositories.Generic;
using Microsoft.EntityFrameworkCore;

namespace Atawiz.UnitTestDemo.EF.Repositories
{
    public class TodoItemRepository : BaseRepository<TodoItem>, ITodoItemRepository
    {
        public TodoItemRepository(MainDbContext context) : base(context)
        {

        }
        
        
        public async Task<TodoItem?> FindByTitleAndAssigneeAsync(string title, string assignee)
        {
            return await _context.TodoItems.FirstOrDefaultAsync(x => x.Title == title && x.Assignee == assignee);
        }
    }
}
