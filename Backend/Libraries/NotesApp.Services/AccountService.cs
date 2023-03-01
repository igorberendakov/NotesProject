using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NotesApp.Domain.Abstractions;
using NotesApp.Domain.Entities;
using NotesApp.Infrastructure.Configurations.Authentication.Models;
using NotesApp.Infrastructure.Dtos;
using NotesApp.Services.Abstractions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NotesApp.Services
{
    public class AccountService : IAccountService
    {
        private readonly IDbContext _dbContext;
        private readonly DbSet<User> _users;
        private readonly JwtOptions _jwtOptions;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AccountService(IDbContext dbContext, IOptions<JwtOptions> jwtOptions, IPasswordHasher<User> passwordHasher)
        {
            _dbContext = dbContext;
            _users = _dbContext.Set<User>();
            _jwtOptions = jwtOptions.Value;
            _passwordHasher = passwordHasher;
        }

        public async Task<LoginResult> LoginUserAsync(UserDto userDto, CancellationToken cancellationToken = default)
        {
            var user = await _users.SingleOrDefaultAsync(x => x.Login == userDto.Login, cancellationToken);

            if (user == null)
            {
                return new LoginResult(false, null, "Неверный логин или пароль.");
            }

            var verificationResult = _passwordHasher.VerifyHashedPassword(new User { Login = user.Login }, user.PasswordHash, userDto.Password);

            if (verificationResult == PasswordVerificationResult.Failed)
            {
                return new LoginResult(false, null, "Неверный логин или пароль.");
            }

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var token = GenerateToken(claims);

            return new LoginResult(true, token, "Авторизация выполнена успешно.");
        }

        public async Task<LoginResult> RegisterUserAsync(UserDto userDto, CancellationToken cancellationToken = default)
        {
            var userExist = await _users.AnyAsync(x => x.Login == userDto.Login, cancellationToken);

            if (userExist)
            {
                return new LoginResult(false, null, "Пользователь с данным логином уже зарегистрирован.");
            }

            var user = new User { Login = userDto.Login };
            var passwordHash = _passwordHasher.HashPassword(user, userDto.Password);
            user.PasswordHash = passwordHash;
            await _users.AddAsync(user, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Login),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var token = GenerateToken(claims);

            return new LoginResult(true, token, "Регистрация выполнена успешно.");
        }

        private string GenerateToken(params Claim[] claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _jwtOptions.Issuer,
                _jwtOptions.Audience,
                claims,
                null,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
