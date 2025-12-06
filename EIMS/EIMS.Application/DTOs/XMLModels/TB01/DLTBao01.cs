using EIMS.Application.DTOs.XMLModels.TB04;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EIMS.Application.DTOs.XMLModels.TB01
{
    public class DLTBao01
    {
        [XmlElement("PBan")]
        public string PBan { get; set; } = "2.1.0";

        [XmlElement("MSo")]
        public string MSo { get; set; } = "01/TB-SSĐT"; // Theo bảng

        [XmlElement("Ten")]
        public string Ten { get; set; } = "Thông báo về việc tiếp nhận và kết quả xử lý về việc hóa đơn điện tử đã lập có sai sót";

        [XmlElement("DDanh")]
        public string DDanh { get; set; } = "";

        [XmlElement("TCQTCTren")]
        public string TCQTCTren { get; set; } = ""; // Tên CQT cấp trên

        [XmlElement("TCQT")]
        public string TCQT { get; set; } = ""; // Tên CQT ra thông báo

        [XmlElement("TNNT")]
        public string TNNT { get; set; } = ""; // Tên người nộp thuế

        [XmlElement("MST")]
        public string MST { get; set; } = "";

        [XmlElement("MDVQHNSach")]
        public string MDVQHNSach { get; set; } = "";

        [XmlElement("MGDDTu")]
        public string MGDDTu { get; set; } = ""; // Mã giao dịch điện tử (Bắt buộc)

        [XmlElement("TGNhan")]
        public string TGNhan { get; set; } = ""; // Thời gian CQT nhận (Ngày)

        [XmlElement("NTBNNT")]
        public string NTBNNT { get; set; } = ""; // Ngày NNT gửi thông báo

        [XmlElement("STTThe")]
        public int STTThe { get; set; } = 1;

        [XmlElement("HThuc")]
        public string HThuc { get; set; } = "Chữ ký số";

        [XmlElement("CDanh")]
        public string CDanh { get; set; } = "Thủ trưởng cơ quan thuế";

        // Danh sách lý do không tiếp nhận chung (nếu có)
        [XmlElement("DSLDKTNhan")]
        public DSLDKTNhan? DSLDKTNhan { get; set; }

        // Danh sách hóa đơn xử lý (Quan trọng)
        [XmlElement("DSHDon")]
        public DSHDonWrapper01 DSHDon { get; set; } = new();
    }
}
