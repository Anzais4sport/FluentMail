using System.Collections.Generic;
using FluentMail.Transport;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace FluentMail.Tests
{
    [TestClass]
    public class EmailMessageTest
    {
        [TestMethod]
        [ExpectedException(typeof(IncompleteEmailException))]
        public void ShouldRequireFromAddressToSendMessage()
        {
            new EmailMessage().Send();
        }

        [TestMethod]
        [ExpectedException(typeof(IncompleteEmailException))]
        public void ShouldRequireToAddressToSendMessage()
        {
            new EmailMessage()
                .From("bauer@ctu.gov.us")
                .Send();
        }

        [TestMethod]
        [ExpectedException(typeof(IncompleteEmailException))]
        public void ShouldRequireSubjectToSendMessage()
        {
            new EmailMessage()
                .From("bauer@ctu.gov.us")
                .To("chloe@ctu.gov.us")
                .Send();
        }

        [TestMethod]
        [ExpectedException(typeof(IncompleteEmailException))]
        public void ShouldRequireBodyToSendMessage()
        {
            new EmailMessage()
                .From("bauer@ctu.gov.us")
                .To("chloe@ctu.gov.us")
                .WithSubject("Subject")
                .Send();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidEmailAddressException))]
        public void ShouldRaiseExceptionWhenFromAddressIsInvalid()
        {
            new EmailMessage()
                .From("ctu.gov.us")
                .To("chloe@ctu.gov.us")
                .WithSubject("Subject")
                .WithBody("Body")
                .Send();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidEmailAddressException))]
        public void ShouldRaiseExceptionWhenToAddressIsInvalid()
        {
            new EmailMessage()
                .From("bauer@ctu.gov.us")
                .To("chloectu.gov.us")
                .WithSubject("Subject")
                .WithBody("Body")
                .Send();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidEmailAddressException))]
        public void ShouldRaiseExceptionWhenCcAddressIsInvalid()
        {
            new EmailMessage()
                .From("bauer@ctu.gov.us")
                .To("chloe@ctu.gov.us")
                .Cc("ctu.gov.us")
                .WithSubject("Subject")
                .WithBody("Body")
                .Send();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidEmailAddressException))]
        public void ShouldRaiseExceptionWhenBccAddressIsInvalid()
        {
            new EmailMessage()
                .From("bauer@ctu.gov.us")
                .To("chloe@ctu.gov.us")
                .Cc("tony@ctu.gov.us")
                .Bcc("ctu.gov.us")
                .WithSubject("Subject")
                .WithBody("Body")
                .Send();
        }

        [TestMethod]
        public void ShouldCallPostalServiceSendWhenParametersAreCorrect()
        {
            new EmailMessage()
                .From("bauer@ctu.gov.us")
                .To("chloe@ctu.gov.us")
                .WithSubject("Subject")
                .WithBody("Body")
                .Send();

            Assert.AreEqual(1, PostalServiceFake.SendCount);
        }

        [TestMethod]
        public void ShouldAllowManyTos()
        {
            EmailMessage email = (EmailMessage) new EmailMessage()
                .To("bauer@ctu.gov.us").To("chloe@ctu.gov.us").To("tony@ctu.gov.us");

            IList<string> addresses = email.ToAddresses;

            Assert.IsNotNull(addresses);
            Assert.AreEqual(3, addresses.Count);

            Assert.IsTrue(addresses.Contains("bauer@ctu.gov.us"));
            Assert.IsTrue(addresses.Contains("chloe@ctu.gov.us"));
            Assert.IsTrue(addresses.Contains("tony@ctu.gov.us"));
        }

        [TestMethod]
        public void ShouldAllowManyTosInTheSameMethod()
        {
            EmailMessage email = (EmailMessage) new EmailMessage()
                .To("bauer@ctu.gov.us", "chloe@ctu.gov.us", "tony@ctu.gov.us");

            IList<string> addresses = email.ToAddresses;

            Assert.IsNotNull(addresses);
            Assert.AreEqual(3, addresses.Count);

            Assert.IsTrue(addresses.Contains("bauer@ctu.gov.us"));
            Assert.IsTrue(addresses.Contains("chloe@ctu.gov.us"));
            Assert.IsTrue(addresses.Contains("tony@ctu.gov.us"));
        }

        [TestMethod]
        public void ShouldIgnoreRepeatedTos()
        {
            EmailMessage email = (EmailMessage) new EmailMessage()
                .To("bauer@ctu.gov.us").To("bauer@ctu.gov.us").To("bauer@ctu.gov.us");

            IList<string> addresses = email.ToAddresses;

            Assert.IsNotNull(addresses);
            Assert.AreEqual(1, addresses.Count);

            Assert.IsTrue(addresses.Contains("bauer@ctu.gov.us"));
        }

        [TestMethod]
        public void ShouldAllowManyCcs()
        {
            EmailMessage email = (EmailMessage) new EmailMessage()
                .Cc("bauer@ctu.gov.us").Cc("chloe@ctu.gov.us").Cc("tony@ctu.gov.us");

            IList<string> addresses = email.CcAddresses;

            Assert.IsNotNull(addresses);
            Assert.AreEqual(3, addresses.Count);

            Assert.IsTrue(addresses.Contains("bauer@ctu.gov.us"));
            Assert.IsTrue(addresses.Contains("chloe@ctu.gov.us"));
            Assert.IsTrue(addresses.Contains("tony@ctu.gov.us"));
        }

        [TestMethod]
        public void ShouldAllowManyCcsInTheSameMethod()
        {
            EmailMessage email = (EmailMessage) new EmailMessage()
                .Cc("bauer@ctu.gov.us", "chloe@ctu.gov.us", "tony@ctu.gov.us");

            IList<string> addresses = email.CcAddresses;

            Assert.IsNotNull(addresses);
            Assert.AreEqual(3, addresses.Count);

            Assert.IsTrue(addresses.Contains("bauer@ctu.gov.us"));
            Assert.IsTrue(addresses.Contains("chloe@ctu.gov.us"));
            Assert.IsTrue(addresses.Contains("tony@ctu.gov.us"));
        }

        [TestMethod]
        public void ShouldIgnoreRepeatedCcs()
        {
            EmailMessage email = (EmailMessage) new EmailMessage()
                .Cc("bauer@ctu.gov.us").Cc("bauer@ctu.gov.us").Cc("bauer@ctu.gov.us");

            IList<string> addresses = email.CcAddresses;

            Assert.IsNotNull(addresses);
            Assert.AreEqual(1, addresses.Count);

            Assert.IsTrue(addresses.Contains("bauer@ctu.gov.us"));
        }

        [TestMethod]
        public void ShouldAllowManyBccs()
        {
            EmailMessage email = (EmailMessage) new EmailMessage()
                .Bcc("bauer@ctu.gov.us").Bcc("chloe@ctu.gov.us").Bcc("tony@ctu.gov.us");

            IList<string> addresses = email.BccAddresses;

            Assert.IsNotNull(addresses);
            Assert.AreEqual(3, addresses.Count);

            Assert.IsTrue(addresses.Contains("bauer@ctu.gov.us"));
            Assert.IsTrue(addresses.Contains("chloe@ctu.gov.us"));
            Assert.IsTrue(addresses.Contains("tony@ctu.gov.us"));
        }

        [TestMethod]
        public void ShouldAllowManyBccsInTheSameMethod()
        {
            EmailMessage email = (EmailMessage) new EmailMessage()
                .Bcc("bauer@ctu.gov.us", "chloe@ctu.gov.us", "tony@ctu.gov.us");

            IList<string> addresses = email.BccAddresses;

            Assert.IsNotNull(addresses);
            Assert.AreEqual(3, addresses.Count);

            Assert.IsTrue(addresses.Contains("bauer@ctu.gov.us"));
            Assert.IsTrue(addresses.Contains("chloe@ctu.gov.us"));
            Assert.IsTrue(addresses.Contains("tony@ctu.gov.us"));
        }

        [TestMethod]
        public void ShouldIgnoreRepeatedBccs()
        {
            EmailMessage email = (EmailMessage) new EmailMessage()
                .Bcc("bauer@ctu.gov.us").Bcc("bauer@ctu.gov.us").Bcc("bauer@ctu.gov.us");

            IList<string> addresses = email.BccAddresses;

            Assert.IsNotNull(addresses);
            Assert.AreEqual(1, addresses.Count);

            Assert.IsTrue(addresses.Contains("bauer@ctu.gov.us"));
        }
    }
}