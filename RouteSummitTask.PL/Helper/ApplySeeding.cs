using RouteSummitTask.BLL.DataSeed;
using RouteSummitTask.DAL.Context;

namespace RouteSummitTask.PL.Helper
{
    public class ApplySeeding
    {
        public static async Task ApplySeedingAsync(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                try
                {
                    var context = scope.ServiceProvider.GetRequiredService<OrderManagementDbContext>();
                    var userManger = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                    var roleManger = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                    await AppContextSeed.SeedAsync(context, userManger, roleManger);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
