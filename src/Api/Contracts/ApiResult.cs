using Microsoft.AspNetCore.Mvc;

namespace Api.Contracts;

public class ApiResult<T> {
  public bool Success { get; set; }
  public T? Data { get; set; }
  public ProblemDetails Error { get; set; } = new ProblemDetails();

  public static ApiResult<T> Ok(T data) => new() { Success = true, Data = data };

  public static ApiResult<T> Fail(ProblemDetails error) => new() { Success = false, Error = error };
}
