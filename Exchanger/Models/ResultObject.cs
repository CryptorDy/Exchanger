namespace Exchanger.Models;

public class ResultObject
{
    public bool Success { get; set; }
    public object Data { get; set; }
    public string Message { get; set; }
    public List<string> Errors { get; set; }

    public ResultObject()
    {
        Errors = new List<string>();
    }

    public static ResultObject SuccessResult(string message = "Operation successful")
    {
        return new ResultObject { Success = true, Message = message };
    }

    public static ResultObject SuccessResult(object data, string message = "Operation successful")
    {
        return new ResultObject { Success = true, Data = data, Message = message };
    }

    public static ResultObject ErrorResult(string error, string message = "Operation failed")
    {
        var response = new ResultObject { Success = false, Message = message };
        response.Errors.Add(error);
        return response;
    }

    public static ResultObject ErrorResult(List<string> errors, string message = "Operation failed")
    {
        return new ResultObject { Success = false, Message = message, Errors = errors };
    }

    public static ResultObject ErrorResult(string message = "Operation failed")
    {
        return new ResultObject { Success = false, Message = message };
    }
}