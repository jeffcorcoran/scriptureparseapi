using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScriptureParseApi.Models;
using System.Linq;

namespace ScriptureParseApiTests
{
    [TestClass]
    public class ScriptureParseTests
    {
        [TestMethod]
        public void BasicReference()
        {
            string scripture = "Genesis 1:1";

            var parsed = ScriptureOps.ParseScriptureFromString(scripture);

            Assert.AreEqual(parsed.Count, 1);
            Assert.AreEqual(parsed.First().Book, "Genesis");
            Assert.AreEqual(parsed.First().StartChapter, 1);
            Assert.AreEqual(parsed.First().StartVerse, 1);
            Assert.AreEqual(parsed.First().EndChapter, null);
            Assert.AreEqual(parsed.First().EndVerse, null);
        }

        [TestMethod]
        public void BasicReferenceWithVerseRange()
        {
            string scripture = "Genesis 1:1-2";

            var parsed = ScriptureOps.ParseScriptureFromString(scripture);

            Assert.AreEqual(parsed.Count, 1);
            Assert.AreEqual(parsed.First().Book, "Genesis");
            Assert.AreEqual(parsed.First().StartChapter, 1);
            Assert.AreEqual(parsed.First().StartVerse, 1);
            Assert.AreEqual(parsed.First().EndChapter, null);
            Assert.AreEqual(parsed.First().EndVerse, 2);
        }

        [TestMethod]
        public void BasicReferenceWithChapterRange()
        {
            string scripture = "Genesis 1:1-2:3";

            var parsed = ScriptureOps.ParseScriptureFromString(scripture);

            Assert.AreEqual(parsed.Count, 1);
            Assert.AreEqual(parsed.First().Book, "Genesis");
            Assert.AreEqual(parsed.First().StartChapter, 1);
            Assert.AreEqual(parsed.First().StartVerse, 1);
            Assert.AreEqual(parsed.First().EndChapter, 2);
            Assert.AreEqual(parsed.First().EndVerse, 3);
        }

        [TestMethod]
        public void DualReference()
        {
            string scripture = "Genesis 1:2;Genesis 3:4";

            var parsed = ScriptureOps.ParseScriptureFromString(scripture);

            Assert.AreEqual(parsed.Count, 2);
            Assert.AreEqual(parsed.First().Book, "Genesis");
            Assert.AreEqual(parsed.First().StartChapter, 1);
            Assert.AreEqual(parsed.First().StartVerse, 2);
            Assert.AreEqual(parsed.First().EndChapter, null);
            Assert.AreEqual(parsed.First().EndVerse, null);
            Assert.AreEqual(parsed.Last().Book, "Genesis");
            Assert.AreEqual(parsed.Last().StartChapter, 3);
            Assert.AreEqual(parsed.Last().StartVerse, 4);
            Assert.AreEqual(parsed.Last().EndChapter, null);
            Assert.AreEqual(parsed.Last().EndVerse, null);
        }

        public void DualReferenceWithVerseRange()
        {
            string scripture = "Genesis 1:2-8;Genesis 3:4";

            var parsed = ScriptureOps.ParseScriptureFromString(scripture);

            Assert.AreEqual(parsed.Count, 2);
            Assert.AreEqual(parsed.First().Book, "Genesis");
            Assert.AreEqual(parsed.First().StartChapter, 1);
            Assert.AreEqual(parsed.First().StartVerse, 2);
            Assert.AreEqual(parsed.First().EndChapter, null);
            Assert.AreEqual(parsed.First().EndVerse, 8);
            Assert.AreEqual(parsed.Last().Book, "Genesis");
            Assert.AreEqual(parsed.Last().StartChapter, 3);
            Assert.AreEqual(parsed.Last().StartVerse, 4);
            Assert.AreEqual(parsed.Last().EndChapter, null);
            Assert.AreEqual(parsed.Last().EndVerse, null);
        }

