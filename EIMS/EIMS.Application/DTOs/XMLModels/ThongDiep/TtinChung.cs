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
        public string PhienBan { get; set; } = "2.1.0"; 
        public string MaNguoiGui { get; set; } = string.Empty;
        public string MaNguoiNhan { get; set; } = "TCT";
        public string MaLoaiThongDiep { get; set; } = string.Empty;
        public string MaThongDiepDoiChieu { get; set; } = string.Empty;
        public string MaThongDiep { get; set; } = Guid.NewGuid().ToString("N").ToUpper();
        public string MaSoThue { get; set; } = string.Empty;
        public string NgayLap { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
        public string ThoiDiemLap { get; set; } = DateTime.Now.ToString("HH:mm:ss");
        public int SoLuong { get; set; } = 1;
    }
}
