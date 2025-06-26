namespace Core.Utilities.Result
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        public DataResult(bool success) : this(default, success) { }
        public DataResult(T data, bool success) : this(data, success, string.Empty) { }
        public DataResult(T data, bool success, string message) : base(success, message)
        {
            Data = data;
        }

        public T Data { get; }
    }
}
