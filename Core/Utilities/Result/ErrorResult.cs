﻿namespace Core.Utilities.Result
{
    public class ErrorResult : Result
    {
        public ErrorResult(string message) : base(false, message) { }
        public ErrorResult() : base(false, string.Empty) { }
    }
}
