using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
namespace TestCase.Application.Common
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public string? ErrorMessage { get; set; }
        public int StatusCode { get; set; }

        public static Result<T> Failure(string errorMessage, int statusCode)
        {
            return new Result<T>
            {
                IsSuccess = false,
                ErrorMessage = errorMessage,
                StatusCode = statusCode
            };
        }
    }

    public static class ResultExtension 
    {
        public static Result<T> ToResult<T>(this T data)
        {
            return new Result<T>
            {
                IsSuccess = true,
                Data = data,
                StatusCode = (int)HttpStatusCode.OK
            };
        }
    }
}
