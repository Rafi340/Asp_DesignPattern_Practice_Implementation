﻿using Demo.Infrastructure.Identity;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.API.Controllers
{

    [ApiController]
    [Route("/api/v1/[controller]")]

    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;

        public TokenController(IConfiguration config,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ITokenService tokenService)
        {
            _configuration = config;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string email, string password)
        {
            if (email != null && password != null)
            {
                var user = await _userManager.FindByEmailAsync(email);
                var result = await _signInManager.CheckPasswordSignInAsync(user, password, true);

                if (result != null && result.Succeeded)
                {
                    var claims = (await _userManager.GetClaimsAsync(user)).ToArray();
                    var token = await _tokenService.GetJwtToken(claims,
                            _configuration["Jwt:Key"],
                            _configuration["Jwt:Issuer"],
                            _configuration["Jwt:Audience"]
                        );

                    return Ok(token);
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
}

