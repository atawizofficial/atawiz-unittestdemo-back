namespace Atawiz.UnitTestDemo.Core.Exceptions
{
    public sealed class AlreadyExistingException<T> : Exception where T : class
    {
        public AlreadyExistingException(string title, string assignee) : base($"The {typeof(T).Name} with title {title} assigned to {assignee} already exists.")
        { }
    }
}
