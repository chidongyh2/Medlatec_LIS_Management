using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace LIS.Infrastructure.Helpers
{
    public static class GenerateHelper
    {
        // Use for generate userId
        private static char[] _base62chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();
        private static Random _random = new Random();

        /// <summary>
        /// Các hằng số mặc định
        /// </summary>
        public const int PasswordDerivationIteration = 1000,
                         PasswordBytesLength = 64,
                         MinPasswordLength = 6,
                         PasswordSaltLength = 16;

        /// <summary>
        /// Tạo ra một chuỗi ngẫu nhiên
        /// </summary>
        /// <param name="length">Độ dài của chuỗi</param>
        /// <returns>Chuỗi ngẫu nhiên</returns>
        public static string GenerateRandomString(int length)
        {
            var result = new StringBuilder();
            const string src = @"abcdefghiklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            var seed = GetRandomSeed();
            var rnd = new Random(seed);
            length.Times(() => result.Append(src[rnd.Next(src.Length - 1)]));
            return result.ToString();
        }

        private static void Times(this int n, Action action)
        {
            if (action == null || n <= 0)
            {
                return;
            }

            for (var i = 0; i < n; i++)
            {
                action();
            }
        }

        /// <summary>
        /// Tạo ra 1 seed ngẫu nhiên
        /// </summary>
        /// <returns>Seed ngâu nhiên</returns>
        public static int GetRandomSeed()
        {
            var rng = new RNGCryptoServiceProvider();
            var randomBytes = new byte[4];
            rng.GetBytes(randomBytes);
            return (randomBytes[0] & 0x7f) << 24 |
                   randomBytes[1] << 16 |
                   randomBytes[2] << 8 |
                   randomBytes[3];
        }

        /// <summary>
        /// Tạo ra 1 mảng byte ngẫu nhiên
        /// </summary>
        /// <param name="length">Độ dài mảng byte</param>
        /// <returns>Mảng byte ngẫu nhiên</returns>
        public static byte[] GenerateRandomBytes(int length)
        {
            var result = new byte[length];
            RandomNumberGenerator.Create().GetBytes(result);
            return result;
        }

        /// <summary>
        /// Băm mật khẩu
        /// </summary>
        /// <param name="pwd">Mật khẩu.</param>
        /// <param name="salt">Muối.</param>
        /// <returns>Mật khẩu đã được băm</returns>
        public static byte[] GetInputPasswordHash(string pwd, byte[] salt)
        {
            var inputPwdBytes = Encoding.UTF8.GetBytes(pwd);
            var inputPwdHasher = new Rfc2898DeriveBytes(inputPwdBytes, salt, PasswordDerivationIteration);
            return inputPwdHasher.GetBytes(PasswordBytesLength);
        }

        /// <summary>
        /// Tạo mã số ngẫu nhiên
        /// </summary>
        /// <param name="length">Độ dài mã số</param>
        /// <returns>Mã số ngẫu nhiên</returns>
        public static string GenerateRandomDigitCode(int length)
        {
            var random = new Random();
            var str = string.Empty;
            for (var i = 0; i < length; i++)
            {
                str = string.Concat(str, random.Next(10).ToString(CultureInfo.InvariantCulture));
            }
            return str;
        }

        /// <summary>
        /// Tạo ra 1 số ngẫu nhiên trong 1 khoảng quy định
        /// </summary>
        /// <param name="min">Số nhỏ nhất</param>
        /// <param name="max">Số lớn nhất</param>
        /// <returns>1 số ngẫu nhiên</returns>
        public static int GenerateRandomInteger(int min = 0, int max = 2147483647)
        {
            var randomNumberBuffer = new byte[10];
            new RNGCryptoServiceProvider().GetBytes(randomNumberBuffer);
            return new Random(BitConverter.ToInt32(randomNumberBuffer, 0)).Next(min, max);
        }

        /// <summary>
        /// Tạo ra mã theo định dạng (ddmmyyhhmm)
        /// </summary>
        /// <returns></returns>
        public static string GenerateId()
        {
            var now = DateTime.Now;

            var year = now.Year;
            var month = now.Month;
            var day = now.Day;
            var hour = now.Hour;
            var minute = now.Minute;
            var second = now.Second;

            return year.ToString() + month.ToString() + day.ToString() + hour.ToString() + minute.ToString() + second.ToString();
        }

        public static string GetBase62(int length)
        {
            var sb = new StringBuilder(length);

            for (int i = 0; i < length; i++)
                sb.Append(_base62chars[_random.Next(62)]);

            return sb.ToString();
        }

        public static string GetBase36(int length)
        {
            var sb = new StringBuilder(length);

            for (int i = 0; i < length; i++)
                sb.Append(_base62chars[_random.Next(36)]);

            return sb.ToString();
        }
    }
}
