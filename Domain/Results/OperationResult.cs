namespace Domain.Results;

public class OperationResult
{
    public bool IsSuccess { get; }
    public string Message { get; }

    public OperationResult(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }

    public static OperationResult Success() => new(true, "Operation complete successfully.");
    public static OperationResult Failure(string errorMessage) => new(false, errorMessage);
}