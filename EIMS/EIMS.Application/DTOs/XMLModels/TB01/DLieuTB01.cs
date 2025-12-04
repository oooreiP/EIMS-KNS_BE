using EIMS.Application.DTOs.XMLModels.TB04;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EIMS.Application.DTOs.XMLModels.TB01
{
    public class DLieuTB01
    {
        [XmlElement("TBao")]
        public TBao01 TBao { get; set; } = new();
    }
}
