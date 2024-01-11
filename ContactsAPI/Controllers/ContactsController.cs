using System;
using System.Threading.Tasks;
using ContactsAPI.Data;
using ContactsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace ContactsAPI.Controllers
 {
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : Controller
    {
        private readonly ContactsAPIDbContext dbContext;

        public ContactsController(ContactsAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return Ok(await dbContext.Contacts.ToListAsync());
            //return View();
        }

        // to get all the data
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetContacts([FromRoute] Guid id)
        {

            var contact = await dbContext.Contacts.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);

        }

        [HttpPost]

        public async Task<IActionResult> AddContact(AddContactRequest addContactRequest) //create Add contact method
        {
            var contact = new Contact//contact object
            {
                Id = Guid.NewGuid(),
                Address = addContactRequest.Address,
                Email = addContactRequest.Email,
                FullName = addContactRequest.FullName,
                Phone = addContactRequest.Phone,
                // here we have done mapping between the app contact request and contact domain models
            };

            await dbContext.Contacts.AddAsync(contact);
            await dbContext.SaveChangesAsync();

            return Ok(contact);
        }


        [HttpPut]
        [Route("{id:Guid}")]

        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, UpdateContactRequest updateContactRequest)
        {


            var contact = await dbContext.Contacts.FindAsync(id);

            if (contact != null)
            {
                contact.FullName = updateContactRequest.FullName;
                contact.Address = updateContactRequest.Address;
                contact.Phone = updateContactRequest.Phone;
                contact.Email = updateContactRequest.Email;

                //await dbContext.Contacts.AddAsync(contact);
                await dbContext.SaveChangesAsync();

                return Ok(contact);

            };

            return NotFound();

        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id);
            if (contact != null)
            {
                dbContext.Remove(contact);
                dbContext.SaveChangesAsync();
                return Ok(contact);


            }

            return NotFound();
        }
    }
  };
