using EIMS.Application.DTOs.XMLModels.TB04;
using EIMS.Application.DTOs.XMLModels.ThongDiep;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EIMS.Application.DTOs.XMLModels.TB01
{
    [XmlRoot("TDiep")]
    public class TDiepTB01
    {
        [XmlElement("TTChung")]
        public TtinChung TTChung { get; set; } = new();

        [XmlElement("DLieu")]
        public DLieuTB01 DLieu { get; set; } = new();
    }
}
