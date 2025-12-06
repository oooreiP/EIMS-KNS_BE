using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EIMS.Application.DTOs.XMLModels.TB04
{
    public class DSHDonWrapper
    {
        [XmlElement("HDon")]
        public List<HDonTB04> HDon { get; set; } = new();
    }
}
