using System;

namespace ClipNote.Models
{
    public class Text
    {
        public Text(string text)
        {
            Value = text;

            // 入力されたテキストの種類を判別するロジックを書く。
        }

        public string Value { get; private set; }

        public TextType Type { get; private set; } = TextType.Text;

        public DateTime CreatedAt { get; private set; }
    }
}