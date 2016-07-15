﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServiceCSharp.Core;
namespace WebServiceTests.Main.Calculator.AdditionRequest
{
    public class AdditionRequest
    {
        Envelope envelope;
        public AdditionRequest(int a, int b)
        {
            envelope = new Envelope();
            Add addValue = new Add();
            addValue.intA = (byte)a;
            addValue.intB = byte.Parse(b.ToString());
            EnvelopeBody addBody = new EnvelopeBody();
            addBody.Add = addValue;
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

        private Add addField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://tempuri.org/")]
        public Add Add
        {
            get
            {
                return this.addField;
            }
            set
            {
                this.addField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://tempuri.org/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://tempuri.org/", IsNullable = false)]
    public partial class Add
    {

        private byte intAField;

        private byte intBField;

        /// <remarks/>
        public byte intA
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
        public byte intB
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
