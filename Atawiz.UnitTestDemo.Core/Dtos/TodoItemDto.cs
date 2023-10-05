#nullable disable

namespace Atawiz.UnitTestDemo.Core.Dtos
{
    public class TodoItemDto
    {
        public int? Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime DueDateUtc { get; set; }

        public string Assignee { get; set; }
    }
}
