using Microsoft.AspNetCore.Identity;

namespace RouteSummitTask.BLL.DataSeed
{
    public class AppContextSeed
    {
        public static async Task SeedAsync(OrderManagementDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            try
            {
                if (!await roleManager.RoleExistsAsync("Admin"))
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                    await roleManager.CreateAsync(new IdentityRole("Customer"));
                }

                if (!userManager.Users.Any())
                {
                    await userManager.CreateAsync(new AppUser
                    {
                        UserName = "FirstAdmin",
                        Email = "admin@gmail.com",
                        PhoneNumber = "123456789"
                    }, "Pass!123");

                    var user = await userManager.FindByEmailAsync("admin@gmail.com");

                    var result = await userManager.AddToRoleAsync(user, "Admin");

                    await userManager.CreateAsync(new AppUser
                    {
                        Email = "Customer@gmail.com",
                        UserName = "Customer1",
                    }, "Pass!123");

                    var customer = await userManager.FindByEmailAsync("Customer@gmail.com");
                    await userManager.AddToRoleAsync(customer, "Customer");

                    await context.Customers.AddAsync(new Customer
                    {
                        Id = 1,
                        AppUserId = customer.Id,
                        Email = customer.Email,
                        Name = customer.UserName
                    });
                    await context.SaveChangesAsync();
                }

                if (context.Products != null && !context.Products.Any())
                {
                    var products = new List<Product>() {
                        new(){
                        Id = 1,
                        Name = "Product 1",
                        Price = 15.99m,
                        Stock = 10
                    }, new(){
                        Id = 2,
                        Name = "Product 2",
                        Price = 89.99m,
                        Stock=100
                    } };

                    await context.Products.AddRangeAsync(products);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
