using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using suaBaladaAqui2.ViewsModels;
using suaBaladaAqui2.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace suaBaladaAqui2.Controllers;

public class LoginController : Controller
{
    private readonly suaBaladaAqui2Context _context;

    public LoginController(suaBaladaAqui2Context context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    //Post: Usuario Login
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login([Bind("login,senha")] LoginViewModel loginViewModel)
    {
        if (ModelState.IsValid)
        {
            var usuariosModel = await _context.usuarios
                .FirstOrDefaultAsync(m => m.Login == loginViewModel.login && m.Senha == loginViewModel.senha);
            if (usuariosModel == null)
            {
                return View("Views/Login/Index.cshtml");
            }
            
            await CriaCookie(HttpContext, loginViewModel.login);
            return RedirectToAction(nameof(Index), "Eventos");
        }
        return RedirectToAction(nameof(Index));
    }

    public async Task CriaCookie(HttpContext ctx, string usuario)
    {
        var claims = new List<Claim>(); 
        claims.Add(new Claim(ClaimTypes.Name, usuario));

        var claimIdentity = new ClaimsPrincipal(
            new ClaimsIdentity(
                claims,
               CookieAuthenticationDefaults.AuthenticationScheme
            )
        );

        var Autenticacao = new AuthenticationProperties
        {
            ExpiresUtc = DateTime.Now.AddHours(2),
            IssuedUtc = DateTime.Now
        };

        await ctx.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimIdentity, Autenticacao);
    }

    public async Task DestroiCookie(HttpContext ctx)
    {
        await ctx.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

}
