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
        [XmlElement("HDon")]
        public HDon HDon { get; set; }
    }
}
