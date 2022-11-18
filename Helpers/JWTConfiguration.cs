using FileManagerAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FileManagerAPI.Helpers
{
    public class JWTConfiguration
    {
        UserLogin user = new();
        private string _secretKey;
        //private string tokenCreado;       

        public string token(int id)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json").Build();

            _secretKey = builder.GetSection("settings").GetSection("secretkey").ToString();

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            var header = new JwtHeader(credentials);

            var payload = new JwtPayload(id.ToString(), null, null, null, DateTime.Today.AddDays(1));
            var securityToken = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        public JwtSecurityToken Verify(string jwt)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json").Build();

            _secretKey = builder.GetSection("settings").GetSection("secretkey").ToString();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            tokenHandler.ValidateToken(jwt, new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false
            },
            out SecurityToken validatedToken);

            return (JwtSecurityToken)validatedToken; //HAY QUE USAR CASTING YA QUE validatedToken ES UN string Y NECESITAMOS UN TIPO "JwtSecurityToken"
        }




        //public JWTConfiguration()
        //{
        //    var builder = new ConfigurationBuilder()
        //       .SetBasePath(Directory.GetCurrentDirectory())
        //       .AddJsonFile("appsettings.json").Build();

        //    _secretKey = builder.GetSection("settings").GetSection("secretkey").ToString();

        //    var keyBytes = Encoding.ASCII.GetBytes(_secretKey);

        //    var claims = new ClaimsIdentity();

        //    claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Email));

        //    //---------------------------------CREACION DEL TOKEN START----------------------------------//
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = claims,
        //        Expires = DateTime.UtcNow.AddMinutes(2),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
        //    };
        //    //------------------------------------CREACION DEL TOKEN END--------------------------------//

        //    //--------------------------------LECTURA DEL TOKEN START-----------------------------------//
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var tokenCOnfig = tokenHandler.CreateToken(tokenDescriptor);

        //    tokenCreado = tokenHandler.WriteToken(tokenCOnfig);
        //    //--------------------------------LECTURA DEL TOKEN END-----------------------------------//
        //}
        //public string token(UserLogin user)
        //{
        //    return tokenCreado;
        //}
    }
}




