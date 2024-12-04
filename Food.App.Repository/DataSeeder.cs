using Food.App.Core.Entities;
using Food.App.Core.Enums;

namespace Food.App.Repository;
public static class DataSeeder
{
    public static async Task SeedUsers(AppDbContext appDbContext)
    {
        if (!appDbContext.Users.Any())
        {
            var users = new List<User> {
                new User{Username="ahmed",Email="ahmed@yahoo.com",Password="ahmed!@#111",Role=Role.User,Country="Cairo",Phone="01527032578",CreatedAt=DateTime.Now,CreatedBy=0},
                new User{Username="alaa",Email="alaa@gmail.com",Password="alaa!@#111",Role=Role.User,Country="Alex",Phone="01113072179",CreatedAt=DateTime.Now,CreatedBy=0},
                new User{Username="abdalla",Email="abdalla@hotmail.com",Password="abdalla!@#111",Role=Role.User,Country="Giza",Phone="01021832132",CreatedAt=DateTime.Now,CreatedBy=0}
            };

            await appDbContext.Users.AddRangeAsync(users);
            await appDbContext.SaveChangesAsync();
        }
    }

    public static async Task SeedAdmins(AppDbContext appDbContext)
    {
        if (!appDbContext.Admins.Any())
        {
            var admin = new Admin { Username = "aliahmed", Email = "aliahmed@gmail.com", Password = "aliahmed!@#111", Role = Role.Admin, CreatedAt = DateTime.Now, CreatedBy = 0 };

            await appDbContext.Admins.AddAsync(admin);
            await appDbContext.SaveChangesAsync();
        }
    }
}
