using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EIMS.Application.DTOs.XMLModels
{
    [XmlRoot(ElementName = "HDon", Namespace = "http://tempuri.org/HDonSchema.xsd")]
    public class HDon
    {
        [XmlElement("DLHDon")]
        public DLHDon DLHDon { get; set; } = new();

        [XmlElement("MCCQT")]
        public MCCQT? MCCQT { get; set; }

        [XmlElement("DSCKS")]
        public DSCKS? DSCKS { get; set; }
    }
}
