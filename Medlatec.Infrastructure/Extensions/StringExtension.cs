using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Medlatec.Infrastructure.Extensions
{
   public static class StringExtension
    {
        /// <summary>
        /// Loại bỏ các ký tự tiếng việt
        /// </summary>
        /// <param name="input">Chuỗi đầu vào.</param>
        /// <returns>Chuỗi đã được loại bỏ các ký tự tiếng việt</returns>
        public static string StripVietnameseChars(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return input;
            }
            input = input.Trim();
            const string sUtf8Lower = "a|á|à|ả|ã|ạ|ă|ắ|ằ|ẳ|ẵ|ặ|â|ấ|ầ|ẩ|ẫ|ậ|đ|e|é|è|ẻ|ẽ|ẹ|ê|ế|ề|ể|ễ|ệ|i|í|ì|ỉ|ĩ|ị|o|ó|ò|ỏ|õ|ọ|ô|ố|ồ|ổ|ỗ|ộ|ơ|ớ|ờ|ở|ỡ|ợ|u|ú|ù|ủ|ũ|ụ|ư|ứ|ừ|ử|ữ|ự|y|ý|ỳ|ỷ|ỹ|ỵ";

            const string sUtf8Upper = "A|Á|À|Ả|Ã|Ạ|Ă|Ắ|Ằ|Ẳ|Ẵ|Ặ|Â|Ấ|Ầ|Ẩ|Ẫ|Ậ|Đ|E|É|È|Ẻ|Ẽ|Ẹ|Ê|Ế|Ề|Ể|Ễ|Ệ|I|Í|Ì|Ỉ|Ĩ|Ị|O|Ó|Ò|Ỏ|Õ|Ọ|Ô|Ố|Ồ|Ổ|Ỗ|Ộ|Ơ|Ớ|Ờ|Ở|Ỡ|Ợ|U|Ú|Ù|Ủ|Ũ|Ụ|Ư|Ứ|Ừ|Ử|Ữ|Ự|Y|Ý|Ỳ|Ỷ|Ỹ|Ỵ";

            const string sUcs2Lower = "a|a|a|a|a|a|a|a|a|a|a|a|a|a|a|a|a|a|d|e|e|e|e|e|e|e|e|e|e|e|e|i|i|i|i|i|i|o|o|o|o|o|o|o|o|o|o|o|o|o|o|o|o|o|o|u|u|u|u|u|u|u|u|u|u|u|u|y|y|y|y|y|y";

            const string sUcs2Upper = "A|A|A|A|A|A|A|A|A|A|A|A|A|A|A|A|A|A|D|E|E|E|E|E|E|E|E|E|E|E|E|I|I|I|I|I|I|O|O|O|O|O|O|O|O|O|O|O|O|O|O|O|O|O|O|U|U|U|U|U|U|U|U|U|U|U|U|Y|Y|Y|Y|Y|Y";

            var aUtf8Lower = sUtf8Lower.Split('|');

            var aUtf8Upper = sUtf8Upper.Split('|');

            var aUcs2Lower = sUcs2Lower.Split('|');

            var aUcs2Upper = sUcs2Upper.Split('|');

            int nLimitChar = aUtf8Lower.GetUpperBound(0);

            for (int i = 1; i <= nLimitChar; i++)
            {
                input = input.Replace(aUtf8Lower[i], aUcs2Lower[i]);

                input = input.Replace(aUtf8Upper[i], aUcs2Upper[i]);
            }
            const string sUcs2Regex = @"[A-Za-z0-9- ]";
            var sEscaped =
                new Regex(sUcs2Regex, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.ExplicitCapture).
                    Replace(input, String.Empty);
            if (String.IsNullOrEmpty(sEscaped))
            {
                return input;
            }
            sEscaped = sEscaped.Replace("[", "\\[");
            sEscaped = sEscaped.Replace("]", "\\]");
            sEscaped = sEscaped.Replace("^", "\\^");
            string sEscapedregex = @"[" + sEscaped + "]";
            return
                new Regex(
                    sEscapedregex, RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.ExplicitCapture).
                    Replace(input, String.Empty);
        }

        /// <summary>
        /// Loại bỏ các ký tự phân cách
        /// </summary>
        /// <param name="input">Chuỗi đầu vào.</param>
        /// <returns>Chuỗi đã được lại bỏ các ký tự phân cách</returns>
        public static string StripDelimiters(this string input)
        {
            var delimiters = new[] { '|', '"', '\'', ';', ',', '.' };
            return input.StripChars(delimiters);
        }

        /// <summary>
        /// Loại bỏ các ký tự xác định.
        /// </summary>
        /// <param name="input">Chuỗi đầu vào.</param>
        /// <param name="strips">Mảng các ký tự cần loại bỏ.</param>
        /// <returns></returns>
        public static string StripChars(this string input, params char[] strips)
        {
            if (strips == null)
            {
                throw new ArgumentNullException("strips");
            }

            var scanner = new StringBuilder(input);
            var builder = new StringBuilder(scanner.Length);
            for (var i = 0; i < scanner.Length; i++)
            {
                if (strips.Any(c => scanner[i].Equals(c)))
                {
                    continue;
                }
                builder.Append(scanner[i]);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Thay thế các nhóm ký tự bằng một nhóm ký tự khác
        /// </summary>
        /// <param name="input">Chuỗi đầu vào.</param>
        /// <param name="strips">Mảng các chữ cần loại bỏ.</param>
        /// <param name="replacements">Mảng các chữ cần thay thế.</param>
        /// <returns></returns>
        public static string ReplaceCharGroups(this string input, string[] strips, char[] replacements)
        {
            if (strips == null)
            {
                throw new ArgumentNullException("strips");
            }
            if (replacements == null)
            {
                throw new ArgumentNullException("replacements");
            }
            if (strips.Length > replacements.Length)
            {
                throw new ArgumentException("Length of replacement array must be larger than strip array");
            }

            var scanner = new StringBuilder(input);
            for (var i = 0; i < scanner.Length; i++)
            {
                for (var j = 0; j < strips.Length; j++)
                {
                    if (strips[j].IndexOf(scanner[i]) != -1)
                    {
                        scanner[i] = replacements[j];
                    }
                }

            }
            return scanner.ToString();
        }

        /// <summary>
        /// Thay thế các nhóm ký tự bằng một nhóm ký tự khác
        /// </summary>
        /// <param name="input">Chuỗi đầu vào.</param>
        /// <param name="strips">Mảng các chữ cần loại bỏ.</param>
        /// <param name="replacements">Mảng các chữ cần thay thế.</param>
        /// <returns></returns>
        public static string ReplaceCharGroups(this string input, string[] strips, string[] replacements)
        {
            if (strips == null)
            {
                throw new ArgumentNullException("strips");
            }
            if (replacements == null)
            {
                throw new ArgumentNullException("replacements");
            }
            if (strips.Length > replacements.Length)
            {
                throw new ArgumentException("Length of replacement array must be larger than strip array");
            }

            var scanner = input.Select(i => i.ToString(CultureInfo.InvariantCulture)).ToArray();
            for (var i = 0; i < scanner.Length; i++)
            {
                for (var j = 0; j < strips.Length; j++)
                {
                    if (strips[j].IndexOf(scanner[i], StringComparison.Ordinal) != -1)
                    {
                        scanner[i] = replacements[j];
                    }
                }

            }
            return string.Join("", scanner);
        }

        /// <summary>
        /// Loại bỏ các ký tự html
        /// </summary>
        /// <param name="source">Chuỗi</param>
        /// <returns>Chuỗi đã được loại bỏ ký tự html</returns>
        public static string StripHtml(this string source)
        {
            try
            {
                // Remove HTML Development formatting
                // Replace line breaks with space
                // because browsers inserts space
                var result = source.Replace("\r", " ");
                // Replace line breaks with space
                // because browsers inserts space
                result = result.Replace("\n", " ");
                // Remove step-formatting
                result = result.Replace("\t", string.Empty);
                // Remove repeating spaces because browsers ignore them
                result = Regex.Replace(result,
                                                                      @"( )+", " ");

                // Remove the header (prepare first by clearing attributes)
                result = Regex.Replace(result,
                         @"<( )*head([^>])*>", "<head>",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"(<( )*(/)( )*head( )*>)", "</head>",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         "(<head>).*(</head>)", string.Empty,
                         RegexOptions.IgnoreCase);

                // remove all scripts (prepare first by clearing attributes)
                result = Regex.Replace(result,
                         @"<( )*script([^>])*>", "<script>",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"(<( )*(/)( )*script( )*>)", "</script>",
                         RegexOptions.IgnoreCase);
                //result = Regex.Replace(result,
                //         @"(<script>)([^(<script>\.</script>)])*(</script>)",
                //         string.Empty,
                //         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"(<script>).*(</script>)", string.Empty,
                         RegexOptions.IgnoreCase);

                // remove all styles (prepare first by clearing attributes)
                result = Regex.Replace(result,
                         @"<( )*style([^>])*>", "<style>",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"(<( )*(/)( )*style( )*>)", "</style>",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         "(<style>).*(</style>)", string.Empty,
                         RegexOptions.IgnoreCase);

                // insert tabs in spaces of <td> tags
                result = Regex.Replace(result,
                         @"<( )*td([^>])*>", "\t",
                         RegexOptions.IgnoreCase);

                // insert line breaks in places of <BR> and <LI> tags
                result = Regex.Replace(result,
                         @"<( )*br( )*>", "\r",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"<( )*li( )*>", "\r",
                         RegexOptions.IgnoreCase);

                // insert line paragraphs (double line breaks) in place
                // if <P>, <DIV> and <TR> tags
                result = Regex.Replace(result,
                         @"<( )*div([^>])*>", "\r\r",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"<( )*tr([^>])*>", "\r\r",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"<( )*p([^>])*>", "\r\r",
                         RegexOptions.IgnoreCase);

                // Remove remaining tags like <a>, links, images,
                // comments etc - anything that's enclosed inside < >
                result = Regex.Replace(result,
                         @"<[^>]*>", string.Empty,
                         RegexOptions.IgnoreCase);

                // replace special characters:
                result = Regex.Replace(result,
                         @" ", " ",
                         RegexOptions.IgnoreCase);

                result = Regex.Replace(result,
                         @"&bull;", " * ",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&lsaquo;", "<",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&rsaquo;", ">",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&trade;", "(tm)",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&frasl;", "/",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&lt;", "<",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&gt;", ">",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&copy;", "(c)",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         @"&reg;", "(r)",
                         RegexOptions.IgnoreCase);
                // Remove all others. More can be added, see
                // http://hotwired.lycos.com/webmonkey/reference/special_characters/
                result = Regex.Replace(result,
                         @"&(.{2,6});", string.Empty,
                         RegexOptions.IgnoreCase);

                // for testing
                //Regex.Replace(result,
                //       this.txtRegex.Text,string.Empty,
                //       RegexOptions.IgnoreCase);

                // make line breaking consistent
                result = result.Replace("\n", "\r");

                // Remove extra line breaks and tabs:
                // replace over 2 breaks with 2 and over 4 tabs with 4.
                // Prepare first to remove any whitespaces in between
                // the escaped characters and remove redundant tabs in between line breaks
                result = Regex.Replace(result,
                         "(\r)( )+(\r)", "\r\r",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         "(\t)( )+(\t)", "\t\t",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         "(\t)( )+(\r)", "\t\r",
                         RegexOptions.IgnoreCase);
                result = Regex.Replace(result,
                         "(\r)( )+(\t)", "\r\t",
                         RegexOptions.IgnoreCase);
                // Remove redundant tabs
                result = Regex.Replace(result,
                         "(\r)(\t)+(\r)", "\r\r",
                         RegexOptions.IgnoreCase);
                // Remove multiple tabs following a line break with just one tab
                result = Regex.Replace(result,
                         "(\r)(\t)+", "\r\t",
                         RegexOptions.IgnoreCase);
                // Initial replacement target string for line breaks
                var breaks = "\r\r\r";
                // Initial replacement target string for tabs
                var tabs = "\t\t\t\t\t";
                for (var index = 0; index < result.Length; index++)
                {
                    result = result.Replace(breaks, "\r\r");
                    result = result.Replace(tabs, "\t\t\t\t");
                    breaks = breaks + "\r";
                    tabs = tabs + "\t";
                }

                return result.Trim();
            }
            catch
            {
                return source;
            }
        }

        /// <summary>
        /// Kiểm tra chuỗi có phải là địa chỉ email hay không
        /// </summary>
        /// <param name="input">Chuỗi</param>
        /// <returns></returns>
        public static bool IsEmailAddress(this string input)
        {
            var regex = new Regex("^(?:[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+\\.)*[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+@(?:(?:(?:[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!\\.)){0,61}[a-zA-Z0-9]?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!$)){0,61}[a-zA-Z0-9]?)|(?:\\[(?:(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\.){3}(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\]))$", RegexOptions.IgnoreCase);
            return regex.IsMatch(input);
        }

        public static bool IsPhoneNumber(this string input)
        {
            var regex = new Regex(@"(\+[0-9]{2}|\+[0-9]{2}\(0\)|\(\+[0-9]{2}\)\(0\)|00[0-9]{2}|0)([0-9]{9}|[0-9\-\s]{9,18})", RegexOptions.IgnoreCase);
            return regex.IsMatch(input);
        }

        /// <summary>
        /// Đọc dung lượng file thành chuỗi
        /// </summary>
        /// <param name="filesize">Dung lượng</param>
        /// <returns></returns>
        public static string ReadFileSize(decimal filesize)
        {
            const decimal oneKiloByte = 1024;
            const decimal oneMegaByte = 1048576;
            const decimal oneGigaByte = 1073741824;
            if (filesize >= oneGigaByte)
            {
                return (filesize / oneGigaByte).ToString("0.00", CultureInfo.InvariantCulture) + " GB";
            }
            if (filesize >= oneMegaByte)
            {
                return (filesize / oneMegaByte).ToString("0.00", CultureInfo.InvariantCulture) + " MB";
            }
            if (filesize >= oneKiloByte)
            {
                return (filesize / oneKiloByte).ToString("0", CultureInfo.InvariantCulture) + " KB";
            }
            return filesize + " bytes";
        }

        public static string ToUrlString(this string str)
        {
            return (new Regex(@"[-]{2}")).Replace((new Regex(@"[^\w]")).Replace(str.ToLower().StripVietnameseChars(), "-"), "-");
        }

        public static string ToPlainText(this string str)
        {
            return string.IsNullOrEmpty(str) ? string.Empty : Regex.Replace(str, @"<(.|\n)*?>", "");
        }

        /// <summary>
        /// Convert first char to lower case. It usefull for convert entity object properties to mongodb properties
        /// </summary>
        /// <param name="str">String that want to convert</param>
        /// <returns></returns>
        public static string ConvertFirstCharToLowerCase(this string str)
        {
            if (string.IsNullOrEmpty(str) || char.IsLower(str[0]))
                return str;

            return char.ToLowerInvariant(str[0]) + str.Substring(1);
        }

        /// <summary>
        /// Get first name
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns></returns>
        public static string GetFirstName(this string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
                return string.Empty;

            var names = fullName.Split(' ');
            return names.Any() ? names.FirstOrDefault() : string.Empty;
        }

        /// <summary>
        /// Get middle name
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns></returns>
        public static string GetMiddleName(this string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
                return string.Empty;

            var names = fullName.Split(' ');
            if (!names.Any()) return string.Empty;

            if (names.Length > 3)
            {
                var middleName = string.Empty;
                for (var i = 1; i < names.Length - 1; i++)
                {
                    middleName += names[i] + " ";
                }
                return middleName;
            }

            if (names.Length < 3)
                return string.Empty;

            return names[1];
        }


        /// <summary>
        /// Get last name
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns></returns>
        public static string GetLastName(this string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
                return string.Empty;

            var names = fullName.Split(' ');
            if (!names.Any()) return string.Empty;

            return names.LastOrDefault();
        }

        public static string NormalizeForSearch(this string source)
        {
            return source.RemoveDiacritics().ToUpper();
        }

        public static string RemoveDiacritics(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            try
            {
                str = str.ToLower().Trim();
                str = Regex.Replace(str, @"[\r|\n]", " ");
                str = Regex.Replace(str, @"\s+", " ");
                str = Regex.Replace(str, "[áàảãạâấầẩẫậăắằẳẵặ]", "a");
                str = Regex.Replace(str, "[éèẻẽẹêếềểễệ]", "e");
                str = Regex.Replace(str, "[iíìỉĩị]", "i");
                str = Regex.Replace(str, "[óòỏõọơớờởỡợôốồổỗộ]", "o");
                str = Regex.Replace(str, "[úùủũụưứừửữự]", "u");
                str = Regex.Replace(str, "[yýỳỷỹỵ]", "y");
                str = Regex.Replace(str, "[đ]", "d");

                //Remove special character
                str = Regex.Replace(str, "[\"`~!@#$%^&*()-+=?/>.<,{}[]|]\\]", "");
                str = str.Replace("̀", "").Replace("̣", "").Replace("̉", "").Replace("̃", "").Replace("́", "");
                return str;
            }
            catch (Exception)
            {
                return str;
            }
        }


    }
}
