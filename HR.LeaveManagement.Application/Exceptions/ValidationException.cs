using FluentValidation.Results;

namespace HR.LeaveManagement.Application.Exceptions;

public class ValidationException : ApplicationException
{
    public List<string> Errors { get; set; } = new();

    public ValidationException(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            Errors.Add(error.ErrorMessage);
        }
    }
}