        [TestMethod]
        public void DualReferenceWithChapterRange()
        {
            string scripture = "Genesis 1:2-8;Genesis 3:4-5:6";

            var parsed = ScriptureOps.ParseScriptureFromString(scripture);

            Assert.AreEqual(parsed.Count, 2);
            Assert.AreEqual(parsed.First().Book, "Genesis");
            Assert.AreEqual(parsed.First().StartChapter, 1);
            Assert.AreEqual(parsed.First().StartVerse, 2);
            Assert.AreEqual(parsed.First().EndChapter, null);
            Assert.AreEqual(parsed.First().EndVerse, 8);
            Assert.AreEqual(parsed.Last().Book, "Genesis");
            Assert.AreEqual(parsed.Last().StartChapter, 3);
            Assert.AreEqual(parsed.Last().StartVerse, 4);
            Assert.AreEqual(parsed.Last().EndChapter, 5);
            Assert.AreEqual(parsed.Last().EndVerse, 6);
        }

        [TestMethod]
        public void DualReferenceSingleUndefinedBook()
        {
            string scripture = "Genesis 1:2;3:4";

            var parsed = ScriptureOps.ParseScriptureFromString(scripture);

            Assert.AreEqual(parsed.Count, 2);
            Assert.AreEqual(parsed.First().Book, "Genesis");
            Assert.AreEqual(parsed.First().StartChapter, 1);
            Assert.AreEqual(parsed.First().StartVerse, 2);
            Assert.AreEqual(parsed.First().EndChapter, null);
            Assert.AreEqual(parsed.First().EndVerse, null);
            Assert.AreEqual(parsed.Last().Book, "Genesis");
            Assert.AreEqual(parsed.Last().StartChapter, 3);
            Assert.AreEqual(parsed.Last().StartVerse, 4);
            Assert.AreEqual(parsed.Last().EndChapter, null);
            Assert.AreEqual(parsed.Last().EndVerse, null);
        }

        [TestMethod]
        public void DualReferenceSingleUndefinedBookVerseRange()
        {
            string scripture = "Genesis 1:2-8;3:4";

            var parsed = ScriptureOps.ParseScriptureFromString(scripture);

            Assert.AreEqual(parsed.Count, 2);
            Assert.AreEqual(parsed.First().Book, "Genesis");
            Assert.AreEqual(parsed.First().StartChapter, 1);
            Assert.AreEqual(parsed.First().StartVerse, 2);
            Assert.AreEqual(parsed.First().EndChapter, null);
            Assert.AreEqual(parsed.First().EndVerse, 8);
            Assert.AreEqual(parsed.Last().Book, "Genesis");
            Assert.AreEqual(parsed.Last().StartChapter, 3);
            Assert.AreEqual(parsed.Last().StartVerse, 4);
            Assert.AreEqual(parsed.Last().EndChapter, null);
            Assert.AreEqual(parsed.Last().EndVerse, null);
        }

        [TestMethod]
        public void DualReferenceSingleUndefinedBookChapterRange()
        {
            string scripture = "Genesis 1:2-8;3:4-5:6";

            var parsed = ScriptureOps.ParseScriptureFromString(scripture);

            Assert.AreEqual(parsed.Count, 2);
            Assert.AreEqual(parsed.First().Book, "Genesis");
            Assert.AreEqual(parsed.First().StartChapter, 1);
            Assert.AreEqual(parsed.First().StartVerse, 2);
            Assert.AreEqual(parsed.First().EndChapter, null);
            Assert.AreEqual(parsed.First().EndVerse, 8);
            Assert.AreEqual(parsed.Last().Book, "Genesis");
            Assert.AreEqual(parsed.Last().StartChapter, 3);
            Assert.AreEqual(parsed.Last().StartVerse, 4);
            Assert.AreEqual(parsed.Last().EndChapter, 5);
            Assert.AreEqual(parsed.Last().EndVerse, 6);
        }

        [TestMethod]
        public void MultipleChaptersNoVerses()
        {
            string scripture = "Genesis 1,4";

            var parsed = ScriptureOps.ParseScriptureFromString(scripture);

            Assert.AreEqual(parsed.Count, 2);
            Assert.AreEqual(parsed.First().Book, "Genesis");
            Assert.AreEqual(parsed.First().StartChapter, 1);
            Assert.AreEqual(parsed.First().StartVerse, null);
            Assert.AreEqual(parsed.First().EndChapter, null);
            Assert.AreEqual(parsed.First().EndVerse, null);
            Assert.AreEqual(parsed.Last().Book, "Genesis");
            Assert.AreEqual(parsed.Last().StartChapter, 4);
            Assert.AreEqual(parsed.Last().StartVerse, null);
            Assert.AreEqual(parsed.Last().EndChapter, null);
            Assert.AreEqual(parsed.Last().EndVerse, null);
        }

