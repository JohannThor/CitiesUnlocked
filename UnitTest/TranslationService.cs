using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data.Implementations.Mock;

namespace UnitTest
{
    [TestClass]
    public class TranslationService
    {
        [TestMethod]
        public void TranslateHello()
        {
            var svc = new TranslateService();
            var result = svc.Translate("Hello", Common.Language.BritishEnglish, Common.Language.Spanish);
            Assert.AreEqual(result, "Hola");
            result = svc.Translate("Good day", Common.Language.BritishEnglish, Common.Language.Spanish);
            Assert.AreEqual(result, "Buenos Dias");
        }
    }
}
