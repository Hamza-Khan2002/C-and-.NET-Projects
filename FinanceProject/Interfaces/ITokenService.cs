using FinanceProject.Models;

namespace FinanceProject.Interfaces{     
    public interface ITokenService
    {
        string CreateToken(AppUser appUser);
    }
}
