﻿using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Eduversity.com.Server.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(DataContext context,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public long GetUserId() => long.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        public string GetUserName() => _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Name)!;

        public async Task<ServiceResponse<string>> SignInAsync(UserLoginRequest request)
        {
            var response = new ServiceResponse<string>();
            var user = await _context.Users
                    .Include(u => u.UserRole)
                    .ThenInclude(ur => ur!.Role)
                    .FirstOrDefaultAsync(u => 
                        u.UserName.ToLower().Equals(request.UserName.ToLower()) && 
                        u.UserRole!.Role!.Name.ToLower().Equals(request.Role.ToLower())
                     );

            // Customize the messages against cyber attack
            if (user == null)
            {
                response.Success = false;
                response.Message = "Invalid credentials."; //User with this role not found.
            }
            else if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Invalid credentials."; //Wrong password.
            }
            else if (user.VerifiedAt == null)
            {
                response.Success = false;
                response.Message = "Invalid credentials."; //Account not verified.
            }
            else
            {
                response.Data = CreateJwtToken(user);
            }

            return response;
        }

        public async Task<ServiceResponse<long>> SignUpAsync(UserRegisterRequest request)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == request.Role);
            if (role == null)
            {
                return new ServiceResponse<long>
                {
                    Success = false,
                    Message = "Invalid role."
                };
            }
            if (await UserExists(request.UserName))
            {
                return new ServiceResponse<long>
                {
                    Success = false,
                    Message = "User already exists."
                };
            }

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                UserName = request.UserName,
                Email = request.UserName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                VerificationToken = CreateRandomToken()
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var userRole = new UserRole
            {
                RoleId = role.Id,
                UserId = user.Id
            };
            _context.UserRoles.Add(userRole);
            await _context.SaveChangesAsync();

            return new ServiceResponse<long> { Data = user.Id, Message = "Registration successful!" };
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(u => u.UserName.ToLower()
                 .Equals(username.ToLower())))
            {
                return true;
            }
            return false;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash =
                    hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(8));
        }

        private string CreateJwtToken(User user)
        {
            /*
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
               // new Claim(ClaimTypes.Role, "Admin")   /////////////////\\\\\\\\\\\\\\
               //new Claim(ClaimTypes.Role, "Lecturer") //////////////////\\\\\\\\\\\\\\\
            };
            */

            if (user == null)
            {
                throw new Exception("Invalid User");
            }

            var claimsIdentity = new ClaimsIdentity(
                new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName)
                }, "AppCookie");

            if (user.UserRole != null && user.UserRole.Role != null)
            {
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, user.UserRole.Role.Name));
            }

            var appToken = _configuration.GetSection("AppSettings:Token").Value;
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(appToken!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claimsIdentity.Claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public async Task<ServiceResponse<bool>> ChangePasswordAsync(long userId, UserChangePasswordRequest request)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "User not found."
                };
            }
            if (!VerifyPasswordHash(request.OldPassword, user.PasswordHash, user.PasswordSalt))
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "Password not changed. Old password is wrong."
                };
            }

            CreatePasswordHash(request.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true, Message = "Password has been changed." };
        }

        public async Task<User> GetUserByUserName(string username)
        {
            var response = await _context.Users.FirstOrDefaultAsync(u => u.UserName.Equals(username));
            return response!;
        }

        public async Task<ServiceResponse<bool>> VerifyEmailAsync(VerifyEmailRequest request)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => 
                    u.VerificationToken! == request.Token &&
                    u.Email == request.Email);

            if (user == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "Invalid token."
                };
            }

            user.VerifiedAt = DateTime.Now;
            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true, Message = "User Verified." };
        }

        public async Task<ServiceResponse<bool>> ForgotPasswordAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "User not found."
                };
            }

            user.PasswordResetToken = CreateRandomToken();
            user.ResetTokenExpires = DateTime.Now.AddDays(1);
            await _context.SaveChangesAsync();

            return new ServiceResponse<bool> { Data = true, Message = "Please, check your email for the password reset token." };
        }

        public async Task<ServiceResponse<bool>> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => 
                    u.PasswordResetToken == request.Token && 
                    u.Email == request.Email);

            if (user == null || user.ResetTokenExpires < DateTime.Now)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "Invalid Token."
                };
            }

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.PasswordResetToken = null;
            user.ResetTokenExpires = null;

            await _context.SaveChangesAsync();
            return new ServiceResponse<bool> { Data = true, Message = "Password reset is successful." };
        }
    }
}
