using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;

namespace SampleLibrary.Core.Commands
{
    public abstract class CommandHandlerBase
    {
        protected IEnumerable<string> Notifications;

        protected ValidationResult Validate<T, TValidator>(
            T command,
            TValidator validator)
            where T : CommandBase
            where TValidator : IValidator<T>
        {
            ValidationResult validationResult = validator.Validate(command);
            Notifications = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

            return validationResult;
        }

        public Result Return() => new Result {Errors = Notifications, Success = !Notifications.Any()};
    }
}