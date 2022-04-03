using Microsoft.EntityFrameworkCore;
using ProfileService.Models;

namespace ProfileService.DataBase
{
    public class ProfileContext: DbContext
    {
        public DbSet<Profile> Profiles { get; set; }
        public ProfileContext(DbContextOptions<ProfileContext> options):base(options)
        {
        }
    }
}
