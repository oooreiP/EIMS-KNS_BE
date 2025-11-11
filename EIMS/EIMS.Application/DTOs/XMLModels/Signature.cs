using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EIMS.Application.DTOs.XMLModels
{
    public class Signature
    {
        [XmlElement("SignatureValue")]
        public string? SignatureValue { get; set; }
        [XmlElement("KeyInfo")]
        public string? KeyInfo { get; set; }
    }
}
