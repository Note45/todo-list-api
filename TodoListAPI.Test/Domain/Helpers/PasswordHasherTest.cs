using System;
using TodoListAPI.Domain.Helpers;

namespace TodoListAPI.Test.Domain.Helpers
{
    public class PasswordHasherTest
	{
        [Fact(DisplayName = "Should be able to hash the password")]
        public void ShouldReturnTheHashedPasswordWhenCallHasher()
        {
            PasswordHasher passwordHasher = new();

            string passwordHashed = passwordHasher.Hash("Password");

            Assert.IsType<string>(passwordHashed);
        }

        [Fact(DisplayName = "Should be able to test the hashed password")]
        public void ShouldTestHashedPasswordWhenCheckHasher()
        {
            PasswordHasher passwordHasher = new();

            string password = "password-to-test";
            string passwordHashed = passwordHasher.Hash(password);
            var (Verified, NeedsUpgrade) = passwordHasher.Check(passwordHashed, password);

            Assert.True(Verified);
            Assert.False(NeedsUpgrade);
        }
    }
}

