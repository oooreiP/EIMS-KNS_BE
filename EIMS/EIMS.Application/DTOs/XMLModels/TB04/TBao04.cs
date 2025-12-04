using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EIMS.Application.DTOs.XMLModels.TB04
{
    public class TBao04
    {
        [XmlElement("PBan")]
        public string PBan { get; set; } = "2.0.1"; // Phiên bản

        [XmlElement("MSo")]
        public string MSo { get; set; } = "04/SS-HĐĐT";

        [XmlElement("Ten")]
        public string Ten { get; set; } = "Thông báo hóa đơn điện tử có sai sót";

        [XmlElement("Loai")]
        public int Loai { get; set; } = 1;

        [XmlElement("MCQT")]
        public string MCQT { get; set; } = ""; // Mã CQT quản lý

        [XmlElement("TCQT")]
        public string TCQT { get; set; } = ""; // Tên CQT quản lý

        [XmlElement("So")]
        public string So { get; set; } // Số thông báo (Do DN tự sinh, vd: TB001)

        [XmlElement("NTBCCQT")]
        public string NTBCCQT { get; set; } // Ngày thông báo cho CQT (yyyy-MM-dd)

        // Wrapper quan trọng bị thiếu ở code cũ
        [XmlElement("DLTBao")]
        public DLTBao DLTBao { get; set; } = new();

        [XmlElement("DSCKS")]
        public DSCKS? DSCKS { get; set; }
    }
}
