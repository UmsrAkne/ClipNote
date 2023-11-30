using ClipNote.Models;
using NUnit.Framework;

namespace ClipNoteTests.Models
{
    public class TextTest
    {
        [Test]
        public void TextTypeの判別テスト()
        {
            Assert.That(new Text("test").Type, Is.EqualTo(TextType.Text), "ノーマルなテキストとして判定される");

            Assert.That(new Text("test.txt").Type, Is.EqualTo(TextType.FileName), "ドットが間に入っているのでファイル名で判定");

            Assert.That(new Text(@"c:\test\directory").Type, Is.EqualTo(TextType.Path), "パスとして判定される");
            Assert.That(new Text(@"C:\test\directory").Type, Is.EqualTo(TextType.Path), "パスとして判定される");

            Assert.That(new Text("001").Type, Is.EqualTo(TextType.Number), "整数なので数値として判定される");
            Assert.That(new Text("1.1").Type, Is.EqualTo(TextType.Number), "実数でも数値で判定される");
        }
    }
}