using ChatApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Data
{
    public class Context: IdentityDbContext<User>
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

    }

    
}
