namespace TaskManagement.Api.Controllers;

[ApiController]
public class ApiController : ControllerBase
{
    protected readonly ISender _mediator;

    protected ApiController(ISender mediator)
    {
        _mediator = mediator;
    }

    protected async Task<IActionResult> Handle<TCommand, TResult>(TCommand command)
    {
        if (command is null) return Problem(Error.NotFound());

        ErrorOr<TResult> result = (await _mediator.Send((IRequest<ErrorOr<TResult>>)command))!;

        return result.Match(
            result => Ok(result),
            error => Problem(error));
    }

    protected IActionResult Problem(List<Error> errors)
    {
        if (errors.Count is 0)
        {
            return Problem();
        }

        if (errors.All(error => error.Type == ErrorType.Validation))
        {
            return ValidationProblem(errors);
        }

        HttpContext.Items[HttpContextItemKeys.Errors] = errors;

        var firstError = errors[0];

        return Problem(firstError);
    }

    private IActionResult ValidationProblem(List<Error> errors)
    {
        var modelStateDictionary = new ModelStateDictionary();

        foreach (var error in errors)
        {
            modelStateDictionary.AddModelError(
                error.Code,
                error.Description);
        }

        return ValidationProblem(modelStateDictionary);
    }

    private IActionResult Problem(Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Failure => StatusCodes.Status417ExpectationFailed,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            _ => StatusCodes.Status500InternalServerError,
        };

        return Problem(statusCode: statusCode, title: error.Description);
    }
}
