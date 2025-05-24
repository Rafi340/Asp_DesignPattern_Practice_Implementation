using Demo.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Infrastructure.Seeds
{
    public static class RoleSeed
    {
        public static ApplicationRole[] GetRoles()
        {
            return[
            new ApplicationRole
            {
                Id = new Guid("7870041D-77EF-4788-BAE3-6D092E4E053B"),
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = new DateTime(2025, 4, 19, 1, 2, 1).ToString(),
            },
            new ApplicationRole
            {
                Id = new Guid("66CB7E8A-0D51-42C0-82DD-DCC1A80491D9"),
                Name = "HR",
                NormalizedName = "HR",
                ConcurrencyStamp = new DateTime(2025, 4, 19, 2, 3, 4).ToString(),
            },
            new ApplicationRole
            {
                Id = new Guid("B0CBA364-772B-4C50-AB32-4AF1A9E5DC84"),
                Name = "Author",
                NormalizedName = "AUTHOR",
                ConcurrencyStamp = new DateTime(2025, 4, 19, 2, 3, 4).ToString(),
            }
            ];
        }
    }
}
