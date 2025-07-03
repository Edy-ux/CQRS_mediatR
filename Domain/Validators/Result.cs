

namespace CQRS_mediatR.Domain.Validators
{
    public class Result<T>
    {
        public bool IsValid { get; }
        public T Value { get; }
        public string Error { get; }

        private Result(bool isValid, T value, string error)
        {
            IsValid = isValid;
            Value = value;
            Error = error;
        }

        public static Result<T> Success(T value) => new Result<T>(true, value, null!);
        public static Result<T> Failure(string error) => new Result<T>(false, default!, error);
    }

}