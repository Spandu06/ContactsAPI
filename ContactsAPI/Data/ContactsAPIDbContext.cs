using ContactsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsAPI.Data
{
    public class ContactsAPIDbContext : DbContext
    {
        public ContactsAPIDbContext(DbContextOptions options) : base(options) //constructor
        {
        }

        public DbSet<Contact>? Contacts { get; set; } //curd operation on contact
    }
}
