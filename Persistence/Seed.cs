using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context, UserManager<UserNxenesi> userManager){
            if(!userManager.Users.Any()){
                var users = new List<UserNxenesi>{
                    new UserNxenesi{DisplayName = "Bob", UserName = "bob", Email = "bob@test.com", Roli = 0},
                    new UserNxenesi{DisplayName = "Charlie", UserName = "charlie", Email = "charlie@test.com", Roli = 0},
                    new UserNxenesi{DisplayName = "Mike", UserName = "mike", Email = "mike@test.com", Roli = 0},
                    new UserNxenesi{DisplayName = "Blendi", UserName = "blendi", Email = "blendi@test.com", Roli = 1}
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }
            }

            await context.Nxenesi.AddRangeAsync();
            await context.SaveChangesAsync();
        }
    }
}