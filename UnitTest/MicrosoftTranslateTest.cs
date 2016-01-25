using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class MicrosoftTranslateTest
    {
        [TestMethod]
        public void TestTranslateEngToSp()
        {
            var svc = new Data.Implementations.MicrosoftTranslateService();
            var result = svc.Translate("Hello",Common.Language.Spanish,Common.Language.BritishEnglish);
            Assert.AreEqual(result.ToLower(), "hola");
        }

        [TestMethod]
        public void TestTranslateSpToEng()
        {
            var svc = new Data.Implementations.MicrosoftTranslateService();
            var result = svc.Translate("hola", Common.Language.BritishEnglish, Common.Language.Spanish);
            Assert.AreEqual(result.ToLower(), "hello");
        }
    }
}
