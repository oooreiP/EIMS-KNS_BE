using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EIMS.Application.DTOs.XMLModels
{
    public class NDHDon
    {
        [XmlElement("NBan")]
        public Party NBan { get; set; } = new();

        [XmlElement("NMua")]
        public BMua NMua { get; set; } = new();

        [XmlArray("DSHHDVu")]
        [XmlArrayItem("HHDVu")]
        public List<HHDVu> DSHHDVu { get; set; } = new();

        [XmlElement("TToan")]
        public TToan TToan { get; set; } = new();
    }
}
