using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Login_Sample.Data
{
    /// <summary>
    /// 密码哈希工具类
    /// </summary>
    public static class PasswordHasher
    {
        /// <summary>
        /// 生成密码哈希
        /// </summary>
        /// <param name="password">原始密码</param>
        /// <returns>哈希后的密码（包含盐和哈希值）</returns>
        public static string HashPassword(string password)
        {
            // 生成16字节的盐
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            
            // 使用PBKDF2算法生成哈希
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            
            // 格式：salt:hashed
            return $"{Convert.ToBase64String(salt)}:{hashed}";
        }
        
        /// <summary>
        /// 验证密码
        /// </summary>
        /// <param name="hashedPassword">哈希后的密码（包含盐和哈希值）</param>
        /// <param name="password">原始密码</param>
        /// <returns>密码是否匹配</returns>
        public static bool VerifyPassword(string hashedPassword, string password)
        {
            // 解析盐和哈希值
            string[] parts = hashedPassword.Split(':');
            if (parts.Length != 2)
            {
                return false;
            }
            
            byte[] salt = Convert.FromBase64String(parts[0]);
            string storedHash = parts[1];
            
            // 使用相同的盐和参数生成哈希
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            
            // 比较哈希值
            return hashed == storedHash;
        }
    }
}