using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EIMS.Application.DTOs.XMLModels.TB01
{
    public class STBao
    {
        [XmlElement("So")]
        public string So { get; set; } = ""; // Số thông báo

        [XmlElement("NTBao")]
        public string NTBao { get; set; } = ""; // Ngày thông báo
    }
}
