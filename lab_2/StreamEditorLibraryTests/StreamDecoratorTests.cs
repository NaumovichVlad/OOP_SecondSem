using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;
using StreamEditorLibrary;
using System.IO;

namespace StreamEditorLibraryTests
{
    [TestClass]
    public class StreamDecoratorTests
    {
        [TestMethod]
        public void MemoryStream_TestOne()
        {
            int count;
            byte[] byteArray;
            UnicodeEncoding uniEncoding = new UnicodeEncoding();
            var testString = uniEncoding.GetBytes("Some text");
            using (StreamDecorator decorator = new StreamDecorator(new MemoryStream(100)))
            {
                decorator.Write(testString, 0, testString.Length);
                byteArray = new byte[decorator.Length];
                count = decorator.Read(byteArray, 0, 2);
                var actual = decorator.ComputeEfficiency();
                var expected = Math.Round(decorator.UsefulTime / decorator.TotalTime * 100, 3);
                Assert.AreEqual(true, Math.Abs(expected - actual) < 0.1);
                Assert.AreEqual(true, decorator.CanRead);
                Assert.AreEqual(true, decorator.CanSeek);
            }
        }

        [TestMethod]
        public void OtherProperties_TestOne()
        {
            string path = @"cMyTest.txt";

            if (File.Exists(path))
                File.Delete(path);

            using (FileStream fss = File.Create(path))
            {
                StreamDecorator fs = new StreamDecorator(fss);
                AddText(fs, "This is some text");
                AddText(fs, "This is some more text,");
                AddText(fs, "\r\nand this is on a new line");
                AddText(fs, "\r\n\r\nThe following is a subset of characters:\r\n");

                for (int i = 1; i < 120; i++)
                {
                    AddText(fs, Convert.ToChar(i).ToString());
                }
                var actual = fs.ComputeEfficiency();
                var expected = Math.Round(Convert.ToDouble(fs.UsefulTime) / Convert.ToDouble(fs.TotalTime) * 100, 3);
                Assert.AreEqual(true, Math.Abs(expected - actual) < 0.1);
                Assert.AreEqual(true, fs.CanWrite);
            }

            void AddText(Stream fs, string value)
            {
                byte[] info = new UTF8Encoding(true).GetBytes(value);
                fs.Write(info, 0, info.Length);
            }
        }
    }
}
