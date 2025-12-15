using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EIMS.Application.DTOs.XMLModels.ThongDiep
{
    public class TDiepDLieu
    {
        [XmlElement(ElementName = "HDon", Namespace = "http://tempuri.org/HDonSchema.xsd")]
        public HDon HDon { get; set; }
    }
}
