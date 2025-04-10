using Fiap.Domain.SeedWork;

namespace Fiap.Application.Common
{
    public class BaseResponse<T>
    {
        public bool Success { get; set; } = true;
        public T? Data { get; set; }
        public NotificationModel? Error { get; set; }

        public static BaseResponse<T> Ok(T data) =>
            new BaseResponse<T> { Success = true, Data = data };

        public static BaseResponse<T> Fail(NotificationModel error) =>
            new BaseResponse<T> { Success = false, Error = error };
    }
}

