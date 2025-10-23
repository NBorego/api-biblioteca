using APIBiblioteca.Errors;
using OneOf;

namespace APIBiblioteca.Extensions
{
    public static class OneOfExtensions
    {
        public static bool IsSuccess<TResult>(this OneOf<TResult, AppError> obj) => obj.IsT0;
        public static TResult GetSuccessResult<TResult>(this OneOf<TResult, AppError> obj) => obj.AsT0;

        public static bool IsError<TResult>(this OneOf<TResult, AppError> obj) => obj.IsT1;
        public static AppError GetErrorResult<TResult>(this OneOf<TResult, AppError> obj) => obj.AsT1;
    }
}
}
