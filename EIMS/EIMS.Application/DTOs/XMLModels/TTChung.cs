using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.XMLModels
{
    public class TTChung
    {
        public string PBan { get; set; } = "2.1.0";
        public string THDon { get; set; } = "Hóa đơn giá trị gia tăng";
        public string KHMSHDon { get; set; } = "";
        public string KHHDon { get; set; } = "";
        public string SHDon { get; set; } = "";
        public string NLap { get; set; } = DateTime.Now.ToString("yyyy-MM-dd");
        public string DVTTe { get; set; } = "VND";
        public string HTTToan { get; set; } = "TM/CK";
        public string MSTTCGP { get; set; } = "";
    }
}
