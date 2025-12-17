using EIMS.Application.Commons;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.Commons.Mapping;
using EIMS.Application.DTOs.Results;
using EIMS.Application.DTOs.XMLModels.TB04;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Infrastructure.Service
{
    public class MockTaxApiClient : ITaxApiClient
    {
        public Task<TaxApiResponse> SendTaxMessageAsync(string xmlPayload, string? referenceId)
        {
            bool isInvoiceSubmission = xmlPayload.Contains("<MLTDiep>200</MLTDiep>") || xmlPayload.Contains("<MLTDiep>201</MLTDiep>");
            bool isErrorNotification = xmlPayload.Contains("<MLTDiep>300</MLTDiep>");
            if (isInvoiceSubmission)
            {
                return GenerateResponse202(xmlPayload, referenceId);
            }
            if (isErrorNotification)
            {
                return GenerateResponse301(xmlPayload, referenceId);
            }

            return Task.FromResult(new TaxApiResponse { IsSuccess = false, SoTBao = "Unknown Message Type" });

        }
        private Task<TaxApiResponse> GenerateResponse301(string xmlPayload, string? referenceId)
        {
            int currentYear = DateTime.Now.Year;
            string prefix = $"TB/{currentYear}/";
            var soThongBao = $"{prefix}{new Random().Next(100000000, 999999999)}";

            try
            {
                var requestObj = XmlHelpers.Deserialize<TDiepTB04>(xmlPayload);
                var dsHdon = requestObj.DLieu.TBao.DLTBao.DSHDon.HDon;
                if (dsHdon.Any(h => string.IsNullOrWhiteSpace(h.LDo)))
                {
                    return GenerateRejectResponse(requestObj, "VAL01", "Lý do sai sót không được để trống");
                }

                if (dsHdon.Any(h => h.LDo.Contains("đã hủy", StringComparison.OrdinalIgnoreCase)))
                {
                    return GenerateRejectResponse(requestObj, "ERR_STATUS", "Hóa đơn gốc đã bị hủy, không thể điều chỉnh");
                }
                if (dsHdon.Any(h => h.MCCQT.Contains("INVALID")))
                {
                    return GenerateRejectResponse(requestObj, "ERR_NOT_FOUND", "Không tìm thấy hóa đơn hoặc thông tin không khớp");
                }
                var responseObj = InvoiceXmlMapper.CreateResponse301FromRequest300(requestObj);
                string responseXml = XmlHelpers.Serialize(responseObj);
                var mtDiepPhanHoi = XmlHelpers.GenerateMTDiep("K", "0311357436");
                return Task.FromResult(new TaxApiResponse
                {
                    IsSuccess = true,
                    MLTDiep = "301",
                    MTDiep = mtDiepPhanHoi,
                    MTDThamChieu = responseObj.TTChung.MaThongDiep,
                    SoTBao = soThongBao,
                    RawResponse = responseXml
                });
            }
            catch (Exception ex)
            {
                return Task.FromResult(new TaxApiResponse
                {
                    IsSuccess = false,
                    SoTBao = "TB12",
                    RawResponse = $"Lỗi khi xử lý Mock 301: {ex.Message}"
                });
            }
        }
        private Task<TaxApiResponse> GenerateRejectResponse(TDiepTB04 request, string errCode, string errMsg)
        {
            var responseObj = InvoiceXmlMapper.CreateRejectResponse301(request, errCode, errMsg);
            string xml = XmlHelpers.Serialize(responseObj);
            return Task.FromResult(new TaxApiResponse
            {
                IsSuccess = true, 
                MLTDiep = "301",
                SoTBao = "TB_REJECT",
                RawResponse = xml
            });
        }
        private Task<TaxApiResponse> GenerateResponse202(string xmlPayload, string? referenceId)
        {
            int currentYear = DateTime.Now.Year;
            string prefix = $"TB/{currentYear}/";
            var soThongBao = $"{prefix}{new Random().Next(100000000, 999999999)}";
            //var error = XmlHelpers.Validate(xmlPayload);
            //if (error.Any())
            //{
            //    var mlTDiepLoi = "204";
            //    var mtDiepPhanHoi = XmlHelpers.GenerateMTDiep("K", "0311357436");
            //    var errorResponseXml = $@"
            //    <TDiep>
            //        <TTChung>
            //            <PBan>2.1.0</PBan>
            //           <MNGui>TCT</MNGui>
            //            <MNNhan>K0311357436</MNNhan>
            //            <MLTDiep>{mlTDiepLoi}</MLTDiep>
            //            <MTDiep>{mtDiepPhanHoi}</MTDiep>
            //            <MTDTChieu>{referenceId}</MTDTChieu>
            //        </TTChung>
            //        <DLieu>
            //            <TBao>
            //                <MTDiep>{referenceId}</MTDiep>
            //                <NNhan>{DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")}</NNhan>
            //                <TTTNhan>1</TTTNhan> {error} </TBao>
            //        </DLieu>
            //    </TDiep>";

            //    return Task.FromResult(new TaxApiResponse
            //    {
            //        IsSuccess = false,
            //        MTDiep = mtDiepPhanHoi,
            //        MTDThamChieu = referenceId,
            //        MLTDiep = mlTDiepLoi,
            //        RawResponse = errorResponseXml
            //    });
            //}
            var mlTDiepThanhCong = "202";
            var mtDiepPhanHoiThanhCong = XmlHelpers.GenerateMTDiep("TCT");
            var uniquePart = Guid.NewGuid().ToString("N"); // 32 ký tự
            var randomPart = new Random().Next(0, 10).ToString(); // 1 ký tự (0-9)
            var mccqt = $"A{uniquePart}{randomPart}".ToUpper();

            // --- XÂY DỰNG XML PHẢN HỒI THÀNH CÔNG THEO CẤU TRÚC MỚI ---
            var successResponseXml =
                $@"<TDiep>
            <TTChung>
                <PBan>2.1.0</PBan>
                <MNGui>TCT</MNGui>
                <MNNhan>K0311357436</MNNhan>
                <MLTDiep>{mlTDiepThanhCong}</MLTDiep>
                <MTDiep>{mtDiepPhanHoiThanhCong}</MTDiep>
                <MTDTChieu>{referenceId}</MTDTChieu>
            </TTChung>
            <DLieu>
                <TBao>
                    <MTDiep>{referenceId}</MTDiep>
                    <NNhan>{DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")}</NNhan>
                    <TTTNhan>0</TTTNhan> <MCCQT>{mccqt}</MCCQT>
                    <SoTBao>{soThongBao}</SoTBao>
                    <MoTa>Cấp mã hóa đơn thành công</MoTa>
                </TBao>
            </DLieu>
         </TDiep>";

            return Task.FromResult(new TaxApiResponse
            {
                IsSuccess = true,
                MTDiep = mtDiepPhanHoiThanhCong,
                MLTDiep = mlTDiepThanhCong,
                SoTBao = soThongBao,
                MCCQT = mccqt,
                RawResponse = successResponseXml
            });
        }
    }
}
