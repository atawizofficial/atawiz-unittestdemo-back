namespace Atawiz.UnitTestDemo.Core.Exceptions
{
    public sealed class NotFoundException<T> : Exception where T : class
    {
        public NotFoundException() : base($"The queried {typeof(T).Name} doesn't exist.")
        { }
    }
}
