using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml;
using System.Reflection;
using System.Xml.Schema;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using EIMS.Application.DTOs;

namespace EIMS.Application.Commons
{
    public static class XmlHelpers
    {
        private static XmlSchemaSet _schemaSet;
        // Tiền tố cố định cho MTDiep của CQT (Ví dụ: TCT + UUID)
        const string CqtMTDiepPrefix = "TCT";
        // Tiền tố cho MTDiep của Người nộp thuế/TC truyền nhận (Ví dụ: V + MST + UUID)
        const string NntMTDiepPrefix = "K";
        const int MtDiepLength = 46;
        public const decimal RATE_KHAC = -1;
        public const decimal RATE_KCT = -2;
        public const decimal RATE_KKNT = -3;
        public static string Serialize<T>(T obj, bool removeDeclaration = false)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            var xmlSerializer = new XmlSerializer(typeof(T));

            var settings = new XmlWriterSettings
            {
                Encoding = new UTF8Encoding(false),
                Indent = true,
                OmitXmlDeclaration = removeDeclaration
            };

            using var stringWriter = new Utf8StringWriter();
            using var xmlWriter = XmlWriter.Create(stringWriter, settings);

            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");  // remove xmlns:xsd, xmlns:xsi

            xmlSerializer.Serialize(xmlWriter, obj, ns);

