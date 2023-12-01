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

        [Test]
        public void FileNameの判定テスト()
        {
            // ファイル名として判定されるもの
            Assert.That(new Text("test.txt").Type, Is.EqualTo(TextType.FileName), "ドットが間に入っているのでファイル名で判定");
            Assert.That(new Text("test-test_test.txt").Type, Is.EqualTo(TextType.FileName), "ハイフンとアンダーバーはファイル名");

            // ファイル名ではないと判定されるもの
            Assert.That(new Text("test.").Type, Is.EqualTo(TextType.Text), "最後にだけドットがついてるものはファイル名ではない");
            Assert.That(new Text("test.a\r").Type, Is.EqualTo(TextType.Text), "改行コードを含むものはファイル名ではない");
            Assert.That(new Text("test.a\n").Type, Is.EqualTo(TextType.Text), "改行コードを含むものはファイル名ではない");
            Assert.That(new Text("test.a\r\n").Type, Is.EqualTo(TextType.Text), "改行コードを含むものはファイル名ではない");
            Assert.That(new Text("test$.a").Type, Is.EqualTo(TextType.Text), "ハイフンとアンダーバー以外の記号を含むとファイル名ではない");
            Assert.That(new Text("test@.a").Type, Is.EqualTo(TextType.Text), "ハイフンとアンダーバー以外の記号を含むとファイル名ではない");
            Assert.That(new Text("test#.a").Type, Is.EqualTo(TextType.Text), "ハイフンとアンダーバー以外の記号を含むとファイル名ではない");
            Assert.That(new Text("test test.a").Type, Is.EqualTo(TextType.Text), "空白を含むものはファイル名ではない");
            Assert.That(new Text("test\ttest.a").Type, Is.EqualTo(TextType.Text), "空白を含むものはファイル名ではない");
        }
    }
}