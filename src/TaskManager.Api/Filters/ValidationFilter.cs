﻿using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TaskManager.Api.Filters
{
    public class ValidationFilter<T> : ActionFilterAttribute where T : class
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var request = context.ActionArguments.Values.FirstOrDefault() as T;
            if (request is null)
            {
                context.Result = new BadRequestObjectResult("Request is null");
                return;
            }

            var validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();
            if (validator is null) throw new Exception($"Class {typeof(T)} has no validator");

            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                context.Result = new BadRequestObjectResult(validationResult.Errors);
                return;
            }
        }
    }
}