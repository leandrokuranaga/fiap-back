using Fiap.Api;
using Fiap.Application.Common;
using Fiap.Domain.SeedWork;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Tests._1._Api_Layer_Tests.MockClasses
{
    public class MockBaseController : BaseController
    {
        public MockBaseController(INotification notification) : base(notification)
        {
        }

        public new IActionResult Response(int? id, object response)
        {
            return base.Response(id, response);
        }

        public new IActionResult Response(object response)
        {
            return base.Response(response);
        }

        public new IActionResult Response(BaseResponse response)
        {
            return base.Response(response);
        }
    }
}
