using System;
using System.ComponentModel;
using System.Configuration;

namespace FluentMail.Configuration
{
    public class FluentMailSection : ConfigurationSection
    {
        [ConfigurationProperty("postalService", IsRequired = true)]
        public PostalServiceElement PostalService
        {
            get { return (PostalServiceElement) this["postalService"]; }
        }

        [ConfigurationProperty("emailAddressValidator", IsRequired = true)]
        public EmailAddressValidatorElement EmailAddressValidator
        {
            get { return (EmailAddressValidatorElement) this["emailAddressValidator"]; }
        }

        public class PostalServiceElement : ConfigurationElement
        {
            [ConfigurationProperty("type", IsRequired = true)]
            [TypeConverter(typeof(TypeNameConverter))]
            public Type Type
            {
                get { return (Type) this["type"]; }
                set { this["type"] = value; }
            }

            public override bool IsReadOnly()
            {
                return false;
            }
        }

        public class EmailAddressValidatorElement : ConfigurationElement
        {
            [ConfigurationProperty("type", IsRequired = true)]
            [TypeConverter(typeof(TypeNameConverter))]
            public Type Type
            {
                get { return (Type)this["type"]; }
            }
        }
    }
}
