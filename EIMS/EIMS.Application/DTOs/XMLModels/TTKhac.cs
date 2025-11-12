using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EIMS.Application.DTOs.XMLModels
{
    public class TTKhac
    {
        [XmlElement("TTin")]
        public List<TTin> TTin { get; set; } = new();
    }
}
