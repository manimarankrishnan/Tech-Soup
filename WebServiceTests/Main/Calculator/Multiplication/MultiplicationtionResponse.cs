using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServiceTests.Main.Calculator.Multiplication
{
    class MultiplicationResponse
    {


        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
        public partial class Envelope
        {

            private EnvelopeBody bodyField;

            /// <remarks/>
            public EnvelopeBody Body
            {
                get
                {
                    return this.bodyField;
                }
                set
                {
                    this.bodyField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public partial class EnvelopeBody
        {

            private MultiplyResponse multiplyResponseField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://tempuri.org/")]
            public MultiplyResponse MultiplyResponse
            {
                get
                {
                    return this.multiplyResponseField;
                }
                set
                {
                    this.multiplyResponseField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://tempuri.org/")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://tempuri.org/", IsNullable = false)]
        public partial class MultiplyResponse
        {

            private byte multiplyResultField;

            /// <remarks/>
            public byte MultiplyResult
            {
                get
                {
                    return this.multiplyResultField;
                }
                set
                {
                    this.multiplyResultField = value;
                }
            }
        }



    }
}
