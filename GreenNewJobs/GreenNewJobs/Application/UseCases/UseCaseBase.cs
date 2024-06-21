using FluentValidation.Results;

namespace GreenNewJobs.Application.UseCases
{
    public abstract class UseCaseBase<TInput, TOutput>
    {
        protected abstract Task<Result<TOutput>> ExecuteCoreAsync(TInput input);

        protected virtual IEnumerable<ValidationFailure> ValidateInput(TInput input)
        {
            return Enumerable.Empty<ValidationFailure>();
        }

        public async Task<Result<TOutput>> ExecuteAsync(TInput input, CancellationToken cancellationToken)
        {
            var validationFailures = ValidateInput(input).ToList();
            if (validationFailures.Any())
            {
                return Result<TOutput>.Failure(validationFailures);
            }

            return await ExecuteCoreAsync(input);
        }
    }
}
