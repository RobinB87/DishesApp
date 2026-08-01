namespace Api.Contracts;

public class ApiResult<T> {
  public bool Success { get; set; }
  public T? Data { get; set; }
  public string? Error { get; set; }

  public static ApiResult<T> Ok(T data) => new() { Success = true, Data = data };

  public static ApiResult<T> Fail(string error) => new() { Success = false, Error = error };
}
