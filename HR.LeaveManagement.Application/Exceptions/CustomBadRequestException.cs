namespace HR.LeaveManagement.Application.Exceptions;

public class CustomBadRequestException : ApplicationException
{
    public CustomBadRequestException(string message) : base(message)
    {
        
    }
}