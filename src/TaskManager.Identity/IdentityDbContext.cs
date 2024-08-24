using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManager.Identity.Models;

namespace TaskManager.Identity
{
    public class IdentityDbContext : IdentityDbContext<ApplicationUser, IdentityRole<long>, long>
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options) { }
    }
}