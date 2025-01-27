using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace MvcWebIdentityA.Services;

public class SeedUsersClaimsInitial : ISeedUserClaimsInitial
{
    private readonly UserManager<IdentityUser> _userManager;

    public SeedUsersClaimsInitial(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task SeedUserClaimsAsync()
    {
        try
        {
            // Criando usuário 1
            IdentityUser user1 = await _userManager.FindByEmailAsync("admin@localhost");
            if (user1 is not null)
            {
                var claims = await _userManager.GetClaimsAsync(user1);

                // Verifica se o claim "CadastradoEm" não existe
                if (!claims.Any(c => c.Type == "CadastradoEm"))
                {
                    // Adiciona o novo claim
                    var claim = new Claim("CadastradoEm", "09/15/2024");
                    var claimResult1 = await _userManager.AddClaimsAsync(user1, [claim]);
                }
                if (!claims.Any(c => c.Type == "IsAdmin"))
                {
                    // Adiciona o novo claim
                    var claim = new Claim("IsAdmin", "true");
                    var claimResult2 = await _userManager.AddClaimsAsync(user1, [claim]);
                }
            }

            // Criando usuário 2
            IdentityUser user2 = await _userManager.FindByEmailAsync("usuario@localhost");
            if (user2 is not null)
            {
                var claims = await _userManager.GetClaimsAsync(user2);

                // Verifica se o claim "IsAdmin" não existe
                if (!claims.Any(c => c.Type == "IsAdmin"))
                {
                    // Adiciona o novo claim
                    var claim = new Claim("IsAdmin", "false");
                    var claimResult1 = await _userManager.AddClaimsAsync(user2, [claim]);
                }
                if (!claims.Any(c => c.Type == "IsFuncionario"))
                {
                    // Adiciona o novo claim
                    var claim = new Claim("IsFuncionario", "true");
                    var claimResult2 = await _userManager.AddClaimsAsync(user2, [claim]);
                }
            }
        }
        catch (Exception ex)
        {
            // Captura a exceção e loga ou trata de forma mais adequada
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}
