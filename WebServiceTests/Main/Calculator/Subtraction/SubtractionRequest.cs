
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServiceCSharp.Core;
namespace WebServiceTests.Main.Calculator.Subtraction
{
    public class SubtractionRequest
    {
        Envelope envelope;
        public SubtractionRequest(int a, int b)
        {
            envelope = new Envelope();
            Subtract addValue = new Subtract();
            addValue.intA = a;
            addValue.intB = b;
            EnvelopeBody addBody = new EnvelopeBody();
            addBody.Subtract = addValue;
            envelope.Body = addBody;
        }

        public override string ToString()
        {
            return Utils.XMLSerializeObject(envelope);
        }

    }


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

        private Subtract subtractField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://tempuri.org/")]
        public Subtract Subtract
        {
            get
            {
                return this.subtractField;
            }
            set
            {
                this.subtractField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://tempuri.org/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://tempuri.org/", IsNullable = false)]
    public partial class Subtract
    {

        private int intAField;

        private int intBField;

        /// <remarks/>
        public int intA
        {
            get
            {
                return this.intAField;
            }
            set
            {
                this.intAField = value;
            }
        }

        /// <remarks/>
        public int intB
        {
            get
            {
                return this.intBField;
            }
            set
            {
                this.intBField = value;
            }
        }
    }


    

}