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
            var response = await contactService.CreateAsync(request);

            if (response == null)
                return BadRequest("Erro ao criar o contato");

            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }

        [HttpGet]

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await contactService.GetById(id);
            if (response == null)
                return NotFound();

            return Ok(response);
        }


        [HttpDelete]


        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateContactRequest request)
        {
            var updated = await contactService.UpdateAsync(request);
            if (!updated)
                return NotFound("Contato não encontrado");

            return Ok("Contato atualizado com sucesso");
        }
    }
}
