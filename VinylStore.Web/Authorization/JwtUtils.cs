using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using VinylStore.DataObjects.AuthenticationModels;

namespace VinylStore.Web.Authorization;

public class JwtUtils
    {
        private readonly JwtSecret _secret;

        public JwtUtils(IOptions<JwtSecret> secret)
        {
            _secret = secret.Value;
        }

        public string GenerateToken(AuthenticatedUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            
            var privateKey = Encoding.ASCII.GetBytes(_secret.Value);

            var tokenDescriptor =
                new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(
                        new[]
                        {
                            new Claim("Id", user.Id.ToString())
                        }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(privateKey),
                            SecurityAlgorithms.HmacSha256
                        )
                };
            
            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            return tokenHandler.WriteToken(token);
        }

        public Guid? ValidateJwtToken(string? token)
        {
            if (token is null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            
            var privateKey = Encoding.ASCII.GetBytes(_secret.Value);

            var validationParams = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(privateKey),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };
            
            try
            {
                tokenHandler.ValidateToken(token, validationParams, out var validatedToken);
                
                return Guid.Parse(
                        (validatedToken as JwtSecurityToken)!
                            .Claims
                            .FirstOrDefault(claim => claim.Type == "Id")!
                            .Value
                        );
            }
            catch (Exception)
            {
                return null;
            }
        }
    }