            return stringWriter.ToString();
        }
        /// <summary>
        /// Chuyển đổi chuỗi XML thành Object (Deserialize)
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu muốn chuyển đổi (VD: TDiepTB04)</typeparam>
        /// <param name="xml">Chuỗi XML đầu vào</param>
        /// <returns>Object chứa dữ liệu</returns>
        public static T Deserialize<T>(string xml) where T : class
        {
            if (string.IsNullOrWhiteSpace(xml))
            {
                return null;
            }

            try
            {
                var serializer = new XmlSerializer(typeof(T));

                using (var reader = new StringReader(xml))
                {
                    // Hàm Deserialize trả về object, cần ép kiểu về T
                    return (T)serializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                // Ném lỗi ra ngoài để Handler biết mà xử lý (ví dụ: XML sai định dạng)
                throw new InvalidOperationException($"Lỗi khi Deserialize XML sang {typeof(T).Name}: {ex.Message}", ex);
            }
        }
        private static void LoadSchemas()
        {
            if (_schemaSet != null) return;

            _schemaSet = new XmlSchemaSet();
            var assembly = Assembly.GetExecutingAssembly();
            string[] names = assembly.GetManifestResourceNames();
            foreach (var name in names)
            {
                Console.WriteLine(name);
            }
            string[] resourceNames = {
            "EIMS.Application.Commons.XSDFiles.TDiep.xsd",
            "EIMS.Application.Commons.XSDFiles.HDon.xsd"
        };

            using (Stream stream = assembly.GetManifestResourceStream("EIMS.Application.Commons.XSDFiles.TDiep.xsd"))
            {
                // Thêm TDiep.xsd với targetNamespace của nó
                _schemaSet.Add("http://tempuri.org/TDiepSchema.xsd", XmlReader.Create(stream));
            }

            // Tải HDon.xsd
            using (Stream stream = assembly.GetManifestResourceStream("EIMS.Application.Commons.XSDFiles.HDon.xsd"))
            {
                // !!! Thêm HDon.xsd với targetNamespace của nó !!!
                _schemaSet.Add("http://tempuri.org/HDonSchema.xsd", XmlReader.Create(stream));
            }
            _schemaSet.Compile();
        }

        // Hàm Validation chính
        public static List<string> Validate(string xmlPayload)
        {
            LoadSchemas();
            var errors = new List<string>();
            var settings = new XmlReaderSettings
            {
                ValidationType = ValidationType.Schema,
                Schemas = _schemaSet
            };

            // Thêm event handler để bắt lỗi validation
            settings.ValidationEventHandler += (sender, args) =>
            {
                errors.Add($"[Dòng {args.Exception.LineNumber}, Cột {args.Exception.LinePosition}] {args.Message}");
            };

            // Đọc XML và thực hiện validation
            using (var reader = XmlReader.Create(new StringReader(xmlPayload), settings))
            {
                try
                {
                    // Đọc toàn bộ tài liệu để kích hoạt validation
                    while (reader.Read()) { }
                }
                catch (XmlException ex)
                {
                    errors.Add($"Lỗi cú pháp XML: {ex.Message}");
                }
            }

            return errors;
        }
        /// <summary>
        /// Tạo Mã Thông Điệp (MTDiep) duy nhất, tối đa 46 ký tự.
        /// </summary>
        public static string GenerateMTDiep(string prefix, string? idPart = null)
        {
            // UUID V4 32 ký tự in hoa, không dấu '-'
            string uuidPart = Guid.NewGuid().ToString("N").ToUpper();
            string idString = idPart ?? ""; // Nếu không có ID Part, dùng chuỗi rỗng

            // Ghép: Tiền tố + ID Part + UUID (đảm bảo độ dài tối đa 46)
            string mtDiep = prefix + idString + uuidPart;

            // Cắt chuỗi để đảm bảo đúng 46 ký tự (hoặc ít hơn nếu không đủ)
            return mtDiep.Length > MtDiepLength ? mtDiep.Substring(0, MtDiepLength) : mtDiep;
        }
        public static InvoiceSigningResult SignInvoiceXml(string rawInvoiceXml, X509Certificate2 signingCert)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.PreserveWhitespace = true;
            xmlDoc.LoadXml(rawInvoiceXml);

            // --- BƯỚC 1: XÁC ĐỊNH ID CHO SIGNATURE ---
            // ID này dùng để liên kết Signature và SignatureProperty
            string signatureId = "NBan"; // Có thể dùng "Signature-NBan" hoặc "NBan" tùy mẫu

            var signedXml = new SignedXml(xmlDoc);
            signedXml.SigningKey = signingCert.GetRSAPrivateKey();

            // --- BƯỚC 2: GÁN ID CHO SIGNATURE ---
            signedXml.Signature.Id = signatureId;

            // --- BƯỚC 3: CẤU HÌNH KEYINFO (Thêm SubjectName) ---
            var keyInfo = new KeyInfo();
            // Thêm SubjectName (Tên chủ thể)
            var keyInfoData = new KeyInfoX509Data(signingCert);
            keyInfoData.AddSubjectName(signingCert.SubjectName.Name); // Thêm dòng này
            keyInfo.AddClause(keyInfoData);
            signedXml.KeyInfo = keyInfo;

            // Reference (Giữ nguyên Enveloped Signature)
            var reference = new Reference { Uri = "" };
            reference.AddTransform(new XmlDsigEnvelopedSignatureTransform());
            reference.AddTransform(new XmlDsigC14NTransform());
            signedXml.AddReference(reference);

            // --- BƯỚC 4: SỬA SIGNATURE PROPERTY (Target trỏ về SignatureId) ---
            var signatureProperty = xmlDoc.CreateElement("SignatureProperty");
            // QUAN TRỌNG: Target phải có dấu '#' + SignatureId
            signatureProperty.SetAttribute("Target", "#" + signatureId);
            // Có thể thêm Id cho chính Property này nếu cần (như mẫu eInvoice: Id="proid")
            // signatureProperty.SetAttribute("Id", "proid"); 

            var signingTimeElement = xmlDoc.CreateElement("SigningTime");
            signingTimeElement.InnerText = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
            signatureProperty.AppendChild(signingTimeElement);

            var signatureProperties = xmlDoc.CreateElement("SignatureProperties");
            signatureProperties.AppendChild(signatureProperty);
            var xmlObject = xmlDoc.CreateElement("Object");
            xmlObject.AppendChild(signatureProperties);
            signedXml.AddObject(new DataObject { Data = xmlObject.ChildNodes });

            // 5. Ký
            signedXml.ComputeSignature();
            byte[] signatureBytes = signedXml.Signature.SignatureValue;
            string signatureBase64 = Convert.ToBase64String(signatureBytes);

            // --- BƯỚC 5: CHÈN VÀO ĐÚNG CẤU TRÚC <DSCKS><NBan> ---
            var signatureElement = signedXml.GetXml();

            // Tạo thẻ bao DSCKS
            var dscksNode = xmlDoc.CreateElement("DSCKS");

            // Tạo thẻ bao NBan (Người bán) bên trong DSCKS
            var nbanNode = xmlDoc.CreateElement("NBan");

            // Nhét Signature vào NBan
            nbanNode.AppendChild(xmlDoc.ImportNode(signatureElement, true));

            // Nhét NBan vào DSCKS
            dscksNode.AppendChild(nbanNode);

            // (Tùy chọn: Thêm các thẻ rỗng khác như NMua, CCKSKhac nếu mẫu yêu cầu)
            // var nmuaNode = xmlDoc.CreateElement("NMua"); dscksNode.AppendChild(nmuaNode);
            // var cqtNode = xmlDoc.CreateElement("CQT"); dscksNode.AppendChild(cqtNode);

            // Chèn DSCKS vào cuối HDon
            xmlDoc.DocumentElement.AppendChild(dscksNode);

            return new InvoiceSigningResult
            {
                SignedXml = xmlDoc.OuterXml,
                SignatureValue = signatureBase64
            };
        }
        /// <summary>
        /// Ký số cho Thông báo sai sót (Mẫu 04/SS-HĐĐT)
        /// Cấu trúc: TDiep -> DLieu -> TBao -> DSCKS -> Signature
        /// </summary>
        public static InvoiceSigningResult SignTB04Xml(string rawXml, X509Certificate2 signingCert)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.PreserveWhitespace = true;
            xmlDoc.LoadXml(rawXml);

            var signedXml = new SignedXml(xmlDoc);
            signedXml.SigningKey = signingCert.GetRSAPrivateKey();

