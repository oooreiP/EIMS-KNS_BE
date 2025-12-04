using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.XMLModels
{
    public class HHDVu
    {
        public int STT { get; set; }
        public string THHDVu { get; set; } = "";
        public string DVTinh { get; set; } = "";
        public double SLuong { get; set; }
        public decimal DGia { get; set; }
        public decimal ThTien { get; set; }
        public string TSuat { get; set; } = "8%";
    }
}
