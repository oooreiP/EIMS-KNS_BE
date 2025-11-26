using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EIMS.Application.DTOs.XMLModels.ThongDiep
{
    public class TDiep
    {
        [XmlElement("TDiepTTChung")]
        public TtinChung TtinChung { get; set; }

        [XmlElement("DLieu")]
        public TDiepDLieu TDiepDLieu { get; set; }
    }
}
