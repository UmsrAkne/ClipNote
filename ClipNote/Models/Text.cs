using System;
using System.Text.RegularExpressions;

namespace ClipNote.Models
{
    public class Text
    {
        public Text(string text)
        {
            Value = text;

            if (string.IsNullOrEmpty(text))
            {
                return;
            }

            // 入力されたテキストの種類を判定する。
            var isInt = int.TryParse(text, out _);
            var isDouble = double.TryParse(text, out _);

            // 整数か実数のどちらかであるか判定する。
            if (isInt || isDouble)
            {
                Type = TextType.Number;
                return;
            }

            // FullPath どうかを判定する。 Windows のパスである前提
            if (Regex.IsMatch(text, "^[A-Za-z]:\\.*"))
            {
                Type = TextType.Path;
                return;
            }

            // ファイル名かどうかを判定する。
            if (Regex.IsMatch(text, @"[a-zA-Z0-9_-]+\..+") && !Regex.IsMatch(text, @"[\r\n \t]"))
            {
                Type = TextType.FileName;
            }
        }

        public string Value { get; private set; }

        public TextType Type { get; private set; } = TextType.Text;

        public DateTime CreatedAt { get; private set; } = DateTime.Now;

        public string Comment { get; set; } = string.Empty;
    }
}