using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EIMS.Application.DTOs.XMLModels
{
    public class DLHDon
    {
        [XmlAttribute("Id")]
        public string Id { get; set; } = "";

        [XmlElement("TTChung")]
        public TTChung TTChung { get; set; } = new();

        [XmlElement("NDHDon")]
        public NDHDon NDHDon { get; set; } = new();

        [XmlElement("TTKhac")]
        public TTKhac? TTKhac { get; set; }
    }
}
