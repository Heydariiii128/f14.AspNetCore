using f14.AspNetCore.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

namespace f14.AspNetCore.Tests
{
    public class HashHelperTest
    {
        private ITestOutputHelper Log { get; }

        public HashHelperTest(ITestOutputHelper log)
        {
            Log = log;
        }

        [Theory]
        [InlineData("Sample data for hash.", "Sample hash salt.")]
        [InlineData("Тестовые данные для хэша.", "Тестовая соль для хэша.")]
        public void ComputeAndValidate(string data, string salt)
        {
            var hash = HashHelper.Compute(data, Encoding.Unicode.GetBytes(salt));

            Log.WriteLine($"Text: {data}\nHash: {hash}");

            Assert.NotEqual(data, hash);

            Assert.True(HashHelper.Validate(hash, data, Encoding.Unicode.GetBytes(salt)));
        }
    }
}
