using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EIMS.Application.DTOs.XMLModels.TB01
{
    public class DSLDKTNhan
    {
        [XmlElement("LDo")]
        public List<LDoError> LDo { get; set; } = new();
    }
}
