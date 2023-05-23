using ApplicationService.DTOs;
using ApplicationService.Implementations;
using DataHR.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.Content.Models;

namespace WebApplication1.Controllers
{
	[Route("api/[controller]")]

	public class TokenController : ControllerBase
	{
		private UserManagementService service = new UserManagementService();

		[HttpPost]
		public IActionResult GetToken([FromBody] LoginModel model)
		{
			UserDTO loggedUser = service.Find(u => u.Username == model.Username &&
																u.Password == model.Password);
			if (loggedUser.ID == 0)
				return Unauthorized();

			var claims = new[]
			{
			new Claim("LoggedUserId", loggedUser.ID.ToString())
			};

			//същият като в configuration
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("!Password123!Password123"));
			//подписваме се с даден алготиръм
			//първите две секции първо се криптират, после се хешират
			var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				"stefka.bg",
				"project.app",
				claims,
				expires: DateTime.UtcNow.AddMinutes(20),
				signingCredentials: signingCredentials
			);

			JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
			string jwt = tokenHandler.WriteToken(token);

			return Ok(jwt);
		}
		
	}
}
