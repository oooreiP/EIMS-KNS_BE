using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.XMLModels;
using EIMS.Application.DTOs.XMLModels.TB01;
using EIMS.Application.DTOs.XMLModels.TB04;
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
        private readonly IInvoiceXMLService _invoiceXMLService;

        public InvoiceXmlMapper(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public static HDon MapInvoiceToXmlModel(Invoice invoice)
        {
            if (invoice == null)
                throw new ArgumentNullException(nameof(invoice));
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
                                KHMSHDon = khmsHDon,                       // Ký hiệu mẫu số hóa đơn 
                                KHHDon = khHDon,                      // Ký hiệu hóa đơn
                                SHDon = invoice.InvoiceNumber.HasValue
                                ? invoice.InvoiceNumber.Value.ToString("0000000")
                                : "",
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
                                    Ten = "CÔNG TY CỔ PHẦN GIẢI PHÁP TỔNG THỂ KỶ NGUYÊN SỐ",
                                    MST = "0311357436",
                                    DChi = "26 Nguyễn Đình Khơi, Phường Tân Sơn Nhất, TP Hồ Chí Minh, Việt Nam",
                                    DCTDTu = "support@einvoice.vn",
                                    SDThoai = "0382502857",
                                    TNHang = "Ngân Hàng TMCP Ngoại Thương Việt Nam - Chi Nhánh Tân Bình",
                                    STKNHang = "0441000627320"
                                },
                                // ---- Người mua ----
                                NMua = new Party
                                {
                                    Ten = invoice.InvoiceCustomerName ?? invoice.Customer.CustomerName,
                                    MST = invoice.InvoiceCustomerTaxCode ?? invoice.Customer.TaxCode ?? "",
                                    DChi = invoice.InvoiceCustomerAddress ?? invoice.Customer.Address,
                                    DCTDTu = invoice.Customer.ContactEmail,
                                    SDThoai = invoice.Customer.ContactPhone ?? "",
                                    TNHang = "",
                                    STKNHang = ""
                                },
                                // ---- Danh sách hàng hóa ----
                                DSHHDVu = invoice.InvoiceItems.Select((item, index) => new HHDVu
                                {
                                    TChat = khmsHDon,
                                    STT = index + 1,
                                    THHDVu = item.Product?.Name ?? "Sản phẩm",
                                    DVTinh = item.Product?.Unit ?? "cái",
                                    SLuong = item.Quantity,
                                    DGia = item.UnitPrice,
                                    ThTien = item.Amount,
                                    TSuat = XmlHelpers.ToXmlValue(
                                    item.Product?.VATRate ?? invoice.VATRate,
                                    appendPercentSymbol: false),
                                }).ToList(),
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
                        new TTin { TTruong = "NgayKy", KDLieu = "date", DLieu = invoice.SignDate?.ToString("yyyy-MM-dd") ?? "01/01/2025" }
                    }
                            }
                        },
                        MCCQT = new MCCQT
                        {
                            Value = String.Empty                
                        }
            };
            if ((invoice.InvoiceType == 2 || invoice.InvoiceType == 3) && invoice.OriginalInvoice != null)
            {
                var orgInv = invoice.OriginalInvoice;
                string orgKhms = khmsHDon;
                string orgKh = khHDon;
                if (orgInv.Template?.Serial != null)
                {
                    var s = orgInv.Template.Serial;
                    orgKhms = s.Prefix.PrefixID.ToString();
                    orgKh = $"{s.SerialStatus.Symbol}{s.Year}{s.Tail}{s.InvoiceType.Symbol}";
                }

                model.DLHDon.TTChung.TTHDLQuan = new TTHDLQuan
                {
                    TCHDon = (invoice.InvoiceType == 3) ? 1 : 2,
                    LHDCLQuan = 1, // 1: Hóa đơn điện tử
                    KHMSHDCLQuan = orgKhms, // Ký hiệu mẫu số gốc
                    KHHDCLQuan = orgKh,     // Ký hiệu hóa đơn gốc
                    SHDCLQuan = orgInv.InvoiceNumber.HasValue ? 
                                orgInv.InvoiceNumber.Value.ToString("D7") : "",
                    NLHDCLQuan = (orgInv.IssuedDate ?? orgInv.CreatedAt).ToString("yyyy-MM-dd"),

                    Gchu = invoice.AdjustmentReason ?? invoice.Notes 
                };
            }
            return model;
        }
        public static TDiep MapThongDiepToXmlModel(Invoice invoice, TaxMessageCode messageCode, int dataCount, string? referenceMessageId = null)
        {
            var tTinChung =  GenerateTTChung(messageCode, dataCount, referenceMessageId);
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
        public static  TtinChung GenerateTTChung(
                TaxMessageCode messageCode,            
                int dataCount,
                string? referenceMessageId = null)
        {
            string providerCode = "K0311357436"; 
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
        /// <summary>
        /// Chuyển đổi từ Thông điệp 300 (TB04 - NNT gửi) sang Thông điệp 301 (TB01 - CQT phản hồi)
        /// </summary>
        /// <param name="requestMsg">Object TB04 đã deserialize từ XML gửi lên</param>
        /// <returns>Object TB01 sẵn sàng để serialize trả về</returns>
        public static TDiepTB01 CreateResponse301FromRequest300(TDiepTB04 requestMsg)
        {
            // 1. Lấy các thông tin cốt lõi từ Request
            var reqTTChung = requestMsg.TTChung;
            var reqDLTBao = requestMsg.DLieu.TBao.DLTBao;
            string mtDiepPhanHoi = "TCT" + Guid.NewGuid().ToString("N").ToUpper();
            string mtDiepGoc = reqTTChung.MaThongDiep;
            string soThongBaoCqt = $"TB/SS/{DateTime.Now.Year}/{new Random().Next(10000, 99999)}";

            // 3. Khởi tạo Object Phản hồi (TB01)
            var response = new TDiepTB01
            {
                TTChung = new TtinChung
                {
                    PhienBan = "2.1.0",
                    MaNguoiGui = "TCT", 
                    MaNguoiNhan = reqDLTBao.MST,
                    MaLoaiThongDiep = "301",
                    MaThongDiep = mtDiepPhanHoi,
                    MaThongDiepDoiChieu = mtDiepGoc, 
                    NgayLap = DateTime.Now.ToString("yyyy-MM-dd"),
                    ThoiDiemLap = DateTime.Now.ToString("HH:mm:ss")
                },
                DLieu = new DLieuTB01
                {
                    TBao = new TBao01
                    {
                        DLTBao = new DLTBao01
                        {
                            PBan = "2.1.0",
                            MSo = "01/TB-SSĐT",
                            Ten = "Thông báo về việc tiếp nhận và kết quả xử lý về việc hóa đơn điện tử đã lập có sai sót",
                            DDanh = reqDLTBao.DDanh, // Lấy địa danh của DN
                            TNNT = "CÔNG TY ...",
                            MST = reqDLTBao.MST,     
                            TCQTCTren = "Tổng cục Thuế",
                            TCQT = "Cục Thuế quản lý",
                            MGDDTu = Guid.NewGuid().ToString().ToUpper(), 
                            TGNhan = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                            NTBNNT = reqDLTBao.NTBao, 
                            STTThe = 1,
                            HThuc = "Chữ ký số",
                            CDanh = "Thủ trưởng cơ quan thuế",
                            DSHDon = MapInvoiceListFromRequest(reqDLTBao.DSHDon)
                        },

                        STBao = new STBao
                        {
                            So = soThongBaoCqt,
                            NTBao = DateTime.Now.ToString("yyyy-MM-dd")
                        },

                        DSCKS = new DSCKS_CQT
                        {
                            CQT = new SignatureWrapper
                            {
                                SignedInfo = new SignedInfo
                                {
                                    CanonicalizationMethod = new AlgorithmMethod { Algorithm = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315" },
                                    SignatureMethod = new AlgorithmMethod { Algorithm = "http://www.w3.org/2000/09/xmldsig#rsa-sha1" },
                                    Reference = new Reference
                                    {
                                        URI = "",
                                        DigestValue = "MOCK_HASH_VALUE_BASE64..."
                                    }
                                },
                                SignatureValue = "MOCK_SIGNATURE_VALUE_BASE64_VERY_LONG_STRING...",
                                KeyInfo = new KeyInfo
                                {
                                    X509Data = new X509Data { X509Certificate = "MOCK_PUBLIC_KEY_CERTIFICATE..." }
                                },
                                Object = new List<SignatureObject>
                                {
                                    new SignatureObject
                                    {
                                        SignatureProperties = new SignatureProperties
                                        {
                                            SignatureProperty = new SignatureProperty
                                            {
                                                Target = "",
                                                SigningTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            return response;
        }
        public static TDiepTB01 CreateRejectResponse301(TDiepTB04 requestMsg, string errorCode, string errorDesc)
        {
            var reqTTChung = requestMsg.TTChung;
            var reqDLTBao = requestMsg.DLieu.TBao.DLTBao;
            string mtDiepPhanHoi = "TCT" + Guid.NewGuid().ToString("N").ToUpper();
            string mtDiepGoc = reqTTChung.MaThongDiep;
            string soThongBaoCqt = $"TB/SS/{DateTime.Now.Year}/{new Random().Next(10000, 99999)}";
            var response = new TDiepTB01
            {
                TTChung = new TtinChung
                {
                    PhienBan = "2.1.0",
                    MaNguoiGui = "TCT",
                    MaNguoiNhan = reqDLTBao.MST,
                    MaLoaiThongDiep = "301",
                    MaThongDiep = mtDiepPhanHoi,
                    MaThongDiepDoiChieu = mtDiepGoc,
                    NgayLap = DateTime.Now.ToString("yyyy-MM-dd"),
                    ThoiDiemLap = DateTime.Now.ToString("HH:mm:ss")
                },
                DLieu = new DLieuTB01
                {
                    TBao = new TBao01
                    {
                        DLTBao = new DLTBao01
                        {
                            PBan = "2.1.0",
                            MSo = "01/TB-SSĐT",
                            Ten = "Thông báo về việc tiếp nhận và kết quả xử lý về việc hóa đơn điện tử đã lập có sai sót",
                            DDanh = reqDLTBao.DDanh, // Lấy địa danh của DN
                            TNNT = "CÔNG TY ...",
                            MST = reqDLTBao.MST,
                            TCQTCTren = "Tổng cục Thuế",
                            TCQT = "Cục Thuế quản lý",
                            MGDDTu = Guid.NewGuid().ToString().ToUpper(),
                            TGNhan = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                            NTBNNT = reqDLTBao.NTBao,
                            STTThe = 1,
                            HThuc = "Chữ ký số",
                            CDanh = "Thủ trưởng cơ quan thuế",
                            DSHDon = new DSHDonWrapper01
                            {
                                HDon = requestMsg.DLieu.TBao.DLTBao.DSHDon.HDon.Select(reqInv => new HDonTB01
                                {
                                    STT = reqInv.STT,
                                    MCQTCap = reqInv.MCCQT,
                                    KHMSHDon = reqInv.KHMSHDon,
                                    KHHDon = reqInv.KHHDon,
                                    SHDon = reqInv.SHDon,
                                    NLap = reqInv.Ngay,
                                    LADHDDT = reqInv.LADHDDT,
                                    TCTBao = reqInv.TCTBao,
                                    TTTNCCQT = 2,
                                    DSLDKTNhan = new DSLDKTNhan
                                    {
                                        LDo = new List<LDoError>
                                {
                                    new LDoError
                                    {
                                        MLoi = errorCode,
                                        MTLoi = errorDesc, // VD: "Mã CQT không tồn tại trong hệ thống"
                                        HDXLy = "Kiểm tra lại Mã CQT trên hóa đơn gốc"
                                    }
                                }
                                    }
                                }).ToList()
                            }
                        },
                        STBao = new STBao
                        {
                            So = soThongBaoCqt,
                            NTBao = DateTime.Now.ToString("yyyy-MM-dd")
                        },

                        DSCKS = new DSCKS_CQT
                        {
                            CQT = new SignatureWrapper
                            {
                                SignedInfo = new SignedInfo
                                {
                                    CanonicalizationMethod = new AlgorithmMethod { Algorithm = "http://www.w3.org/TR/2001/REC-xml-c14n-20010315" },
                                    SignatureMethod = new AlgorithmMethod { Algorithm = "http://www.w3.org/2000/09/xmldsig#rsa-sha1" },
                                    Reference = new Reference
                                    {
                                        URI = "",
                                        DigestValue = "MOCK_HASH_VALUE_BASE64..."
                                    }
                                },
                                SignatureValue = "MOCK_SIGNATURE_VALUE_BASE64_VERY_LONG_STRING...",
                                KeyInfo = new KeyInfo
                                {
                                    X509Data = new X509Data { X509Certificate = "MOCK_PUBLIC_KEY_CERTIFICATE..." }
                                },
                                Object = new List<SignatureObject>
                                {
                                    new SignatureObject
                                    {
                                        SignatureProperties = new SignatureProperties
                                        {
                                            SignatureProperty = new SignatureProperty
                                            {
                                                Target = "",
                                                SigningTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };
            return response;
        }
        private static DSHDonWrapper01 MapInvoiceListFromRequest(DSHDonWrapper requestList)
        {
            var resultList = new List<HDonTB01>();

            if (requestList?.HDon != null)
            {
                foreach (var reqInv in requestList.HDon)
                {
                    var respInv = new HDonTB01
                    {
                        STT = reqInv.STT,
                        MCQTCap = reqInv.MCCQT,     // Mã CQT gốc
                        KHMSHDon = reqInv.KHMSHDon, // Ký hiệu mẫu
                        KHHDon = reqInv.KHHDon,     // Ký hiệu
                        SHDon = reqInv.SHDon,       // Số hóa đơn
                        NLap = reqInv.Ngay,         // Ngày lập
                        LADHDDT = reqInv.LADHDDT,
                        TCTBao = reqInv.TCTBao,     // Tính chất (Hủy/ĐC...)
                        TTTNCCQT = 1
                    };

                    resultList.Add(respInv);
                }
            }

            return new DSHDonWrapper01 { HDon = resultList };
        }
    }
}
