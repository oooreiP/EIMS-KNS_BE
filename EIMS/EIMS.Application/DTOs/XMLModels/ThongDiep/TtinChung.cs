using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EIMS.Application.DTOs.XMLModels.ThongDiep
{
    public class TtinChung
    {
        [XmlElement("PBan")]
        public string PhienBan { get; set; } = "2.1.0";
        [XmlElement("MNGui")]
        public string MaNguoiGui { get; set; } = string.Empty;
        [XmlElement("MNNhan")]
        public string MaNguoiNhan { get; set; } = "TCT";
        [XmlElement("MLTDiep")]
        public string MaLoaiThongDiep { get; set; } = string.Empty;
        [XmlElement("MTDTChieu")]
        public string MaThongDiepDoiChieu { get; set; } = string.Empty;
        [XmlElement("MTDiep")]
        public string MaThongDiep { get; set; } = Guid.NewGuid().ToString("N").ToUpper();
        [XmlElement("MST")]
        public string MaSoThue { get; set; } = string.Empty;
        [XmlElement("NLap")]
        public string NgayLap { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
        [XmlElement("TDLap")]
        public string ThoiDiemLap { get; set; } = DateTime.Now.ToString("HH:mm:ss");
        [XmlElement("SLuong")]
        public int SoLuong { get; set; } = 1;
    }
}
