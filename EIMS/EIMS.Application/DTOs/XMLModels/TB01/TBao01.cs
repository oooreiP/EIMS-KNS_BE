using EIMS.Application.DTOs.XMLModels.TB04;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EIMS.Application.DTOs.XMLModels.TB01
{
    public class TBao01
    {
        [XmlElement("DLTBao")]
        public DLTBao01 DLTBao { get; set; } = new();

        [XmlElement("STBao")]
        public STBao STBao { get; set; } = new();

        [XmlElement("DSCKS")]
        public DSCKS_CQT? DSCKS { get; set; }
    }
}
