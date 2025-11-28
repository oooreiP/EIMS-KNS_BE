using EIMS.Application.Commons;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Infrastructure.Service
{
    public class MockTaxApiClient : ITaxApiClient
    {
        public Task<TaxApiResponse> SendInvoiceAsync(string xmlPayload, string referenceId)
        {           
            var soThongBao = $"2025/{new Random().Next(100000000, 999999999)}";
            var error = XmlHelpers.Validate(xmlPayload);
            if (error.Any())
            {
                var mlTDiepLoi = "204";
                var mtDiepPhanHoi = XmlHelpers.GenerateMTDiep("K", "0311357436");
                var errorResponseXml = $@"
                <TDiep>
                    <TTChung>
                        <PBan>2.1.0</PBan>
                       <MNGui>TCT</MNGui>
                        <MNNhan>K0311357436</MNNhan>
                        <MLTDiep>{mlTDiepLoi}</MLTDiep>
                        <MTDiep>{mtDiepPhanHoi}</MTDiep>
                        <MTDTChieu>{referenceId}</MTDTChieu>
                    </TTChung>
                    <DLieu>
                        <TBao>
                            <MTDiep>{referenceId}</MTDiep>
                            <NNhan>{DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")}</NNhan>
                            <TTTNhan>1</TTTNhan> {error} </TBao>
                    </DLieu>
                </TDiep>";

            return Task.FromResult(new TaxApiResponse
            {
                IsSuccess = false,
                MTDiep = mtDiepPhanHoi,
                MLTDiep = mlTDiepLoi,
                RawResponse = errorResponseXml
            });
        }
            var mlTDiepThanhCong = "202";
            var mtDiepPhanHoiThanhCong = XmlHelpers.GenerateMTDiep("TCT");
            var mccqt = "A" + Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N").ToUpper();

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
