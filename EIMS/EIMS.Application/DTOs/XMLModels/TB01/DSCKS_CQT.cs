using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EIMS.Application.DTOs.XMLModels.TB01
{
    public class DSCKS_CQT
    {
        [XmlElement("TTCQT")]
        public SignatureWrapper? TTCQT { get; set; } // Chữ ký thủ trưởng

        [XmlElement("CQT")]
        public SignatureWrapper? CQT { get; set; } // Chữ ký con dấu CQT (Bắt buộc)

        [XmlElement("CCKSKhac")]
        public SignatureWrapper? CCKSKhac { get; set; }
    }
}
