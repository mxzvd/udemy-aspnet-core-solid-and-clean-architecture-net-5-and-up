namespace HR.LeaveManagement.MVC.Services.Base;

public class Response<T>
{
    public string Message { get; set; } = null!;
    public string ValidationErrors { get; set; } = null!;
    public bool Success { get; set; }
    public T Data { get; set; }
}
