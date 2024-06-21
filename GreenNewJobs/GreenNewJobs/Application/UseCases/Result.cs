using FluentValidation.Results;

namespace GreenNewJobs.Application.UseCases
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T Value { get; }
        public string[] Errors { get; }

        private Result(bool isSuccess, T value, string[] errors)
        {
            IsSuccess = isSuccess;
            Value = value;
            Errors = errors;
        }

        public static Result<T> SuccessResponse(T value) => new Result<T>(true, value, new string[0]);

        public static Result<T> Failure(IEnumerable<ValidationFailure> failures)
        {
            var errors = failures.Select(f => f.ErrorMessage).ToArray();
            return new Result<T>(false, default, errors);
        }
    }
}
