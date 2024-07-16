namespace RouteSummitTask.BLL.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(AppUser appUser, bool isAdmin);
    }
}
