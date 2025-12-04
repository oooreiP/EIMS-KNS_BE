using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EIMS.Application.DTOs.XMLModels.TB04
{
    public class HDonTB04
    {
        [XmlElement("STT")]
        public int STT { get; set; }

        [XmlElement("MCCQT")]
        public string MCCQT { get; set; } // Mã CQT của hóa đơn sai (34 ký tự)

        [XmlElement("KHMSHDon")]
        public string KHMSHDon { get; set; } // Ký hiệu mẫu số (1C22...)

        [XmlElement("KHHDon")]
        public string KHHDon { get; set; } // Ký hiệu hóa đơn (C22TYY)

        [XmlElement("SHDon")]
        public string SHDon { get; set; } // Số hóa đơn

        [XmlElement("Ngay")]
        public string Ngay { get; set; } // Ngày lập hóa đơn (yyyy-MM-dd) - Code cũ là NgayLap

        [XmlElement("LADHDDT")]
        public int LADHDDT { get; set; } = 1; // Loại áp dụng HĐĐT (1: Nghị định 123)

        [XmlElement("TCTBao")]
        public int TCTBao { get; set; } // Tính chất thông báo (1: Hủy, 2: Điều chỉnh, 3: Thay thế, 4: Giải trình) - Code cũ là TChat

        [XmlElement("LDo")]
        public string LDo { get; set; } // Lý do
    }
}
