using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.XMLModels;
using EIMS.Application.DTOs.XMLModels.ThongDiep;
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
        private readonly IUnitOfWork _unitOfWork;

        public InvoiceXmlMapper(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public static HDon MapInvoiceToXmlModel(Invoice invoice)
        {
            if (invoice == null)
                throw new ArgumentNullException(nameof(invoice));
            var company = invoice.Company;
            if (company == null)
            {
                // Fallback or throw error if Company is strictly required
                // throw new Exception("Invoice is not linked to a Company (Seller).");
            }
            var template = invoice.Template;
            var serial = template.Serial;
            var prefix = serial.Prefix;
            string khmsHDon = prefix.PrefixID.ToString();
            string khHDon =
                $"{serial.SerialStatus.Symbol}" +
                $"{serial.Year}" +
                $"{serial.Tail}" +
                $"{serial.InvoiceType.Symbol}";
            var model = new HDon
            {
                DLHDon = new DLHDon
                {
                    Id = $"INV_{invoice.InvoiceID}_{invoice.SignDate:yyyyMMddHHmmss}",
                    TTChung = new TTChung
                    {
                        PBan = "2.1.0",
                        THDon = "Hóa đơn giá trị gia tăng",
                        KHMSHDon = khmsHDon,                       // Ký hiệu mẫu số hóa đơn (bạn có thể cấu hình)
                        KHHDon = khHDon,                      // Ký hiệu hóa đơn
                        SHDon = invoice.InvoiceNumber.ToString("0000000"),  // Số hóa đơn
                        NLap = invoice.SignDate?.ToString("yyyy-MM-dd"),
                        DVTTe = "VND",
                        HTTToan = "TM/CK",
                        MSTTCGP = "0311357436" // Mã số thuế tổ chức phát hành
                    },
                    NDHDon = new NDHDon
                    {
                        // ---- Người bán ----
                        NBan = new Party
                        {
                            Ten = company?.CompanyName ?? "MISSING COMPANY NAME",
                            MST = company?.TaxCode ?? "",
                            DChi = company?.Address ?? "",
                            DCTDTu = "", // Note: Company entity does not have an Email field yet.
                            SDThoai = company?.ContactPhone ?? "",
                            STKNHang = company?.AccountNumber ?? "",
                            TNHang = company?.BankName ?? ""
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
                            TSuat = invoice.VATRate switch
                            {
                                5 => "5%",
                                8 => "8%",
                                10 => "10%",
                                _ => "0%"
                            }
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
                        new TTin { TTruong = "NgayKy", KDLieu = "date", DLieu = invoice.SignDate?.ToString("yyyy-MM-dd") }
                    }
                    }
                },
                MCCQT = new MCCQT
                {
                    Id = $"MCCQT_{invoice.InvoiceID}",
                    Value = String.Empty
                }
            };

            return model;
        }
        public static TDiep MapThongDiepToXmlModel(Invoice invoice, TaxMessageCode messageCode, int dataCount, string? referenceMessageId = null)
        {
            var tTinChung = GenerateTTChung(messageCode, dataCount, referenceMessageId);
            if (invoice == null)
                throw new ArgumentNullException(nameof(invoice));
            var model = new TDiep
            {
                TtinChung = tTinChung,
                TDiepDLieu = new TDiepDLieu
                {
                    HDon = MapInvoiceToXmlModel(invoice),

                }
            };

            return model;
        }
        public static TtinChung GenerateTTChung(
                TaxMessageCode messageCode,
                int dataCount,
                string? referenceMessageId = null)
        {
            string providerCode = "K0311357436";
            // 1. Xác định nơi gửi và nơi nhận
            string mnGui, mnNhan;

            if (messageCode.FlowType == 1)
            {
                mnGui = providerCode;
                mnNhan = "TCT";
            }
            else
            {
                mnGui = "TCT";
                mnNhan = providerCode;
            }
            string mtDiep = mnGui + Guid.NewGuid().ToString("N").ToUpper();
            string? mtdtChieu = (messageCode.FlowType == 2) ? referenceMessageId : null;
            return new TtinChung
            {
                PhienBan = "2.1.0",
                MaNguoiGui = mnGui,
                MaNguoiNhan = mnNhan,
                MaLoaiThongDiep = messageCode.MessageCode,
                MaThongDiep = mtDiep,
                MaThongDiepDoiChieu = mtdtChieu,
                MaSoThue = "0311357436",
                SoLuong = dataCount,
                NgayLap = DateTime.Now.ToString("yyyy-MM-dd"),
                ThoiDiemLap = DateTime.Now.ToString("HH:mm:ss"),
            };
        }
    }
}
