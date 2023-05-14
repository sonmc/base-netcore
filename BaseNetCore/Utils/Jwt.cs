﻿using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web;
 

namespace BaseNetCore.Utils
{
    public static class Jwt
    {
        public static string ACCESS_TOKEN = "access_token";
        public static string REFRESH_TOKEN = "refresh_token";

        public static CookieOptions GetConfigOption()
        {
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.UtcNow.AddHours(1),
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None
            };
            return cookieOptions;
        }

        public static string GenerateAccessToken(int userId, string secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", userId.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public static string MD5Hash(string input)
        {
            StringBuilder hash = new();
            MD5CryptoServiceProvider md5provider = new();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }


        public static int GetRefreshTokenExpiryTime()
        {
            var refreshTokenExpiryDate = DateTime.Now.AddDays(7);
            return DateTimeToUnixTimeStamp(refreshTokenExpiryDate);
        }

        public static Int32 DateTimeToUnixTimeStamp(DateTime dateTime)
        {
            DateTime localDateTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dateTime, Time.TIMEZONE_ID.GMT0);
            Int32 unixTimestamp = (int)(localDateTime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
            return unixTimestamp;
        }

        public static string Encode(string url)
        {
            return HttpUtility.UrlEncode(url);
        }

        public static string Decode(string url)
        {
            return HttpUtility.UrlDecode(url);
        }

    }
}
