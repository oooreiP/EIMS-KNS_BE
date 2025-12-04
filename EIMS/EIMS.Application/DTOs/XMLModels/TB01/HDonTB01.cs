using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EIMS.Application.DTOs.XMLModels.TB01
{
    public class HDonTB01
    {
        [XmlElement("STT")]
        public int STT { get; set; }

        [XmlElement("MCQTCap")]
        public string MCQTCap { get; set; } = "";

        [XmlElement("KHMSHDon")]
        public string KHMSHDon { get; set; } = "";

        [XmlElement("KHHDon")]
        public string KHHDon { get; set; } = "";

        [XmlElement("SHDon")]
        public string SHDon { get; set; } = "";

        [XmlElement("NLap")]
        public string NLap { get; set; } = "";

        [XmlElement("LADHDDT")]
        public int LADHDDT { get; set; } = 1;

        [XmlElement("TCTBao")]
        public int TCTBao { get; set; } // 1: Hủy, 2: ĐC...

        // TRƯỜNG QUAN TRỌNG NHẤT: 1=Tiếp nhận, 2=Không tiếp nhận
        [XmlElement("TTTNCCQT")]
        public int TTTNCCQT { get; set; }

        [XmlElement("DSLDKTNhan")]
        public DSLDKTNhan? DSLDKTNhan { get; set; }
    }
}
