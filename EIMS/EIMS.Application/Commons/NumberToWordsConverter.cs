using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Commons
{
    public static class NumberToWordsConverter
    {
        // Mảng chứa các từ cho các chữ số từ 0 đến 9
        private static readonly string[] ChuSo = { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };

        // Mảng chứa tên của các hàng (nghìn, triệu, tỷ)
        private static readonly string[] DonVi = { "", "nghìn", "triệu", "tỷ" };

        /// <summary>
        /// Chuyển số tiền thành chuỗi chữ tiếng Việt (hỗ trợ đến hàng tỷ).
        /// </summary>
        /// <param name="soTien">Số tiền nguyên cần chuyển.</param>
        /// <returns>Chuỗi đọc số tiền bằng chữ.</returns>
        public static string ChuyenSoThanhChu(decimal soTien)
        {
            if (soTien == 0)
            {
                return "không đồng";
            }

            // Đảm bảo số tiền là dương
            decimal soAm = soTien;
            if (soAm < 0)
            {
                soAm = -soAm;
            }

            string ketQua = "";
            int nhom = 0; // Nhóm hàng (0: đơn vị, 1: nghìn, 2: triệu, 3: tỷ)

            while (soAm > 0)
            {
                // Lấy 3 chữ số cuối cùng của số (nhóm hàng)
                int baChuSo = (int)(soAm % 1000);

                // Xử lý 3 chữ số này thành chữ
                string chuoiBaChuSo = DocBaChuSo(baChuSo);

                // Gán tên đơn vị hàng (nghìn, triệu, tỷ)
                if (!string.IsNullOrEmpty(chuoiBaChuSo))
                {
                    chuoiBaChuSo += " " + DonVi[nhom];
                }

                // Cộng vào kết quả chung
                ketQua = chuoiBaChuSo + " " + ketQua;

                // Chuyển sang nhóm hàng tiếp theo
                soAm /= 1000;
                nhom++;
            }

            // Xóa khoảng trắng thừa và chuẩn hóa chuỗi
            ketQua = ketQua.Trim();

            // Thêm từ "đồng" vào cuối
            if (!string.IsNullOrEmpty(ketQua))
            {
                ketQua += " đồng";
            }

            // Xử lý trường hợp số âm
            if (soTien < 0)
            {
                ketQua = "âm " + ketQua;
            }

            // Viết hoa chữ cái đầu tiên
            if (ketQua.Length > 0)
            {
                return char.ToUpper(ketQua[0]) + ketQua.Substring(1);
            }

            return ketQua;
        }

        /// <summary>
        /// Đọc 3 chữ số thành chữ (VD: 580 -> năm trăm tám mươi).
        /// </summary>
        private static string DocBaChuSo(int so)
        {
            if (so == 0) return "";

            // Tách hàng trăm, chục, đơn vị
            int tram = so / 100;
            int chuc = (so % 100) / 10;
            int donvi = so % 10;

            string result = "";

            // 1. Xử lý hàng trăm
            if (tram > 0)
            {
                result += ChuSo[tram] + " trăm";
            }

            // 2. Xử lý hàng chục và đơn vị
            int phanConLai = so % 100;
            if (phanConLai > 0)
            {
                if (tram > 0)
                {
                    result += " "; // Thêm khoảng trắng nếu có hàng trăm
                }

                if (chuc == 0)
                {
                    // VD: 105 -> một trăm lẻ năm (nếu tram > 0 và donvi > 0)
                    if (tram > 0 && donvi > 0)
                    {
                        result += "lẻ " + ChuSo[donvi];
                    }
                    else if (donvi > 0)
                    {
                        result += ChuSo[donvi];
                    }
                }
                else if (chuc == 1)
                {
                    // VD: 15 -> mười lăm
                    result += "mười";
                    if (donvi > 0)
                    {
                        result += " " + XuLyDonVi(donvi);
                    }
                }
                else // chuc >= 2
                {
                    // VD: 25 -> hai mươi lăm
                    result += ChuSo[chuc] + " mươi";
                    if (donvi > 0)
                    {
                        result += " " + XuLyDonVi(donvi);
                    }
                }
            }

            return result.Trim();
        }

        /// <summary>
        /// Xử lý chữ số đơn vị (0-9) trong ngữ cảnh đặc biệt.
        /// </summary>
        private static string XuLyDonVi(int donvi)
        {
            if (donvi == 5)
            {
                // 15 -> mười lăm (không phải mười năm)
                // 25 -> hai mươi lăm (không phải hai mươi năm)
                return "lăm";
            }
            else if (donvi == 1)
            {
                // 21 -> hai mươi mốt (không phải hai mươi một)
                return "mốt";
            }
            else
            {
                return ChuSo[donvi];
            }
        }
    }
}
