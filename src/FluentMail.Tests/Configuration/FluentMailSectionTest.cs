using System.Configuration;
using FluentMail.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentMail.Validation;

namespace FluentMail.Tests.Configuration
{
    [TestClass]
    public class FluentMailSectionTest
    {
        [TestMethod]
        public void ShouldLoadFluentMailSection()
        {
            FluentMailSection config = (FluentMailSection)
                System.Configuration.ConfigurationManager.GetSection("fluentMail");

            Assert.IsNotNull(config);
        }

        [TestMethod]
        public void ShouldLoadPostalServiceElement()
        {
            FluentMailSection config = (FluentMailSection)
                System.Configuration.ConfigurationManager.GetSection("fluentMail");

            Assert.IsNotNull(config.PostalService);
        }

        [TestMethod]
        public void ShouldReadPostalServiceType()
        {
            FluentMailSection config = (FluentMailSection)
                System.Configuration.ConfigurationManager.GetSection("fluentMail");

            Assert.IsNotNull(config.PostalService.Type);
            Assert.AreEqual(typeof(PostalServiceFake), config.PostalService.Type);
        }

        [TestMethod]
        public void ShouldThrowIfPostalServiceTypeIsNotProvided()
        {
            try
            {
                FluentMailSection config = (FluentMailSection)
                    System.Configuration.ConfigurationManager.GetSection("fluentMail-postalservice-not-provided");
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.IsTrue(ex.Message.Contains("Required attribute 'type' not found."));
            }
        }

        [TestMethod]
        public void ShouldThrowIfPostalServiceTypeIsEmpty()
        {
            try
            {
                FluentMailSection config = (FluentMailSection)
                    System.Configuration.ConfigurationManager.GetSection("fluentMail-postalservice-empty");

                Assert.Fail();
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.IsTrue(ex.Message.Contains("The value of the property 'type' cannot be parsed."));
            }
        }

        [TestMethod]
        public void ShouldLoadEmailAddressValidatorElement()
        {
            FluentMailSection config = (FluentMailSection)
                System.Configuration.ConfigurationManager.GetSection("fluentMail");

            Assert.IsNotNull(config.EmailAddressValidator);
        }

        [TestMethod]
        public void ShouldReadEmailAddressValidatorType()
        {
            FluentMailSection config = (FluentMailSection)
                System.Configuration.ConfigurationManager.GetSection("fluentMail");

            Assert.IsNotNull(config.EmailAddressValidator.Type);
            Assert.AreEqual(typeof(EmailAddressValidator), config.EmailAddressValidator.Type);
        }

        [TestMethod]
        public void ShouldThrowIfEmailAddressValidatorTypeIsNotProvided()
        {
            try
            {
                FluentMailSection config = (FluentMailSection)
                    System.Configuration.ConfigurationManager.GetSection("fluentMail-emailaddressvalidator-not-provided");
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.IsTrue(ex.Message.Contains("Required attribute 'type' not found."));
            }
        }

        [TestMethod]
        public void ShouldThrowIfEmailAddressValidatorTypeIsEmpty()
        {
            try
            {
                FluentMailSection config = (FluentMailSection)
                    System.Configuration.ConfigurationManager.GetSection("fluentMail-emailaddressvalidator-empty");

                Assert.Fail();
            }
            catch (ConfigurationErrorsException ex)
            {
                Assert.IsTrue(ex.Message.Contains("The value of the property 'type' cannot be parsed."));
            }
        }
    }
}
