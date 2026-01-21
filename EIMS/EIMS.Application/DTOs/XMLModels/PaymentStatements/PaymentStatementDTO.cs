using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EIMS.Application.DTOs.XMLModels.PaymentStatements
{
    [XmlRoot("PaymentStatement")]
    public class PaymentStatementDTO
    {
        [XmlElement("ProviderInfo")]
        public ProviderInfoDTO ProviderInfo { get; set; }

        [XmlElement("HeaderInfo")]
        public HeaderInfoDTO HeaderInfo { get; set; }

        // Danh sách các dòng chi tiết
        [XmlArray("Items")]
        [XmlArrayItem("Item")]
        public List<StatementItemDTO> Items { get; set; } = new List<StatementItemDTO>();

        [XmlElement("Summary")]
        public StatementSummaryDTO Summary { get; set; }
    }
}
