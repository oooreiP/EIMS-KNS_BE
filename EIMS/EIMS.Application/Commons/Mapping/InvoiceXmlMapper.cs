using EIMS.Application.DTOs.XMLModels;
using EIMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Commons.Mapping
{
    public class InvoiceXmlMapper
    {
        public static HDon MapInvoiceToXmlModel(Invoice invoice)
        {
            if (invoice == null)
                throw new ArgumentNullException(nameof(invoice));

            var model = new HDon
            {
                DLHDon = new DLHDon
                {
                    Id = $"INV_{invoice.InvoiceID}_{invoice.SignDate:yyyyMMddHHmmss}",
                    TTChung = new TTChung
                    {
                        PBan = "2.1.0",
                        THDon = "Hóa đơn giá trị gia tăng",
                        KHMSHDon = "1",                       // Ký hiệu mẫu số hóa đơn (bạn có thể cấu hình)
                        KHHDon = "EINV",                      // Ký hiệu hóa đơn
                        SHDon = invoice.InvoiceNumber.ToString() ?? "0000001",  // Số hóa đơn
                        NLap = invoice.CreatedAt.ToString("yyyy-MM-dd"),
                        DVTTe = "VND",
                        HTTToan = "TM/CK",
                        MSTTCGP = "0311357436" // Mã số thuế tổ chức phát hành
                    },
                    NDHDon = new NDHDon
                    {
                        // ---- Người bán ----
                        NBan = new Party
                        {
                            Ten = "CÔNG TY TNHH E-INVOICE SYSTEM",
                            MST = "0109999999",
                            DChi = "Hà Nội, Việt Nam",
                            DCTDTu = "support@einvoice.vn",
                            SDThoai = "0123456789",
                            TNHang = "Vietcombank",
                            STKNHang = "0123456789"
                        },
                        // ---- Người mua ----
                        NMua = new Party
                        {
                            Ten = invoice.Customer.CustomerName,
                            MST = invoice.Customer.TaxCode ?? "",
                            DChi = invoice.Customer.Address,
                            DCTDTu = invoice.Customer.ContactEmail,
                            SDThoai = invoice.Customer.ContactPhone ?? "",
                            TNHang = "",
                            STKNHang = ""
                        },
                        // ---- Danh sách hàng hóa ----
                        DSHHDVu = invoice.InvoiceItems.Select((item, index) => new HHDVu
                        {
                            STT = index + 1,
                            THHDVu = item.Product?.Name ?? "Sản phẩm",
                            DVTinh = item.Product?.Unit ?? "cái",
                            SLuong = item.Quantity,
                            DGia = item.UnitPrice,
                            ThTien = item.Amount,
                            TSuat = $"{Math.Round((item.VATAmount / Math.Max(1, item.Amount)) * 100, 0)}%"
                        }).ToList(),
                        // ---- Tổng tiền thanh toán ----
                        TToan = new TToan
                        {
                            TgTCThue = invoice.SubtotalAmount,
                            TgTThue = invoice.VATAmount,
                            TgTTTBSo = invoice.TotalAmount,
                            TgTTTBChu = invoice.TotalAmountInWords
                        }
                    },
                    TTKhac = new TTKhac
                    {
                        TTin = new List<TTin>
                    {
                        new TTin { TTruong = "NguoiKy", KDLieu = "string", DLieu = "Kế toán trưởng" },
                        new TTin { TTruong = "NgayKy", KDLieu = "date", DLieu = invoice.SignDate.ToString("yyyy-MM-dd") }
                    }
                    }
                },
                MCCQT = new MCCQT
                {
                    Id = $"MCCQT_{invoice.InvoiceID}",
                    Value = "CHUA_GUI_CO_QUAN_THUE" // sau khi gửi CQT thì thay bằng mã xác nhận
                }
            };

            return model;
        }
    }
}
