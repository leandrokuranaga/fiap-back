//using Fiap.Api;
//using Fiap.Application.Common;
//using Fiap.Domain.SeedWork;
//using Microsoft.AspNetCore.Mvc;

//namespace Fiap.Tests._1._Api_Layer_Tests.MockClasses
//{
//    public class MockBaseController(INotification notification) : BaseController(notification)
//    {
//        public new IActionResult Response<T>(int? id, object response)
//        {
//            return base.Response<T>(id, response);
//        }

//        public new IActionResult Response<T>(object response)
//        {
//            if (response is BaseResponse<T> typedResponse)
//                return base.Response<T>(typedResponse);

//            throw new ArgumentException("Invalid response type, expected BaseResponse<T>", nameof(response));
//        }


//        public new IActionResult Response<T>(BaseResponse<T> response)
//        {
//            return base.Response<T>(response);
//        }
//    }
//}
