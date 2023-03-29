using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Context;
using WebAPI.Models;
using WebAPI.Repository;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private IRepository<Contact> Repo;
        public ContactsController(IRepository<Contact> repo)
        {
            Repo = repo;
        }
        [HttpGet(Name = "GetAllContactItems")]
        public IEnumerable<Contact> Get()
        {
            return Repo.Get();
        }
        [HttpGet("{id}", Name = "GetContactItem")]
        public IActionResult Get(int id)
        {
            Contact contact = Repo.Get(id);
            if (contact == null) return NotFound();
            return new ObjectResult(contact);
        }
        [HttpPost]
        public IActionResult Create([FromBody] Contact contact)
        {
            if (contact == null) return BadRequest();
            Repo.Create(contact);
            return CreatedAtRoute("GetContactItem", new { id = contact.Id }, contact);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Contact updatedContact)
        {
            if (updatedContact == null || updatedContact.Id != id)
            {
                return BadRequest();
            }

            if (Repo.Get(id) == null) return NotFound();

            Repo.Update(updatedContact);
            return new ObjectResult(updatedContact);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Contact contact = Repo.Delete(id);
            return contact == null ? NotFound() : new ObjectResult(contact);
        }
    }
}
