using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EIMS.Application.DTOs.XMLModels
{
    public class TToan
    {
        public decimal TgTCThue { get; set; }
        public decimal TgTThue { get; set; }
        public decimal TgTTTBSo { get; set; }

        [XmlElement("TgTTTBChu")]
        public string? TgTTTBChu { get; set; }
    }
}
