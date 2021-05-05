using Xunit;
using Password.Services;

namespace Password.Service.Test
{
    public class DefaultPasswordServiceTest
    {
        [Theory]
        [InlineData("")]
        [InlineData("aa")]
        [InlineData("ab")]
        [InlineData("AAAbbbCc")]
        public void IsValid_LessThanNineDigits_ReturnsFalse(string password)
        {
            IPasswordValidator passwordValidator = new DefaultPasswordValidator();
            bool result = passwordValidator.IsValid(password);

            Assert.False(result);
        }

        [Theory]
        [InlineData("")]
        [InlineData("aa")]
        [InlineData("ab")]
        [InlineData("AAAbbbCc")]
        public void IsValid_LessThanOneDigit_ReturnsFalse(string password)
        {
            IPasswordValidator passwordValidator = new DefaultPasswordValidator();
            bool result = passwordValidator.IsValid(password);

            Assert.False(result);
        }

        [Theory]
        [InlineData("AA")]
        [InlineData("AB")]
        public void IsValid_LessThanOneLowerCaseCharacter_ReturnsFalse(string password)
        {
            IPasswordValidator passwordValidator = new DefaultPasswordValidator();
            bool result = passwordValidator.IsValid(password);

            Assert.False(result);
        }

        [Theory]
        [InlineData("aa")]
        [InlineData("ab")]
        public void IsValid_LessThanOneUpperCaseCharacter_ReturnsFalse(string password)
        {
            IPasswordValidator passwordValidator = new DefaultPasswordValidator();
            bool result = passwordValidator.IsValid(password);

            Assert.False(result);
        }

        [Theory]
        [InlineData("")]
        [InlineData("aa")]
        [InlineData("ab")]
        [InlineData("AAAbbbCc")]
        [InlineData("AbTp9 fok")]
        public void IsValid_LessThanOneSpecialCharacter_ReturnsFalse(string password)
        {
            IPasswordValidator passwordValidator = new DefaultPasswordValidator();
            bool result = passwordValidator.IsValid(password);

            Assert.False(result);
        }

        [Theory]
        [InlineData("aa")]
        [InlineData("AAAbbbCc")]
        [InlineData("AbTp9!foo")]
        [InlineData("AbTp9!foA")]
        public void IsValid_RepeatedCharacter_ReturnsFalse(string password)
        {
            IPasswordValidator passwordValidator = new DefaultPasswordValidator();
            bool result = passwordValidator.IsValid(password);

            Assert.False(result);
        }

        [Fact]
        public void IsValid_ValidPassword_ReturnsTrue()
        {
            IPasswordValidator passwordValidator = new DefaultPasswordValidator();
            bool result = passwordValidator.IsValid("AbTp9!fok");

            Assert.True(result);
        }
    }
}
