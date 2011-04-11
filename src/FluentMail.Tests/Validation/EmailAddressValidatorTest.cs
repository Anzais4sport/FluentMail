using FluentMail.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentMail.Tests.Validation
{
    [TestClass]
    public class EmailAddressValidatorTest
    {
        private readonly EmailAddressValidator validator = new EmailAddressValidator();

        [TestMethod]
        public void ShouldValidateLongAddress() {
            Assert.IsTrue(validator.Validate("jack.bauer@secret.dev.wm.ctu.gov.us"));
        }

        [TestMethod]
        public void ShouldValidateShortAddress() {
            Assert.IsTrue(validator.Validate("j@c.gov"));
        }

        [TestMethod]
        public void ShouldNotValidateInvalidAddresses() {
            Assert.IsFalse(validator.Validate("22 Acacia Avenue"));
            Assert.IsFalse(validator.Validate("ctu.gov.us"));
            Assert.IsFalse(validator.Validate("@ctu.gov.us"));
            Assert.IsFalse(validator.Validate(""));
        }

        [TestMethod]
        public void ShouldNotValidateNullAddress() {
            Assert.IsFalse(validator.Validate(null));
        }
    }
}