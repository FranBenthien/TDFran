using Microsoft.AspNetCore.Mvc;
using dto;

namespace back.Controllers;

using Services;
using Model;

[ApiController]
[Route("user")]
public class UserController : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody]UsuarioDTO user, 
        [FromServices]TokenService service
    )
    {
        using TDSabadoContext context   //utiliza o using para limpar depois ***
            = new TDSabadoContext();
        
        var possibleUser = context.Usuarios
            .FirstOrDefault(
                u => u.UserId == user.UserId);
        
        if (possibleUser == null)
            return BadRequest("Nome de usuário inválido");  //pode incluir qualquer tipo de erro ***

        if (possibleUser.Userpass != user.Password)
            return BadRequest("Senha inválida!");

        var token = await service.CreateToken(possibleUser);
        
        return Ok(token.Value); // vai mandar para o front end fazer a verificação se o usuário é realmente quem logou e ferou aquele token
    }

    [HttpPost("register")]
    public IActionResult Register(
        [FromBody] UsuarioDTO user
        )
    {
        using TDSabadoContext context 
            = new TDSabadoContext();
        
        List<string> errors = new List<string>();

        if (user.BirthDate == null)
        {
            errors.Add("Data de nascimento não foi informada");
        }

        if (user.Name.Length < 5)
        {
            errors.Add("O nome do usuário precisa conter ao menos 5 letras.");
        }

        if (context.Usuarios
            .Any(u => u.UserId == user.UserId))
        {
            errors.Add("Seu Nome de Usuário já está em uso!");
        }

        if (errors.Count > 0)
        {
            return this.BadRequest(errors);
        }

        Usuario usuario = new Usuario();
        usuario.Name = user.Name;
        usuario.BirthDate = user.BirthDate.Value;
        usuario.UserId = user.UserId;
        usuario.Userpass = user.Password;

        context.Add(usuario);
        context.SaveChanges();

        return Ok();
    }

    [HttpPost("update")]
    public IActionResult UpdateName()
    {
        throw new NotImplementedException();
    }
}
