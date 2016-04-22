using Authentication.API.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Authentication.API
{
    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext()
            : base("AuthContext")
        {

        }

        public DbSet<AspNetClient> Clients { get; set; }
        public DbSet<AspNetRefreshToken> RefreshTokens { get; set; }
    }
}
