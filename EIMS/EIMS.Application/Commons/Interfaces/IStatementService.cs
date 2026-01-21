using EIMS.Application.DTOs.InvoiceStatement;
using EIMS.Application.DTOs.XMLModels.PaymentStatements;
using EIMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Commons.Interfaces
{
    public interface IStatementService
    {
        List<StatementProductDto> CalculateStatementProductSummary(IEnumerable<Invoice> invoices);
        Task<PaymentStatementDTO> GetPaymentRequestXmlAsync(InvoiceStatement statementEntity);
    }
}
