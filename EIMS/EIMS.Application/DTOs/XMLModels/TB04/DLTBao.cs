using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EIMS.Application.DTOs.XMLModels.TB04
{
    public class DLTBao
    {
        [XmlElement("MST")]
        public string MST { get; set; }

        [XmlElement("MDVQHNSach")]
        public string MDVQHNSach { get; set; } = ""; // Mã đơn vị quan hệ ngân sách (thường để trống nếu không có)

        [XmlElement("DDanh")]
        public string DDanh { get; set; } = ""; // Địa danh (VD: Hà Nội)

        [XmlElement("NTBao")]
        public string NTBao { get; set; } // Ngày thông báo (yyyy-MM-dd)

        [XmlElement("DSHDon")]
        public DSHDonWrapper DSHDon { get; set; } = new();
    }
}
