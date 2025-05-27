using Demo.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Infrastructure.Seeds
{
    public static class ClaimSeed
    {
        public static ApplicationUserClaim[] GetClaims()
        {
            return [
            
                new ApplicationUserClaim
                {
                    Id = -1,
                    UserId = new Guid("F9019C06-A30A-4937-2A90-08DD9AC7EE99"),
                    ClaimType = "create_user",
                    ClaimValue = "allowed"
                },
                
            ];
        }
    }
}
