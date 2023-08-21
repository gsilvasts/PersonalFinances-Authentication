using FluentValidation;
using PersonalFinances.Authentication.Api.Models;
using System.Diagnostics.CodeAnalysis;

namespace PersonalFinances.Authentication.Api.Filters
{
    [ExcludeFromCodeCoverage]
    public class ValidationFilter<T> : IEndpointFilter where T : class
    {
        private readonly IValidator<T> _validator;

        public ValidationFilter(IValidator<T> validator)
        {
            _validator = validator;
        }

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var obj = context.Arguments.FirstOrDefault(x=>x?.GetType() == typeof(T)) as T;

            if (obj == null)
            {
                return Results.BadRequest();
            }

            var validationResult = await _validator.ValidateAsync(obj);

            if (!validationResult.IsValid)
            {
                var messages = validationResult.Errors.Select(e => new ErrorModel.ErrorDetails { Message = e.ErrorMessage }).ToList();

                return Results.BadRequest(new ErrorModel(messages));
            }

            return await next(context);
        }

        //public void OnActionExecuted(ActionExecutedContext context)
        //{
        //    if (!context.ModelState.IsValid)
        //    {
        //        var messages = context.ModelState
        //            .SelectMany(ms => ms.Value.Errors)
        //            .Select(e => new ErrorModel.ErrorDetails { Message = e.ErrorMessage })
        //            .ToList();

        //        context.Result = new BadRequestObjectResult(new ErrorModel(messages));
        //    }
        //}

        //public void OnActionExecuting(ActionExecutingContext context)
        //{
        //    if (!context.ModelState.IsValid)
        //    {
        //        var messages = context.ModelState
        //            .SelectMany(ms => ms.Value.Errors)
        //            .Select(e => new ErrorModel.ErrorDetails { Message = e.ErrorMessage })
        //            .ToList();

        //        context.Result = new BadRequestObjectResult(new ErrorModel(messages));
        //    }
        //}
    }
}
