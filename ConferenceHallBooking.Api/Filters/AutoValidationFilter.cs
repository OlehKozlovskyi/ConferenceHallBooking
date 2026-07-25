using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ConferenceHallBooking.Api.Filters
{
    public class AutoValidationFilter : IAsyncActionFilter
    {
        private readonly IServiceProvider _serviceProvider;

        public AutoValidationFilter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            foreach (var argument in context.ActionArguments.Values)
            {
                if (argument is null || IsValueType(argument))
                    continue;

                var validatorType = typeof(IValidator<>).MakeGenericType(argument.GetType());
                var validator = _serviceProvider.GetService(validatorType) as IValidator;

                if (validator != null)
                {
                    var result = await validator.ValidateAsync(new ValidationContext<object>(argument));

                    if (!result.IsValid)
                        throw new ValidationException(result.Errors);
                }
            }

            await next();
        }

        private static bool IsValueType(object argument)
        {
            var type = argument.GetType();

            if (type.IsValueType || type == typeof(string))
                return true;

            return false;
        }
    }
}
