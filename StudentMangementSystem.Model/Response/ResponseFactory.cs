using StudentMangementSystem.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentMangementSystem.Model.Response
{
    public static class ResponseFactory
    {
        public static ApiResponse<T> Success<T>(T data, string? message = "Success")
            => new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Result = data
            };

        public static ApiResponse<T> Failure<T>(string message, List<ErrorDetail> errors = null)
            => new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Errors = errors ?? new List<ErrorDetail>()
            };

        public static ApiResponse<T> ModelValidation<T>(T data)
            => new ApiResponse<T>
            {
                Success = false,
                Message = "Model Validaiton Error",
                ValidaitonErrors = data
            };

        public static ApiResponse<T> Error<T>(string message)
            => new ApiResponse<T>
            {
                Success = false,
                Message = message
            };

        public static ApiResponse<T> Error<T>(T data)
           => new ApiResponse<T>
           {
               Success = false,
               Message = "An Unexpected error occurred",
               Exception = data
           };
    }
}
