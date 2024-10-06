using System;
using SnackTech.Core.Domain.Util;

namespace SnackTech.Core.Tests.Domain.Util
{
    public class CpfValidatorTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void AgainstInvalidCpf_ThrowsArgumentException_WhenCpfIsNullOrWhiteSpace(string cpf)
        {
            Assert.Throws<ArgumentException>(() => CpfValidator.AgainstInvalidCpf(cpf));
        }

        [Theory]
        [InlineData("11111111111")]
        [InlineData("22222222222")]
        [InlineData("33333333333")]
        [InlineData("44444444444")]
        [InlineData("55555555555")]
        [InlineData("66666666666")]
        [InlineData("77777777777")]
        [InlineData("88888888888")]
        [InlineData("99999999999")]
        [InlineData("00000000000")]
        public void AgainstInvalidCpf_ThrowsArgumentException_WhenCpfIsInvalid(string cpf)
        {
            Assert.Throws<ArgumentException>(() => CpfValidator.AgainstInvalidCpf(cpf));
        }

        [Theory]
        [InlineData("58091454007")]
        [InlineData("11144477735")]
        [InlineData("00000000191")]
        public void AgainstInvalidCpf_DoesNotThrow_WhenCpfIsValid(string cpf)
        {
            //not throws
            CpfValidator.AgainstInvalidCpf(cpf);
        }
    }
}