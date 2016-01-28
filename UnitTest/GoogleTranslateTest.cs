using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class GoogleTranslateTest
    {
        [TestMethod]
        public void TestTranslateEngToSp()
        {
            var svc = new Data.Implementations.GoogleTranslateService();
            var result = svc.Translate("Hello",Common.Language.Spanish,Common.Language.BritishEnglish);
            System.Diagnostics.Debug.WriteLine(result);
            Assert.AreEqual(result.ToLower(), "hola");
        }

        [TestMethod]
        public void TestTranslateSpToEng()
        {
            var svc = new Data.Implementations.GoogleTranslateService();
            var result = svc.Translate("hola", Common.Language.BritishEnglish, Common.Language.Spanish);
            Assert.AreEqual(result.ToLower(), "hey there");
        }

        [TestMethod]
        public void TestLanguageDetect()
        {
            var svc = new Data.Implementations.GoogleTranslateService();
            var result = svc.DetectLanguage("hola, buenos dias");
            Assert.AreEqual(result, "es");
        }
    }
}
