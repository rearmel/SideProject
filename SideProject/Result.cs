namespace SideProject;

public class Result
{
    public Result(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }

    public bool IsSuccess { get; set; }

    public string Message { get; set; }

    public static Result Success(string message = "Operação realizada com sucesso!")
    {
        return new Result(true, message);
    }

    public static Result Failure(string message = "Ocorreu um erro durante a operação.")
    {
        return new Result(false, message);
    }
}