        [TestMethod]
        public void ChapterRangeNoVerses()
        {
            string scripture = "Genesis 1-4";

            var parsed = ScriptureOps.ParseScriptureFromString(scripture);

            Assert.AreEqual(parsed.Count, 1);
            Assert.AreEqual(parsed.First().Book, "Genesis");
            Assert.AreEqual(parsed.First().StartChapter, 1);
            Assert.AreEqual(parsed.First().StartVerse, null);
            Assert.AreEqual(parsed.First().EndChapter, 4);
            Assert.AreEqual(parsed.First().EndVerse, null);
        }

        [TestMethod]
        public void MultipleBooksSingleBookMultiChapterWithSingleBookVerseRange()
        {
            string scripture = "Genesis 16; 21; 1 John 4:21-31";

            var parsed = ScriptureOps.ParseScriptureFromString(scripture);

            Assert.AreEqual(parsed.Count, 3);
            Assert.AreEqual(parsed[0].Book, "Genesis");
            Assert.AreEqual(parsed[0].StartChapter, 16);
            Assert.AreEqual(parsed[0].StartVerse, null);
            Assert.AreEqual(parsed[0].EndChapter, null);
            Assert.AreEqual(parsed[0].EndVerse, null);
            Assert.AreEqual(parsed[1].Book, "Genesis");
            Assert.AreEqual(parsed[1].StartChapter, 21);
            Assert.AreEqual(parsed[1].StartVerse, null);
            Assert.AreEqual(parsed[1].EndChapter, null);
            Assert.AreEqual(parsed[1].EndVerse, null);
            Assert.AreEqual(parsed[2].Book, "1 John");
            Assert.AreEqual(parsed[2].StartChapter, 4);
            Assert.AreEqual(parsed[2].StartVerse, 21);
            Assert.AreEqual(parsed[2].EndChapter, null);
            Assert.AreEqual(parsed[2].EndVerse, 31);
        }

        [TestMethod]
        public void MultipleBooksSingleBookMultiChapterWithSingleBookChapterRange()
        {
            string scripture = "Genesis 16; 21; 1 John 4:21-5:1";

            var parsed = ScriptureOps.ParseScriptureFromString(scripture);

            Assert.AreEqual(parsed.Count, 3);
            Assert.AreEqual(parsed[0].Book, "Genesis");
            Assert.AreEqual(parsed[0].StartChapter, 16);
            Assert.AreEqual(parsed[0].StartVerse, null);
            Assert.AreEqual(parsed[0].EndChapter, null);
            Assert.AreEqual(parsed[0].EndVerse, null);
            Assert.AreEqual(parsed[1].Book, "Genesis");
            Assert.AreEqual(parsed[1].StartChapter, 21);
            Assert.AreEqual(parsed[1].StartVerse, null);
            Assert.AreEqual(parsed[1].EndChapter, null);
            Assert.AreEqual(parsed[1].EndVerse, null);
            Assert.AreEqual(parsed[2].Book, "1 John");
            Assert.AreEqual(parsed[2].StartChapter, 4);
            Assert.AreEqual(parsed[2].StartVerse, 21);
            Assert.AreEqual(parsed[2].EndChapter, 5);
            Assert.AreEqual(parsed[2].EndVerse, 1);
        }

        //Genesis 16; 21; Galatians 4:21-31 *
        //Genesis; Luke 24:25-27,44-49
        //Acts 20,27-28 *
        //John 1:1-3, 14
        //Luke 1, 2*
        //Luke 1
        //1 John 1
        //1 John 1:1
        //1 John 1:1-3
        //1 John 1:1-3,7
        //1 John 1:1-3,7-13
        //1 John 1:1-3,2:7-13
        //1 John 1:1-3,2:7-3:13
    }
}
