using System;

namespace BanHangOnline.Models.Common
{
    public class Filter
    {
        private static readonly string[] VietNamChar = new string[]
        {
            "aAeEoOuUiIdDyY",
            "áàạảãâấầậẩẫăắằặẳẵ",
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
            "éèẹẻẽêếềệểễ",
            "ÉÈẸẺẼÊẾỀỆỂỄ",
            "óòọỏõôốồộổỗơớờợởỡ",
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
            "úùụủũưứừựửữ",
            "ÚÙỤỦŨƯỨỪỰỬỮ",
            "íìịỉĩ",
            "ÍÌỊỈĨ",
            "đ",
            "Đ",
            "ýỳỵỷỹ",
            "ÝỲỴỶỸ"
        };

        /// <summary>
        /// Chuẩn hóa chuỗi: bỏ dấu tiếng Việt, loại bỏ ký tự đặc biệt, thay khoảng trắng bằng dấu '-'
        /// </summary>
        public static string FilterChar(string str)
        {
            str = str.Trim();

            // Chuyển ký tự có dấu thành không dấu
            for (int i = 1; i < VietNamChar.Length; i++)
            {
                for (int j = 0; j < VietNamChar[i].Length; j++)
                {
                    str = str.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);
                }
            }

            // Thay khoảng trắng bằng dấu '-'
            str = str.Replace(" ", "-");

            // Loại bỏ ký tự đặc biệt
            string[] specialChars = new string[]
            {
                "--", ":", ";", ",", ".", "?", "/", "\"", "'", "#", "%", "&", "*", "!", "@", "$", "^",
                "(", ")", "+", "=", "<", ">", "{", "}", "[", "]", "|", "\\", "`", "~"
            };

            foreach (var ch in specialChars)
            {
                str = str.Replace(ch, "");
            }

            return str.ToLower();
        }

        /// <summary>
        /// Chuyển chuỗi có dấu thành không dấu, giữ nguyên khoảng trắng
        /// </summary>
        public static string ChuyenCoDauThanhKhongDau(string str)
        {
            str = str.Trim();
            for (int i = 1; i < VietNamChar.Length; i++)
            {
                for (int j = 0; j < VietNamChar[i].Length; j++)
                {
                    str = str.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);
                }
            }
            return str;
        }
    }
}
