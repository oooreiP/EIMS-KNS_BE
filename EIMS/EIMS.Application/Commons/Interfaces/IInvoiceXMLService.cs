using EIMS.Domain.Entities;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace EIMS.Application.Commons.Interfaces
{
    public interface IInvoiceXMLService
    {
        Task<XmlDocument> LoadXmlFromUrlAsync(string url);
        Task<string> DownloadStringAsync(string path);
        Task<string> UploadXmlAsync(XmlDocument xmlDoc, string fileName);
        Task<Result<X509Certificate2>> GetCertificateAsync(int companyId);
        void EmbedMccqtIntoXml(XmlDocument xmlDoc, string mccqtValue);
        Task<Result> ValidateXmlForIssuanceAsync(string xmlUrl);
        Task<string> GenerateNextNotificationNumberAsync();
        Task<string> GenerateAndUploadXmlAsync(Invoice fullInvoice);
        Task<(XmlDocument XmlDoc, string MessageId)> Generate04SSXmlDocumentAsync(InvoiceErrorNotification notification);
    }
}
