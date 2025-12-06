using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EIMS.Application.DTOs.XMLModels.TB01
{
    [XmlRoot(ElementName = "Signature", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
    public class SignatureWrapper
    {
        [XmlElement("SignedInfo")]
        public SignedInfo SignedInfo { get; set; } = new();

        [XmlElement("SignatureValue")]
        public string SignatureValue { get; set; } = ""; // Chuỗi Base64 chữ ký

        [XmlElement("KeyInfo")]
        public KeyInfo KeyInfo { get; set; } = new();

        [XmlElement("Object")]
        public List<SignatureObject> Object { get; set; } = new(); // Chứa SigningTime
    }

    public class SignedInfo
    {
        [XmlElement("CanonicalizationMethod")]
        public AlgorithmMethod CanonicalizationMethod { get; set; } = new();

        [XmlElement("SignatureMethod")]
        public AlgorithmMethod SignatureMethod { get; set; } = new();

        [XmlElement("Reference")]
        public Reference Reference { get; set; } = new();
    }

    public class AlgorithmMethod
    {
        [XmlAttribute("Algorithm")]
        public string Algorithm { get; set; } = "";
    }

    public class Reference
    {
        [XmlAttribute("URI")]
        public string URI { get; set; } = "";

        [XmlElement("Transforms")]
        public Transforms Transforms { get; set; } = new();

        [XmlElement("DigestMethod")]
        public AlgorithmMethod DigestMethod { get; set; } = new();

        [XmlElement("DigestValue")]
        public string DigestValue { get; set; } = "";
    }

    public class Transforms
    {
        [XmlElement("Transform")]
        public List<AlgorithmMethod> Transform { get; set; } = new();
    }

    public class KeyInfo
    {
        [XmlElement("X509Data")]
        public X509Data X509Data { get; set; } = new();
    }

    public class X509Data
    {
        [XmlElement("X509SubjectName")]
        public string X509SubjectName { get; set; } = "";

        [XmlElement("X509Certificate")]
        public string X509Certificate { get; set; } = "";
    }

    // --- Phần mở rộng cho CQT Việt Nam (SigningTime) ---
    public class SignatureObject
    {
        [XmlElement("SignatureProperties")]
        public SignatureProperties SignatureProperties { get; set; } = new();
    }

    public class SignatureProperties
    {
        [XmlElement("SignatureProperty")]
        public SignatureProperty SignatureProperty { get; set; } = new();
    }

    public class SignatureProperty
    {
        [XmlAttribute("Target")]
        public string Target { get; set; } = "";

        [XmlElement("SigningTime", Namespace = "")] // Thường thẻ này không có namespace W3C
        public string SigningTime { get; set; } = "";
    }
}
