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
using System.Security.Cryptography;
using System.Collections.Concurrent;
namespace EIMS.Application.Commons.Helpers
{
    public static class XmlHelpers
    {
        private static readonly ConcurrentDictionary<string, XmlSerializer> _serializerCache = new();
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

            // 1. Tự động tìm Namespace được khai báo trên class DTO
            // Ví dụ: [XmlRoot(Namespace = "http://tempuri.org/TDiepSchema.xsd")]
            var xmlRootAttr = typeof(T).GetCustomAttribute<XmlRootAttribute>();

            if (xmlRootAttr != null && !string.IsNullOrEmpty(xmlRootAttr.Namespace))
            {
                // Nếu class có khai báo Namespace, thêm nó vào làm default namespace
                // Key rỗng "" nghĩa là: xmlns="http://..."
                ns.Add("", xmlRootAttr.Namespace);
            }
            else
            {
                // Nếu không có khai báo, giữ nguyên logic cũ (xóa namespace mặc định xsd, xsi cho gọn)
                ns.Add("", "");
            }
            // ---------------------------------

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
        public static T DeserializeFlexible<T>(string xmlContent)
        {
            if (string.IsNullOrWhiteSpace(xmlContent)) return default;

            // 1. Tạo Cache Key mới (Thêm _Override để tránh dùng lại cái cũ bị lỗi)
            string cacheKey = typeof(T).FullName + "_Override_NoNs";

            // 2. Lấy hoặc Tạo Serializer cấu hình đặc biệt
            var serializer = _serializerCache.GetOrAdd(cacheKey, key =>
            {
                // A. Tìm tên Element gốc (VD: "HDon" hoặc "TDiep") từ Class T
                var rootAttr = typeof(T).GetCustomAttribute<XmlRootAttribute>();
                string rootName = rootAttr?.ElementName ?? typeof(T).Name;

                // B. Tạo cấu hình GHI ĐÈ (Override)
                var overrides = new XmlAttributeOverrides();
                var attrs = new XmlAttributes();

                // C. Ép Serializer: "Root của class này tên là 'rootName', và Namespace là RỖNG"
                attrs.XmlRoot = new XmlRootAttribute
                {
                    ElementName = rootName,
                    Namespace = "" // <--- QUAN TRỌNG NHẤT: Ép về rỗng
                };

                overrides.Add(typeof(T), attrs);

                // D. Tạo Serializer với cấu hình đè này
                return new XmlSerializer(typeof(T), overrides);
            });

            // 3. Đọc XML và lột bỏ Namespace
            using (var stringReader = new StringReader(xmlContent))
            using (var reader = new NamespaceIgnorantXmlTextReader(stringReader))
            {
                return (T)serializer.Deserialize(reader);
            }
        }
        private static void LoadSchemas()
        {
            if (_schemaSet != null) return;

            _schemaSet = new XmlSchemaSet();
            var assembly = typeof(XmlHelpers).Assembly;
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
            try
            {
                LoadSchemas();
            }
            catch (Exception ex)
            {
                return new List<string> { $"Lỗi cấu hình hệ thống (Load Schema): {ex.Message}" };
            }
            var errors = new List<string>();
            var settings = new XmlReaderSettings
            {
                ValidationType = ValidationType.Schema,
                Schemas = _schemaSet,
                ValidationFlags = XmlSchemaValidationFlags.ReportValidationWarnings | XmlSchemaValidationFlags.ProcessIdentityConstraints
            };

            // Thêm event handler để bắt lỗi validation
            settings.ValidationEventHandler += (sender, args) =>
            {
                string severity = args.Severity == XmlSeverityType.Warning ? "Cảnh báo" : "Lỗi";
                errors.Add($"[{severity} - Dòng {args.Exception.LineNumber}, Cột {args.Exception.LinePosition}] {args.Message}");
            };

            // Đọc XML và thực hiện validation
            using (var reader = XmlReader.Create(new StringReader(xmlPayload), settings))
            {
                try
                {
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
            string uuidPart = Guid.NewGuid().ToString("N").ToUpper();
            string idString = idPart ?? "";
            string mtDiep = prefix + idString + uuidPart;
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
        /// Ký số bằng USB
        /// </summary>
        public static XmlDocument EmbedSignatureToXml(XmlDocument xmlDoc, string signatureBase64, string certificateBase64)
        {
            // 1. TÌM HOẶC TẠO THẺ BAO (Khác code của bạn một chút để an toàn hơn)
            XmlElement dscksNode = (XmlElement)xmlDoc.GetElementsByTagName("DSCKS")[0];
            if (dscksNode == null)
            {
                dscksNode = xmlDoc.CreateElement("DSCKS");
                xmlDoc.DocumentElement.AppendChild(dscksNode);
            }

            XmlElement nbanNode = (XmlElement)dscksNode.GetElementsByTagName("NBan")[0];
            if (nbanNode == null)
            {
                nbanNode = xmlDoc.CreateElement("NBan");
                dscksNode.AppendChild(nbanNode);
            }
            else
            {
                nbanNode.RemoveAll();
            }

            // 2. KHỞI TẠO SIGNED XML
            var signedXml = new SignedXml(xmlDoc);
            // --- ID ---
            string signatureId = "NBan";
            signedXml.Signature.Id = signatureId;

            // --- KEY INFO ---
            var keyInfo = new KeyInfo();
            var cert = new X509Certificate2(Convert.FromBase64String(certificateBase64));
            var keyInfoData = new KeyInfoX509Data(cert);
            keyInfoData.AddSubjectName(cert.SubjectName.Name); // Giống code bạn
            keyInfo.AddClause(keyInfoData);
            signedXml.KeyInfo = keyInfo;

            // --- REFERENCE ---
            var reference = new Reference { Uri = "" };
            reference.AddTransform(new XmlDsigEnvelopedSignatureTransform());
            reference.AddTransform(new XmlDsigC14NTransform()); // Giống code bạn
            signedXml.AddReference(reference);

            // --- OBJECT (SigningTime) ---
            // Sửa lại đoạn này để giống code bạn nhưng xử lý Namespace chuẩn
            var signingTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");

            // Tạo Object
            var dataObject = new DataObject();
            var signatureProperty = xmlDoc.CreateElement("SignatureProperty", SignedXml.XmlDsigNamespaceUrl);
            signatureProperty.SetAttribute("Target", "#" + signatureId);

            var signingTimeElement = xmlDoc.CreateElement("SigningTime", SignedXml.XmlDsigNamespaceUrl);
            signingTimeElement.InnerText = signingTime;
            signatureProperty.AppendChild(signingTimeElement);

            var signatureProperties = xmlDoc.CreateElement("SignatureProperties", SignedXml.XmlDsigNamespaceUrl);
            signatureProperties.AppendChild(signatureProperty);

            // Gán vào DataObject
            dataObject.Data = signatureProperties.SelectNodes(".");
            signedXml.AddObject(dataObject);

            // 3. TÍNH TOÁN CẤU TRÚC (Dùng Key giả để tạo khung)
            using (RSA rsa = RSA.Create())
            {
                signedXml.SigningKey = rsa;
                signedXml.SignedInfo.SignatureMethod = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256";
                signedXml.ComputeSignature();
            }

            // 4. LẤY XML VÀ GHI ĐÈ CHỮ KÝ THẬT
            XmlElement signatureElement = signedXml.GetXml();

            // Tìm thẻ SignatureValue và ghi đè giá trị từ USB Token
            var signatureValueNode = signatureElement.GetElementsByTagName("SignatureValue")[0];
            signatureValueNode.InnerText = signatureBase64;

            // 5. CHÈN VÀO NBAN (Đã tìm thấy hoặc tạo ở bước 1)
            nbanNode.AppendChild(xmlDoc.ImportNode(signatureElement, true));

            return xmlDoc;
        }
        /// <summary>
        /// Kiểm tra tính toàn vẹn của chữ ký số trong file XML
        /// </summary>
        public static bool ValidateXmlSignature(XmlDocument xmlDoc)
        {
            // QUAN TRỌNG: XmlDocument phải được load với PreserveWhitespace = true trước khi gọi hàm này
            // Nếu không, hash sẽ bị sai lệch.

            // 1. Tạo đối tượng SignedXml
            SignedXml signedXml = new SignedXml(xmlDoc);

            // 2. Tìm thẻ <Signature>
            // Namespace chuẩn của W3C XML Digital Signature
            var nodeList = xmlDoc.GetElementsByTagName("Signature", "http://www.w3.org/2000/09/xmldsig#");

            // Nếu không tìm thấy chữ ký nào -> False
            if (nodeList.Count == 0) return false;

            // 3. Duyệt qua các chữ ký (Thường hóa đơn chỉ có 1 hoặc 2 chữ ký)
            // Trong ngữ cảnh API "CompleteSigning", ta thường chỉ quan tâm chữ ký vừa thêm vào.
            // Ở đây tôi viết code để validate chữ ký ĐẦU TIÊN tìm thấy (thường là Người bán).

            // Load thẻ Signature vào đối tượng SignedXml
            signedXml.LoadXml((XmlElement)nodeList[0]);

            // 4. Kiểm tra (Verify)
            // Hàm CheckSignature() sẽ làm 2 việc:
            // - Lấy Public Key từ thẻ <KeyInfo> trong XML.
            // - Tính toán lại Hash của XML và so sánh với Hash trong chữ ký.
            // - Decrypt chữ ký bằng Public Key xem có khớp không.
            return signedXml.CheckSignature();
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
            string signatureId = "Signature_" + Guid.NewGuid().ToString("N");
            signedXml.Signature.Id = signatureId;
            var keyInfo = new KeyInfo();
            keyInfo.AddClause(new KeyInfoX509Data(signingCert));
            signedXml.KeyInfo = keyInfo;

            var reference = new Reference { Uri = "" };
            reference.AddTransform(new XmlDsigEnvelopedSignatureTransform());
            reference.AddTransform(new XmlDsigC14NTransform());
            signedXml.AddReference(reference);

            // Thêm Metadata (SigningTime)
            var dataObject = new DataObject();

            // Namespace chuẩn của XMLDSig
            string dsigNs = SignedXml.XmlDsigNamespaceUrl; // "http://www.w3.org/2000/09/xmldsig#"

            // Tạo SignatureProperties có Namespace
            var signatureProperties = xmlDoc.CreateElement("SignatureProperties", dsigNs);

            // Tạo SignatureProperty có Namespace
            var signatureProperty = xmlDoc.CreateElement("SignatureProperty", dsigNs);
            // Target phải trỏ về ID của chữ ký
            signatureProperty.SetAttribute("Target", "#" + signatureId);

            // Tạo SigningTime có Namespace
            var signingTime = xmlDoc.CreateElement("SigningTime", dsigNs);
            signingTime.InnerText = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");

            // Ghép các thẻ lại
            signatureProperty.AppendChild(signingTime);
            signatureProperties.AppendChild(signatureProperty);

            // Đưa vào DataObject
            dataObject.Data = signatureProperties.SelectNodes(".");
            signedXml.AddObject(dataObject);

            // Compute Signature
            signedXml.ComputeSignature();

            byte[] signatureBytes = signedXml.Signature.SignatureValue;
            string signatureBase64 = Convert.ToBase64String(signatureBytes);
            var signatureElement = signedXml.GetXml();
            var tBaoNode = xmlDoc.SelectSingleNode("//*[local-name()='TBao']");

            if (tBaoNode != null)
            {
                var dscksNode = xmlDoc.CreateElement("DSCKS", tBaoNode.NamespaceURI);
                dscksNode.AppendChild(xmlDoc.ImportNode(signatureElement, true));
                tBaoNode.AppendChild(dscksNode);
            }
            else
            {
                throw new Exception("Không tìm thấy thẻ <TBao> trong XML để chèn chữ ký.");
            }
            return new InvoiceSigningResult
            {
                SignedXml = xmlDoc.OuterXml,
                SignatureValue = signatureBase64
            };
        }
        public static string CreateDigest(string xmlContent)
        {
            var doc = new XmlDocument();
            doc.PreserveWhitespace = true;
            doc.LoadXml(xmlContent);
            var signedXml = new SignedXml(doc);
            var reference = new Reference { Uri = "" };
            reference.AddTransform(new XmlDsigEnvelopedSignatureTransform());
            signedXml.AddReference(reference);
            // Để lấy được DigestValue chính xác, ta cần "giả vờ" ký để .NET chạy qua quy trình Canonicalization
            // Ta dùng một key tạm (hoặc không cần key nếu chỉ lấy Digest của Reference)
            // Tuy nhiên, cách thủ công chuẩn nhất là Canonicalize XML rồi Hash SHA256
            // CÁCH ĐƠN GIẢN: Lấy nội dung XML đã chuẩn hóa
            Transform transform = new XmlDsigEnvelopedSignatureTransform();
            transform.LoadInput(doc);
            var output = (Stream)transform.GetOutput(typeof(Stream));

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hash = sha256.ComputeHash(output);
                return Convert.ToBase64String(hash);
            }
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
        /// <summary>
        /// Tính chuỗi Hash (Digest Value) chuẩn SHA256 của XML để gửi xuống Client ký
        /// </summary>
        public static string CalculateDigest(XmlDocument xmlDoc)
        {
            // 1. Chuẩn hóa XML (Canonicalization - C14N)
            // Bước này cực kỳ quan trọng để đảm bảo XML ở Server và Client giống hệt nhau từng byte
            // Client (Plugin ký số) thường sẽ tự động C14N trước khi Hash, nên Server cũng phải làm vậy.

            var transform = new XmlDsigC14NTransform();
            transform.LoadInput(xmlDoc);

            // 2. Lấy luồng dữ liệu sau khi chuẩn hóa
            // Lưu ý: Dùng HashAlgorithm SHA256 vì các thiết bị Token hiện nay đều dùng chuẩn này
            using (var stream = (Stream)transform.GetOutput(typeof(Stream)))
            using (var sha256 = SHA256.Create())
            {
                // 3. Tính Hash
                byte[] hashBytes = sha256.ComputeHash(stream);

                // 4. Trả về Base64
                return Convert.ToBase64String(hashBytes);
            }
        }
        private static bool IsNumericRate(decimal rate)
        {
            // Kiểm tra xem có phải là các mã đặc biệt không
            return rate != RATE_KHAC && rate != RATE_KCT && rate != RATE_KKNT;
        }
        private class NamespaceIgnorantXmlTextReader : XmlTextReader
        {
            public NamespaceIgnorantXmlTextReader(TextReader reader) : base(reader) { }
            public override string NamespaceURI => "";
        }
    }

    public class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding => new UTF8Encoding(false);
    }
}
