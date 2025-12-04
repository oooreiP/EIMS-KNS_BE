using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EIMS.Application.DTOs.XMLModels.TB04
{
    public class DLieuTB04
    {
        [XmlElement("TBao")]
        public TBao04 TBao { get; set; } = new();
    }
}
