#nullable disable

using Atawiz.UnitTestDemo.Core.Models.Base;

namespace Atawiz.UnitTestDemo.Core.Models;

public class TodoItem : BaseModel
{

    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime DueDateUtc { get; set; }

    public string Assignee { get; set; }
}