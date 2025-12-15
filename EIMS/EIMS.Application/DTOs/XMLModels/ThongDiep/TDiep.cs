using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EIMS.Application.DTOs.XMLModels.ThongDiep
{
    [XmlRoot(ElementName = "TDiep", Namespace = "http://tempuri.org/TDiepSchema.xsd")]
    public class TDiep
    {
        [XmlElement("TTChung")]
        public TtinChung TtinChung { get; set; }

        [XmlElement("DLieu")]
        public TDiepDLieu TDiepDLieu { get; set; }
    }
}
