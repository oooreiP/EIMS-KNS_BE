using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EIMS.Application.DTOs.XMLModels
{
    /// <summary>
    /// Thông tin hóa đơn liên quan (Dùng cho hóa đơn Điều chỉnh/Thay thế)
    /// </summary>
    public class TTHDLQuan
    {
        /// <summary>
        /// Tính chất hóa đơn (1: Thay thế, 2: Điều chỉnh)
        /// </summary>
        [XmlElement("TCHDon")]
        public int TCHDon { get; set; }

        /// <summary>
        /// Loại hóa đơn có liên quan (1: HĐĐT, 2: HĐ đặt in/tự in, 3: HĐ giấy...)
        /// Thường mặc định là 1 (HĐĐT)
        /// </summary>
        [XmlElement("LHDCLQuan")]
        public int LHDCLQuan { get; set; } = 1;

        /// <summary>
        /// Ký hiệu mẫu số hóa đơn có liên quan (VD: 1)
        /// </summary>
        [XmlElement("KHMSHDCLQuan")]
        public string KHMSHDCLQuan { get; set; } = string.Empty;

        /// <summary>
        /// Ký hiệu hóa đơn có liên quan (VD: C22TYY)
        /// </summary>
        [XmlElement("KHHDCLQuan")]
        public string KHHDCLQuan { get; set; } = string.Empty;

        /// <summary>
        /// Số hóa đơn có liên quan (VD: 0000123)
        /// </summary>
        [XmlElement("SHDCLQuan")]
        public string SHDCLQuan { get; set; } = string.Empty;

        /// <summary>
        /// Ngày lập hóa đơn có liên quan (yyyy-MM-dd)
        /// </summary>
        [XmlElement("NLHDCLQuan")]
        public string NLHDCLQuan { get; set; } = string.Empty;

        /// <summary>
        /// Ghi chú (Lý do điều chỉnh/thay thế)
        /// </summary>
        [XmlElement("Gchu")]
        public string Gchu { get; set; } = string.Empty;
    }
}
