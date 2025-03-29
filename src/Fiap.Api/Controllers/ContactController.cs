using Fiap.Application.Contact.Models.Request;
using Fiap.Application.Contact.Services;
using Fiap.Domain.SeedWork;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController(IContactService contactService, INotification notification) : BaseController(notification)
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateContactRequest request)
        {
            await contactService.Create(request);
            return Created();
        }


        [HttpGet]


        [HttpDelete]


        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateContactRequest request)
        {
            await contactService.Update(request);
            return Ok();
        }
    }
}
