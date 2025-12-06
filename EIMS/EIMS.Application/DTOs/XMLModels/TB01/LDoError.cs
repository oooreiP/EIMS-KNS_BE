using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EIMS.Application.DTOs.XMLModels.TB01
{
    public class LDoError
    {
        [XmlElement("MLoi")]
        public string MLoi { get; set; } = "";
        [XmlElement("MTLoi")]
        public string MTLoi { get; set; } = ""; // Mô tả lỗi
        [XmlElement("HDXLy")]
        public string HDXLy { get; set; } = ""; // Hướng dẫn xử lý
        [XmlElement("GChu")]
        public string GChu { get; set; } = "";
    }
}
