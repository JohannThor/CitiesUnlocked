using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain
{
    public class TranslationController
    {
        public string Translate(String stringToTranslate)
        {
            var svc = new Data.Implementations.Mock.TranslateService();
            return svc.Translate(stringToTranslate, Common.Language.BritishEnglish, Common.Language.Spanish);
        }
    }
}