            var keyInfo = new KeyInfo();
            keyInfo.AddClause(new KeyInfoX509Data(signingCert));
            signedXml.KeyInfo = keyInfo;

            var reference = new Reference { Uri = "" };
            reference.AddTransform(new XmlDsigEnvelopedSignatureTransform());
            reference.AddTransform(new XmlDsigC14NTransform());
            signedXml.AddReference(reference);

            // Thêm Metadata (SigningTime)
            var signatureProperty = xmlDoc.CreateElement("SignatureProperty");
            signatureProperty.SetAttribute("Target", "");
            var signingTimeElement = xmlDoc.CreateElement("SigningTime");
            signingTimeElement.InnerText = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
            signatureProperty.AppendChild(signingTimeElement);

            var signatureProperties = xmlDoc.CreateElement("SignatureProperties");
            signatureProperties.AppendChild(signatureProperty);
            var xmlObject = xmlDoc.CreateElement("Object");
            xmlObject.AppendChild(signatureProperties);
            signedXml.AddObject(new DataObject { Data = xmlObject.ChildNodes });

            // Compute Signature
            signedXml.ComputeSignature();

            byte[] signatureBytes = signedXml.Signature.SignatureValue;
            string signatureBase64 = Convert.ToBase64String(signatureBytes);

            // --- ĐIỂM KHÁC BIỆT: VỊ TRÍ CHÈN CHỮ KÝ ---
            var signatureElement = signedXml.GetXml();

            // Tìm thẻ TBao (thay vì HDon)
            var tBaoNode = xmlDoc.SelectSingleNode("//*[local-name()='TBao']");

            if (tBaoNode != null)
            {
                // Tạo thẻ DSCKS bên trong TBao
                var dscksNode = xmlDoc.CreateElement("DSCKS");
                dscksNode.AppendChild(xmlDoc.ImportNode(signatureElement, true));
                tBaoNode.AppendChild(dscksNode);
            }
            else
            {
                throw new Exception("Không tìm thấy thẻ <TBao> trong XML để chèn chữ ký.");
            }
            // -------------------------------------------

            return new InvoiceSigningResult
            {
                SignedXml = xmlDoc.OuterXml,
                SignatureValue = signatureBase64
            };
        }
        public static int MapApiCodeToStatusId(string apiCode)
        {
            return apiCode?.ToUpper() switch
            {
                // === Nhóm Logic ===
                "PENDING" => 1,
                "RECEIVED" => 2,
                "REJECTED" => 3,
                "APPROVED" => 4,
                "FAILED" => 5,
                "PROCESSING" => 6,
                "NOT_FOUND" => 7,

                // === Nhóm TBxx (Tiếp nhận/Lỗi validation) ===
                "TB01" => 10, // Tiếp nhận hợp lệ
                "TB02" => 11, // Sai format XML
                "TB03" => 12, // Sai chữ ký số
                "TB04" => 13, // Sai MST
                "TB05" => 14, // Thiếu thông tin
                "TB06" => 15, // Sai dữ liệu
                "TB07" => 16, // Trùng hóa đơn
                "TB08" => 17, // Không được cấp mã
                "TB09" => 18, // Không tìm thấy HĐ gốc
                "TB10" => 19, // Hàng hóa sai
                "TB11" => 20, // PDF lỗi
                "TB12" => 21, // Lỗi kỹ thuật CQT

                // === Nhóm KQxx (Kết quả xử lý) ===
                "KQ01" => 30, // Đã cấp mã
                "KQ02" => 31, // Từ chối cấp mã
                "KQ03" => 32, // Chưa có KQ
                "KQ04" => 33, // Không tìm thấy HĐ

                // Mặc định nếu thành công nhưng không rõ mã
                _ => 2 // RECEIVED (CQT đã tiếp nhận)
            };
        }
        /// <summary>
        /// Chuyển đổi VAT Rate sang chuỗi XML.
        /// </summary>
        /// <param name="vatRate">Giá trị rate</param>
        /// <param name="appendPercentSymbol">
        /// TRUE: Gửi "8%" (Giống XML bạn cung cấp).
        /// FALSE: Gửi "8" (Chuẩn an toàn nhất).
        /// </param>
        public static string ToXmlValue(decimal vatRate, bool appendPercentSymbol = false)
        {
            string value = vatRate switch
            {
                RATE_KHAC => "KHAC",
                RATE_KCT => "KCT",
                RATE_KKNT => "KKNT",
                _ => vatRate.ToString("G29") // G29 để bỏ số 0 thừa (8.00 -> 8)
            };
            if (appendPercentSymbol && IsNumericRate(vatRate))
            {
                return value + "%";
            }

            return value;
        }

        private static bool IsNumericRate(decimal rate)
        {
            // Kiểm tra xem có phải là các mã đặc biệt không
            return rate != RATE_KHAC && rate != RATE_KCT && rate != RATE_KKNT;
        }
    }

    public class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding => new UTF8Encoding(false);
    }
}
