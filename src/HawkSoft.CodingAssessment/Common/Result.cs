using System;
using System.Collections.Generic;
using System.Text;

namespace HawkSoft.CodingAssessment.Common
{
    public interface IResult
    {
        bool Success { get; }
        IEnumerable<string> FailureMessages { get; }
        Exception Exception { get; }

        bool IsException { get; }
    }

    public interface IResult<T> : IResult
    {
        T Value { get; }
    }

    public class Result : IResult
    {
        public bool Success { get; protected set; }

        public IEnumerable<string> FailureMessages { get; protected set; }

        public Exception Exception { get; protected set; }

        public bool IsException => Exception != null;

        protected Result()
        {
            Success = true;
        }

        protected Result(IEnumerable<string> messages)
        {
            Success = false;
            FailureMessages = messages;
        }

        protected Result(Exception exception)
        {
            Success = false;
            Exception = exception;
        }

        public static Result SuccessResult() => new Result();
        public static Result FailureResult(IEnumerable<string> messages) => new Result(messages);
        public static Result FailureResult(string message) => new Result(new[] { message });
        public static Result ExceptionResult(Exception exception) => new Result(exception);
    }

    public class Result<T> : IResult<T>
    {
        public bool Success { get; protected set; }

        public IEnumerable<string> FailureMessages { get; protected set; }

        public Exception Exception { get; protected set; }

        public bool IsException => Exception != null;
        public T Value { get; protected set; }

        protected Result(T value)
        {
            Value = value;
            Success = true;
        }
        protected Result(IEnumerable<string> messages)
        {
            Success = false;
            FailureMessages = messages;
        }

        protected Result(Exception exception)
        {
            Success = false;
            Exception = exception;
        }

        public static Result<T> SuccessResult(T value) => new Result<T>(value);
        public static Result<T> FailureResult(IEnumerable<string> messages) => new Result<T>(messages);
        public static Result<T> FailureResult(string message) => new Result<T>(new[] { message });
        public static Result<T> ExceptionResult(Exception exception) => new Result<T>(exception);

    }
}
