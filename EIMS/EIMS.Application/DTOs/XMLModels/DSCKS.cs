using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EIMS.Application.DTOs.XMLModels
{
    public class DSCKS
    {
        [XmlElement("NBan")]
        public Signature? NBan { get; set; }

        [XmlElement("NMua")]
        public Signature? NMua { get; set; }

        [XmlElement("CQT")]
        public Signature? CQT { get; set; }
    }
}
